Program "Przychodnia" posiada bazê danych uzupe³nion¹ o dane niezbêdnê do zalogowania siê oraz przyk³adowe osoby. Aby poprawnie uruchomiæ plik .exe programu znajduj¹cy siê w Przychodnia\Przychodnia\bin\Debug\Przychodnia.exe trzeba zmieniæ œcie¿kê dostêpu do bazy danych w nastêpuj¹cych plikach:
- Page_pacjenci.xaml.cs - 28 linia kodu
- Page_notatki.xaml.cs - 36 linia kodu
- Page_kalendarz.xaml.cs - 39 linia kodu
- Page_dodawanie_pacjenta.xaml.cs - 27 linia kodu
- Logowanie.xaml.cs - 27 linia kodu

Po wykonaniu powy¿szych zmian mo¿na sie zalogowaæ do programu i korzystaæ ze wszystkich jego funkcjonalnoœci.

Dane potrzebne do logowania:
login: admin
has³o: admin
