@echo off
setlocal

rem ============================================================
rem  SpaceWalker 120Hz Project - install script (v6)
rem
rem  Resolves the SpaceWalker path (recorded config -> default
rem  install path -> ask the user), saves it to swpath.txt for
rem  all other scripts (boost.bat / restore_sw.bat / check), then
rem  installs the patched exe (auto backup + byte verify).
rem
rem  Run as administrator (right-click -> Run as administrator).
rem  Close SpaceWalker first.
rem ============================================================

set "CONFIG=%~dp0swpath.txt"
set "SRC=%~dp0SpaceWalker.exe.patched"

rem ---------- 1) resolve SpaceWalker path ----------
if exist "%CONFIG%" (
    set "SWPATH="
    set /p SWPATH=<"%CONFIG%"
    if exist "%SWPATH%" goto havepath
    echo [!] Recorded path is no longer valid, asking again.
    del "%CONFIG%" >nul 2>&1
)

if exist "C:\Program Files\VITURE\SpaceWalker\SpaceWalker.exe" (
    set "SWPATH=C:\Program Files\VITURE\SpaceWalker\SpaceWalker.exe"
    goto havepath
)

echo.
echo [!] SpaceWalker.exe was not found at the default location:
echo     C:\Program Files\VITURE\SpaceWalker\SpaceWalker.exe
echo     Enter the full path of your SpaceWalker.exe below.
echo.
set "SWPATH="
set /p SWPATH=Path:
if "%SWPATH%"=="" (
    echo [!] Empty path - aborting.
    pause
    exit /b 1
)
if not exist "%SWPATH%" (
    echo [!] File not found: %SWPATH%
    pause
    exit /b 1
)

:havepath
>"%CONFIG%" echo %SWPATH%
set "DST=%SWPATH%"
echo [OK] SpaceWalker.exe: %SWPATH%  (saved to swpath.txt)
echo.

rem ---------- 2) admin check ----------
net session >nul 2>&1
if not %errorlevel%==0 goto needadmin

if not exist "%SRC%" goto nosrc
if not exist "%DST%" goto nodst

tasklist /FI "IMAGENAME eq SpaceWalker.exe" 2>nul | find /I "SpaceWalker.exe" >nul
set "P1=%errorlevel%"
tasklist /FI "IMAGENAME eq SpaceWalker.Unity.exe" 2>nul | find /I "SpaceWalker.Unity.exe" >nul
set "P2=%errorlevel%"
if %P1%==0 goto running
if %P2%==0 goto running

if not exist "%DST%.orig" (
    copy /Y "%DST%" "%DST%.orig" >nul
)
if not exist "%DST%.orig" goto backupfail
echo [OK] Backup: %DST%.orig

copy /Y "%SRC%" "%DST%" >nul
fc /b "%SRC%" "%DST%" >nul
if not errorlevel 1 goto installed

echo [FAIL] Copy mismatch - patched exe was not installed correctly.
echo        Make sure SpaceWalker is fully closed (check tray), then retry.
pause
exit /b 1

:installed
echo.
echo ============================================
echo  PATCH INSTALLED (byte-identical copy verified)
echo  Glasses window mode now 120Hz.
echo  Daily use: boost.bat  (powerspacewalker + boosts
echo  virtual displays to 120Hz, then exits)
echo  Rollback:  restore_sw.bat
echo ============================================
pause
exit /b 0

:needadmin
echo [!] This script needs administrator rights.
echo     Right-click this file and choose "Run as administrator", then retry.
pause
exit /b 1

:nosrc
echo [!] Patched exe not found next to this script:
echo     %SRC%
pause
exit /b 1

:nodst
echo [!] Target not found: %DST%
pause
exit /b 1

:running
echo [!] SpaceWalker is still running - the exe is locked.
echo     Close it fully (check system tray), then run this script again.
pause
exit /b 1

:backupfail
echo [!] Could not create backup %DST%.orig
pause
exit /b 1