Poziomy u�ycia:
TCP/IP (nie przetestowane)
	- Client po�rednicz�cy mi�dzy serwerem, a programem (Solverem)
	- pomocnicze metody: IsAccepted, ParseToken, 
	- p�tla main, program robi NextTurn (bez wait!!)
	
Uruchamianie programu:
	- zdefiniowanie parametr�w po��czenia w _Run1(2,3..).bat
	- odpalanie _Run1(2,3..).bat powoduje skopiowanie outputa programu (aby odpalenie by�o niezale�ne od dalszych prac) i odpalenie podanego exeka

Wizualizacja
	- osobny program WinForms
	- SolverUI : Solver implementuje metod� prezentuj�c� aktualny stan
	- jest pomocnicza klasa Grid do obs�ugi plansz
	- rysowanie odbywa si� w w�tku GUI (na timer), a uruchomienie programu (praktycznie takie jak bez UI) w osobnym w�tku (UWAGA! Bez �adnej synchronizacji, wi�c trzeba uwa�a�)
	
Testowanie programu offline
	- osobny projekt konsolowy, aby sprawdzi� czy si� nie wypieprza mocno
	- interfejs IServer zawieraj�cy wszystkie metody opisuj�ce komunikacje z serwerem
	- ClientMock implementuje IServer
	- Solver musi wtedy korzysta� z tylko tego interfejsu
	- osobny main w kt�rym dwie instacje Solvera "graj�" ze sob� (tzn wszystko zale�y od skomplikowania ClientMock). Dzia�a to sekwencyjnie (najpierw obs�ugiwany jest pierwszy potem drugi Solver, Wait w takiej sytuacji nie robi nic.
	
Testowanie programu offline z UI
	- tak jak we wcze�niejszej wizualizacji z tym, �e main pochodzi z "Testowanie programu offline"

PRZYK�AD:
	W katalogu Example znajduje si� przyk�ad wykorzystania (oczywi�cie tylko wersja OFFLINE).