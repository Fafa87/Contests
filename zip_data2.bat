rd /S /Q zip_dir
del solution.zip

md zip_dir
copy *.* zip_dir\

xcopy _Commons zip_dir\_Commons /i /e
xcopy Clients zip_dir\Clients /i
xcopy DeadlineTest zip_dir\DeadlineTest /i /e
xcopy GUIs zip_dir\GUIs /i /e

"C:\Program Files\WinRAR\WinRAR.exe" a -afzip solution.zip zip_dir\*.cs zip_dir\*.sln zip_dir\*.csproj zip_dir\Clients zip_dir\_Commons zip_dir\DeadlineTest zip_dir\GUIs