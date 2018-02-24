@ECHO OFF

set name=probe
REM exe is Deadline.exe

for /L %%i in (1,1,10) do if exist input\%name%0%%i.in ( call RunAsync.bat %name%0%%i
    echo ...puszczone %name%0%%i
)
for /L %%i in (1,1,10) do if exist input\%name%1%%i.in ( call RunAsync.bat %name%1%%i
    echo ...puszczone %name%1%%i
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