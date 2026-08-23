@echo off
setlocal

rem ============================================================
rem  VDD Boost - one-shot helper (v4)
rem  boost_vdd.bat            start app + boost VDDs to 120Hz + exit
rem  boost_vdd.bat i3d        same for Immersive3D
rem  boost_vdd.bat restart    force-restart the app first
rem
rem  v4: cd into the app's own directory before start - some
rem  apps (SpaceWalker is a single-file bundle that spawns
rem  helper processes) fail when started with a foreign
rem  working directory.
rem ============================================================

set "APP=%~1"
if "%APP%"=="" set "APP=sw"
set "RESTART=0"
if /i "%APP%"=="restart" set "RESTART=1" & set "APP=sw"
if /i "%~2"=="restart" set "RESTART=1"

if /i "%APP%"=="sw" goto usesw
if /i "%APP%"=="i3d" goto usei3d
echo [!] Unknown app '%APP%'. Use: boost_vdd.bat [sw^|i3d] [restart]
pause
exit /b 1

:usesw
set "APPDIR=C:\Program Files\VITURE\SpaceWalker"
set "EXE=SpaceWalker.exe"
set "PROC1=SpaceWalker.exe"
set "PROC2=SpaceWalker.Unity.exe"
goto run

:usei3d
set "APPDIR=C:\Users\<user>\AppData\Local\Programs\VITURE\Immersive3D"
set "EXE=Immersive3D.exe"
set "PROC1=Immersive3D.exe"
set "PROC2=Immersive3D.App.exe"
goto run

:run
if %RESTART%==1 (
    echo [boost] force-restart: stopping %PROC1% / %PROC2% ...
    taskkill /IM %PROC1% /F >nul 2>&1
    taskkill /IM %PROC2% /F >nul 2>&1
    timeout /t 2 /nobreak >nul
)

echo [boost] starting %PROC1% from its own directory ...
pushd "%APPDIR%"
start "" "%EXE%"
popd

rem ---- wait up to 10s for the app process to appear ----
set /a N=0
:waitproc
tasklist /FI "IMAGENAME eq %PROC1%" 2>nul | find /I "%PROC1%" >nul
if not errorlevel 1 goto up
set /a N+=1
if %N% geq 20 goto timeout
timeout /t 1 /nobreak >nul
goto waitproc

:up
echo [boost] %PROC1% is running.

"%~dp0VDDBoost.exe"
echo.
echo [boost] all virtual displays boosted. Done.
pause
exit /b 0

:timeout
echo [!] %PROC1% did not appear within 10s of starting it.
echo     Possible causes: security software blocking programmatic
echo     launches, or an app-internal crash at startup.
echo     Workaround: start the app manually once, then run this
echo     boost script again (it will skip the start step... no wait,
echo     v4 always starts - use: boost_vdd.bat restart after the app
echo     is already open is harmless - single instance).
pause
exit /b 1
