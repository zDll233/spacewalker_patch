@echo off
setlocal

rem ============================================================
rem  SpaceWalker 120Hz Project - restore script (v6)
rem
rem  Restores the original exe. Uses the path recorded in
rem  swpath.txt (written by install_sw.bat); falls back to the
rem  default install path if the config is missing.
rem ============================================================

set "CONFIG=%~dp0swpath.txt"

set "DST="
if exist "%CONFIG%" (
    set /p DST=<"%CONFIG%"
)
if "%DST%"=="" set "DST=C:\Program Files\VITURE\SpaceWalker\SpaceWalker.exe"
if not exist "%DST%" (
    echo [!] Target not found: %DST%
    pause
    exit /b 1
)

echo ============================================
echo  SpaceWalker - restore original exe
echo  Target: %DST%
echo ============================================
echo.

net session >nul 2>&1
if not %errorlevel%==0 goto needadmin

tasklist /FI "IMAGENAME eq SpaceWalker.exe" 2>nul | find /I "SpaceWalker.exe" >nul
set "P1=%errorlevel%"
tasklist /FI "IMAGENAME eq SpaceWalker.Unity.exe" 2>nul | find /I "SpaceWalker.Unity.exe" >nul
set "P2=%errorlevel%"
if %P1%==0 goto running
if %P2%==0 goto running

if not exist "%DST%.orig" goto nobackup

copy /Y "%DST%.orig" "%DST%" >nul
fc /b "%DST%.orig" "%DST%" >nul
if not errorlevel 1 goto restored

echo [FAIL] Restore copy mismatch. Make sure SpaceWalker is fully closed, then retry.
pause
exit /b 1

:restored
echo [OK] Original exe restored and verified.
echo      You may now delete %DST%.orig
pause
exit /b 0

:needadmin
echo [!] This script needs administrator rights.
echo     Right-click this file and choose "Run as administrator", then retry.
pause
exit /b 1

:nobackup
echo [!] No backup found at %DST%.orig
pause
exit /b 1

:running
echo [!] SpaceWalker is still running - the exe is locked.
echo     Close it fully (check tray), then run this script again.
pause
exit /b 1