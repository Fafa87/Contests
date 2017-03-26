REM %1 is exe path
call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\bin\vcvars32.bat"
editbin /stack:100000000 %1