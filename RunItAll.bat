@ECHO OFF

set name=probe
setlocal enableDelayedExpansion

for /L %%i in (0,1,10) do (
    set "n=00%%i"
    set "NAME=%name%!n:~-2!
    if exist input\!NAME!.in ( 
        echo odpalone... !NAME!
        call Run.bat !NAME!
        echo ...zrobione !NAME!
    )
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