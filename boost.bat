@echo off
setlocal

rem ============================================================
rem  SpaceWalker 120Hz Project - boost script (v8)
rem
rem  Reads the SpaceWalker path from swpath.txt (written by
rem  install_sw.bat - run that first if this file is missing).
rem  If SpaceWalker is already running it is NOT started again;
rem  otherwise it is auto-started. Then VDDBoost.ps1 waits for
rem  the virtual displays and boosts them all to 120Hz, exits.
rem
rem  boost.bat             normal
rem  boost.bat forget      clear the recorded path
rem ============================================================

set "CONFIG=%~dp0swpath.txt"
set "FORGET=0"
if /i "%~1"=="forget"  set "FORGET=1"

if %FORGET%==1 (
    del "%CONFIG%" >nul 2>&1
    echo [boost] recorded path cleared.
    echo         Run install_sw.bat to configure it again.
    pause
    exit /b 0
)

if not exist "%CONFIG%" goto noconfig

set "SWPATH="
set /p SWPATH=<"%CONFIG%"
if not exist "%SWPATH%" goto noconfig

rem --- start SpaceWalker only if it is not already running ---
tasklist /FI "IMAGENAME eq SpaceWalker.exe" 2>nul | find /I "SpaceWalker.exe" >nul
if not errorlevel 1 goto boost

echo [boost] starting SpaceWalker from its own directory ...
for %%I in ("%SWPATH%") do set "SWDIR=%%~dpI"
pushd "%SWDIR%"
start "" "%SWPATH%"
popd

set /a N=0
:waitproc
tasklist /FI "IMAGENAME eq SpaceWalker.exe" 2>nul | find /I "SpaceWalker.exe" >nul
if not errorlevel 1 goto up
set /a N+=1
if %N% geq 20 goto timeout
timeout /t 1 /nobreak >nul
goto waitproc

:up
echo [boost] SpaceWalker is running.

:boost
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0VDDBoost.ps1"
set "RC=%errorlevel%"
echo.
if %RC%==0 (
    echo [boost] finished - virtual displays boosted to 120Hz
    echo         per-display results are printed above.
) else if %RC%==2 (
    echo [!] Some virtual displays could NOT be switched to 120Hz.
    echo     See the per-display results above, then run boost.bat again.
) else if %RC%==3 (
    echo [!] No virtual display appeared within 120s - nothing boosted.
    echo     Check: glasses connected over USB-C? A virtual-screen
    echo     layout active inside SpaceWalker? Then run boost.bat again.
) else if %RC%==4 (
    echo [!] Failed to start SpaceWalker - nothing boosted.
) else if %RC%==5 (
    echo [!] SpaceWalker path not found. Run install_sw.bat first.
) else (
    echo [!] Unexpected exit code: %RC%
)
pause
exit /b %RC%

:noconfig
echo [!] No usable SpaceWalker path recorded (swpath.txt missing or invalid).
echo     Please run install_sw.bat first - it detects the default
echo     install path (or asks you) and saves it for all scripts.
pause
exit /b 1

:timeout
echo [!] SpaceWalker did not appear within 10s.
echo     If a window opened anyway, everything is fine - run
echo     boost.bat once more. If not, check security software
echo     blocking script-started programs, then try again.
pause
exit /b 1