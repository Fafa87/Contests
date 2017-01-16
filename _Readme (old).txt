Poziomy u¿ycia:
TCP/IP (nie przetestowane)
	- Client poœrednicz¹cy miêdzy serwerem, a programem (Solverem)
	- pomocnicze metody: IsAccepted, ParseToken, 
	- pêtla main, program robi NextTurn (bez wait!!)
	
Uruchamianie programu:
	- zdefiniowanie parametrów po³¹czenia w _Run1(2,3..).bat
	- odpalanie _Run1(2,3..).bat powoduje skopiowanie outputa programu (aby odpalenie by³o niezale¿ne od dalszych prac) i odpalenie podanego exeka

Wizualizacja
	- osobny program WinForms
	- SolverUI : Solver implementuje metodê prezentuj¹c¹ aktualny stan
	- jest pomocnicza klasa Grid do obs³ugi plansz
	- rysowanie odbywa siê w w¹tku GUI (na timer), a uruchomienie programu (praktycznie takie jak bez UI) w osobnym w¹tku (UWAGA! Bez ¿adnej synchronizacji, wiêc trzeba uwa¿aæ)
	
Testowanie programu offline
	- osobny projekt konsolowy, aby sprawdziæ czy siê nie wypieprza mocno
	- interfejs IServer zawieraj¹cy wszystkie metody opisuj¹ce komunikacje z serwerem
	- ClientMock implementuje IServer
	- Solver musi wtedy korzystaæ z tylko tego interfejsu
	- osobny main w którym dwie instacje Solvera "graj¹" ze sob¹ (tzn wszystko zale¿y od skomplikowania ClientMock). Dzia³a to sekwencyjnie (najpierw obs³ugiwany jest pierwszy potem drugi Solver, Wait w takiej sytuacji nie robi nic.
	
Testowanie programu offline z UI
	- tak jak we wczeœniejszej wizualizacji z tym, ¿e main pochodzi z "Testowanie programu offline"

PRZYK£AD:
	W katalogu Example znajduje siê przyk³ad wykorzystania (oczywiœcie tylko wersja OFFLINE).