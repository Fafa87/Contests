@ECHO OFF

set name=probe
setlocal enableDelayedExpansion

for /L %%i in (0,1,10) do (
    set "n=00%%i"
    set "NAME=%name%!n:~-2!
    if exist input\!NAME!.in ( call RunAsync.bat !NAME!
        echo ...puszczone !NAME!
    )
)

REM call RunAsync.bat %name%00
REM call RunAsync.bat %name%01
REM call RunAsync.bat %name%02
REM call RunAsync.bat %name%03
REM call RunAsync.bat %name%04
REM call RunAsync.bat %name%05
REM call RunAsync.bat %name%06
REM call RunAsync.bat %name%07
REM call RunAsync.bat %name%08
REM call RunAsync.bat %name%09
REM call RunAsync.bat %name%10

pause