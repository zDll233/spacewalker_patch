@echo off
setlocal

echo ============================================
echo  VDD120Switcher - autostart installer
echo  Keeps every VITURE Virtual Display at
echo  120Hz after SpaceWalker / Immersive3D
echo  creates it (created at 60Hz by the driver,
echo  this tool upgrades it within ~2s).
echo ============================================

set "DEST=%LOCALAPPDATA%\VITURE\VDD120Switcher"

if not exist "%DEST%" mkdir "%DEST%"
copy /Y "%~dp0VDD120Switcher.exe" "%DEST%\" >nul
if not exist "%DEST%\run_hidden.vbs" (
    echo Set sh = CreateObject("WScript.Shell")> "%DEST%\run_hidden.vbs"
    echo sh.Run "%DEST%\VDD120Switcher.exe --watch 2000", 0, False>> "%DEST%\run_hidden.vbs"
)
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Run" /v "VITUREVDD120" /t REG_SZ /d "wscript.exe \"%DEST%\run_hidden.vbs\"" /f >nul

start "" wscript.exe "%DEST%\run_hidden.vbs"
echo.
echo [OK] Installed + autostart on next login. Running now (hidden).
echo      To verify: run VDD120Switcher.exe manually - all VITURE
echo      Virtual Displays should report 120Hz.
pause
