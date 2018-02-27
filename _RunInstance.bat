@SET EXE=Deadline.exe
@SET OUT=bin\Release
@SET RUNNAME=%1
@SET HOST=%2
@SET PORT=%3 

rd /S /Q Run\%RUNNAME%

xcopy /Y /I /E %OUT% Run\%RUNNAME%

@start "%RUNNAME%" Run\%RUNNAME%\%EXE% %HOST% %PORT%
