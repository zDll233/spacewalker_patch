@echo off
setlocal
reg delete "HKCU\Software\Microsoft\Windows\CurrentVersion\Run" /v "VITUREVDD120" /f >nul 2>&1
taskkill /IM VDD120Switcher.exe /F >nul 2>&1
echo [OK] Autostart removed and watcher stopped.
pause
