rd /S /Q zip_dir
del zip_solution.zip

md zip_dir
copy *.* zip_dir\

xcopy _Commons zip_dir\_Commons /i /E
xcopy _Graph zip_dir\_Graph /i /E
xcopy Properties zip_dir\Properties /i /E
xcopy DeadlineTest zip_dir\DeadlineTest /i /E
xcopy Clients zip_dir\Clients /i /E

@REM Remove unnecessary files
cd zip_dir
del _Graph\QuickGraphDoc.chm
del *.exe
del *.bat
del *.pdf
del *.pdb
FOR /d /r . %%d IN (bin) DO @IF EXIST "%%d" rd /s /q "%%d"
FOR /d /r . %%d IN (obj) DO @IF EXIST "%%d" rd /s /q "%%d"
cd ..

powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory('zip_dir', 'zip_solution.zip'); }"

rd /S /Q zip_dir
