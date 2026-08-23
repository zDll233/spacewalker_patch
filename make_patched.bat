@echo off
setlocal

rem ============================================================
rem  make_patched.bat - 克隆/更新后一键重建补丁 exe
rem
rem  SpaceWalker.exe.orig/.patched 不进 git(93MB x2),克隆后
rem  运行本脚本自动重建:
rem    1) 从安装目录复制原版(已有 .orig 则跳过)
rem    2) 缺 bundle_extracted 时从 .orig 提取(bundleextract3)
rem    3) 构建 swpatch(优先项目内 dotnet\ SDK)
rem    4) 生成 SpaceWalker.exe.patched
rem    5) 提示运行 install_sw.bat 安装
rem ============================================================

set "SWPATH="
if exist "%~dp0swpath.txt" set /p SWPATH=<"%~dp0swpath.txt"
if not exist "%SWPATH%" (
    if exist "C:\Program Files\VITURE\SpaceWalker\SpaceWalker.exe" set "SWPATH=C:\Program Files\VITURE\SpaceWalker\SpaceWalker.exe"
)
if not exist "%SWPATH%" (
    echo [!] Cannot find SpaceWalker.exe.
    echo     Install SpaceWalker first, or put its path in swpath.txt
    echo     ^(run install_sw.bat once, or edit swpath.txt manually^).
    pause
    exit /b 1
)
echo [1/4] original exe: %SWPATH%

rem ---- 1) 取原版 ----
if not exist "%~dp0SpaceWalker.exe.orig" (
    copy /Y "%SWPATH%" "%~dp0SpaceWalker.exe.orig" >nul
    echo [OK] copied original to SpaceWalker.exe.orig
) else (
    echo [OK] SpaceWalker.exe.orig already present
)

rem 校验 .orig 是干净原版(偏移 0x3AA031C 应为 0x3D;补丁版是 0x3F)
powershell -NoProfile -Command "$b=[IO.File]::ReadAllBytes('%~dp0SpaceWalker.exe.orig'); if($b.Length -gt 0x3AA031D -and $b[0x3AA031C] -eq 0x3D){exit 0}else{exit 1}"
if errorlevel 1 (
    echo [!] .orig is NOT the clean original ^(patch bytes already present^).
    echo     The install dir may already contain the patched exe.
    echo     Get a clean copy: reinstall/repair SpaceWalker, or restore
    echo     the exe from the app's own installer, then run this again.
    pause
    exit /b 1
)
echo [OK] .orig verified as clean original

rem ---- 2) 提取 bundle(每次都重新提取,保证与 .orig 一致)----
if not exist "%~dp0tools\bundleextract3.exe" (
    echo [!] tools\bundleextract3.exe missing - re-clone the repo.
    pause
    exit /b 1
)
echo [2/4] extracting bundle ...
"%~dp0tools\bundleextract3.exe" "%~dp0SpaceWalker.exe.orig" "%~dp0bundle_extracted" 58D96B4 >nul
if errorlevel 1 (
    echo [!] bundle extraction failed.
    pause
    exit /b 1
)

rem ---- 3) 构建 swpatch ----
set "DOTNET=%~dp0dotnet\dotnet.exe"
if not exist "%DOTNET%" set "DOTNET=dotnet"
echo [3/4] building swpatch ...
"%DOTNET%" build "%~dp0patch\swpatch.csproj" -c Release --nologo -v q
if errorlevel 1 (
    echo [!] swpatch build failed. Need .NET SDK ^(project dotnet\ or system dotnet^).
    pause
    exit /b 1
)

rem ---- 4) 生成 patched ----
echo [4/4] generating SpaceWalker.exe.patched ...
"%DOTNET%" "%~dp0patch\bin\Release\net8.0\swpatch.dll" "%~dp0SpaceWalker.exe.orig" "%~dp0SpaceWalker.exe.patched" "%~dp0bundle_extracted"
if errorlevel 1 (
    echo [!] patch generation failed.
    pause
    exit /b 1
)

echo.
echo ============================================
echo  SpaceWalker.exe.patched generated.
echo  Next step: run install_sw.bat (as admin)
echo  to install it. Daily use: boost.bat
echo ============================================
pause
exit /b 0