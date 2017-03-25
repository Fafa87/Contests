@SET EXE=GUIs.exe
@SET OUT=GUIs\bin\Release
@SET RUNNAME=%1
@SET HOST=%2
@SET PORT=%3 

rd /S /Q Run\UI\%RUNNAME%

xcopy /I /E %OUT% Run\UI\%RUNNAME%

@start "%RUNNAME%" Run\UI\%RUNNAME%\%EXE% %HOST% %PORT%
