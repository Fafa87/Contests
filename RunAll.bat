@ECHO OFF
REM USAGE: RunAll.bat zad1 test
REM exe is Deadline.exe

set input=input
set katalog=%input%\%1
set nazwa=%2
set startTime=%time%
set exe=Deadline.exe

echo %katalog%

for /L %%i in (1,1,220) do if exist %katalog%\%nazwa%0%%i.in  %exe% < %katalog%\%nazwa%0%%i.in > %katalog%\%nazwa%0%%i.out

for /L %%i in (1,1,220) do if exist %katalog%\%nazwa%%%i.in  %exe% < %katalog%\%nazwa%%%i.in > %katalog%\%nazwa%%%i.out

REM %exe% < %katalog%\busy_day.in > %katalog%\busy_day.out

echo Start Time: %startTime% 
echo Finish Time: %time%