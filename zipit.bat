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
del zip_dir\_Graph\QuickGraphDoc.chm
del zip_dir\*.exe
del zip_dir\*.bat
del zip_dir\*.pdf
del zip_dir\*.pdb

powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory('zip_dir', 'zip_solution.zip'); }"

rd /S /Q zip_dir
