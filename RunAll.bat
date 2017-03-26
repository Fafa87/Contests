set name=rocks

call RunAsync.bat %name%00
call RunAsync.bat %name%01

call RunAsync.bat %name%02

call RunAsync.bat %name%03
call RunAsync.bat %name%04
call RunAsync.bat %name%05
call RunAsync.bat %name%06
call RunAsync.bat %name%07
call RunAsync.bat %name%08
call RunAsync.bat %name%09
call RunAsync.bat %name%10

REM ALTERNATIVE USAGE: RunAll.bat nazwa_pliku
REM exe is Deadline.exe		 +

REM for /L %%i in (1,1,10) do if exist input\%10%%i.in  Run.bat %10%%i
REM for /L %%i in (1,1,10) do if exist input\%11%%i.in  Run.bat %11%%i
