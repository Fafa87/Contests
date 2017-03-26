rd /S /Q zip_dir
del zip_solution.zip

md zip_dir
copy *.* zip_dir\

xcopy DeadlineTest zip_dir\DeadlineTest /i /E
xcopy _Commons zip_dir\_Commons /i /E
xcopy Clients zip_dir\Clients /i /E
xcopy GUIs zip_dir\GUIs /i /E

powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory('zip_dir', 'zip_solution.zip'); }"

rd /S /Q zip_dir