REM %1 is exe path
call "C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\vcvarsall.bat" x86
editbin /stack:100000000 %1