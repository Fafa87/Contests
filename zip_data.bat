rd /S /Q zip_dir
del zip_solution.zip

md zip_dir
copy *.* zip_dir\

xcopy _Commons zip_dir\_Commons /i
xcopy Clients zip_dir\Clients /i
xcopy GUIs zip_dir\GUIs /i

powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory('zip_dir', 'zip_solution.zip'); }"

rd /S /Q zip_dir