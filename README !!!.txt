Program "Przychodnia" posiada baz� danych uzupe�nion� o dane niezb�dn� do zalogowania si� oraz przyk�adowe osoby. Aby poprawnie uruchomi� plik .exe programu znajduj�cy si� w Przychodnia\Przychodnia\bin\Debug\Przychodnia.exe trzeba zmieni� �cie�k� dost�pu do bazy danych w nast�puj�cych plikach:
- Page_pacjenci.xaml.cs - 28 linia kodu
- Page_notatki.xaml.cs - 36 linia kodu
- Page_kalendarz.xaml.cs - 39 linia kodu
- Page_dodawanie_pacjenta.xaml.cs - 27 linia kodu
- Logowanie.xaml.cs - 27 linia kodu

Po wykonaniu powy�szych zmian mo�na sie zalogowa� do programu i korzysta� ze wszystkich jego funkcjonalno�ci.

Dane potrzebne do logowania:
login: admin
has�o: admin
