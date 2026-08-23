@echo off
setlocal
rem ============================================================
rem  cleanup_vdd.bat - 清空所有 VITURE 虚拟显示器(残留清理)
rem
rem  用途:某些残留 VDD(如实验工具创建的、驱动 watchdog 不清理
rem        的孤儿显示器)无法用普通 API 删除——驱动协议没有
rem        "按名删除/枚举"接口,删除必须精确 MonitorGuid(创建时
rem        随机生成、不对外暴露)。最干净的清法就是重启 VDA 驱动。
rem
rem  双击运行即可:非管理员时自动 UAC 提权,然后重启
rem  ROOT\DISPLAY\0001(VitureVDA),所有 VDD 立即清空。
rem ============================================================

rem --- 非管理员则自动提权重跑 ---
net session >nul 2>&1
if not %errorlevel%==0 (
    echo Requesting administrator rights ...
    powershell -NoProfile -Command "Start-Process -FilePath '%~f0' -Verb RunAs"
    exit /b 0
)

echo [cleanup] restarting VitureVDA driver (ROOT\DISPLAY\0001) ...
pnputil /restart-device "ROOT\DISPLAY\0001"
echo [cleanup] done - all VITURE virtual displays removed.
echo.
echo Tip: the apps will recreate their virtual displays next time a
echo layout is activated. VDDs at 60Hz can be boosted to 120Hz with
echo boost.bat afterwards.
pause
exit /b 0