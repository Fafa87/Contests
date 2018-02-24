@ECHO OFF

set name=probe
REM exe is Deadline.exe

for /L %%i in (1,1,10) do if exist input\%name%0%%i.in ( call Run.bat %name%0%%i
    echo ...zrobione %name%0%%i
)
for /L %%i in (1,1,10) do if exist input\%name%1%%i.in ( call Run.bat %name%1%%i
    echo ...zrobione %name%1%%i
)

REM call Run.bat %name%00
REM call Run.bat %name%01
REM call Run.bat %name%02
REM call Run.bat %name%03
REM call Run.bat %name%04
REM call Run.bat %name%05
REM call Run.bat %name%06
REM call Run.bat %name%07
REM call Run.bat %name%08
REM call Run.bat %name%09
REM call Run.bat %name%10

pause