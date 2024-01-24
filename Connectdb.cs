namespace Program_Do_Rezerwacji_Lotow
{
    public abstract class Connectdb : Listoflights // Klasa Connectdb zarządza połączeniem z bazą danych, która jest reprezentowana przez plik CSV.
    {

        // Lista przechowująca wszystkie rekordy z pliku CSV.



        public void wypiszbaze() // Wyświetla bazę danych w konsoli.

        {
            connectdb(); //połączenie z bazą danych zostało nawiązane.
            if (allFields.Count == 0)// Sprawdzenie czy lista danych nie jest pusta.
            {
                Console.WriteLine("Brak danych do wyświetlenia.");
                return;
            }


            Console.WriteLine("ID Rezerwacji | Miejsce Odlotu | Miejsce Przylotu | Data Odlotu | Data Przylotu | Ilość KM"); // Wyświetlanie nagłówków dla każdej kolumny.
            Console.WriteLine(new String('-', 80)); // Linia oddzielająca

            foreach (var fields in allFields) // Iteracja przez wszystkie rekordy i ich wyświetlanie.
            {
                if (fields.Length >= 6)
                {
                    string formattedLine = String.Format("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}",
                        fields[0], fields[1], fields[2], fields[3], fields[4], fields[5]);
                    Console.WriteLine(formattedLine);
                }
                else
                {
                    Console.WriteLine("Niekompletny rekord: " + String.Join(" | ", fields));
                }

            }
            Console.WriteLine("Nacisnij dowolny klawisz, aby kontynułowac.....");
            Console.ReadKey();
        }
        public void wyszukajpoid(string id)  // Wyszukuje w bazie danych rekordy o podanym ID rezerwacji.
        {
            connectdb(); // Wczytanie danych
            bool found = false;

            foreach (var fields in allFields) // Iteracja przez listę w poszukiwaniu rekordów o podanym ID.
            {
                if (fields.Length > 0 && fields[0] == id)
                {
                    if (!found)
                    {
                        // Wyświetlanie nagłówków kolumn tylko raz
                        Console.WriteLine("Znaleziono rekord:");
                        Console.WriteLine("ID Rezerwacji | Miejsce Odlotu | Miejsce Przylotu | Data Odlotu | Data Przylotu | Ilość KM");
                        Console.WriteLine(new String('-', 80)); // Linia oddzielająca
                        found = true;
                    }

                    string formattedLine = String.Format("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}",
                        fields[0], fields[1], fields[2], fields[3], fields[4], fields[5]); // Formatowanie i wyświetlenie znalezionego rekordu.
                    Console.WriteLine(formattedLine);
                }

            }

            if (!found)     // Jeśli żaden rekord nie został znaleziony, wyświetl odpowiedni komunikat.
            {
                Console.WriteLine("Nie znaleziono rekordu o ID: " + id);
            }
            Console.WriteLine("Nacisnij dowolny klawisz, aby kontynułowac.....");
            Console.ReadKey();
        }

        public void wyszukajpoodlotach(string odlot) // Wyszukuje w bazie danych rekordy na podstawie miejsca odlotu.
        {
            connectdb(); //dane zostały wczytane
            bool found = false;

            foreach (var fields in allFields)// Iteracja przez wszystkie rekordy w poszukiwaniu odlotu.
            {
                if (fields.Length > 0 && fields[1] == odlot)
                {
                    if (!found)
                    {
                        // Wyświetlanie nagłówków kolumn tylko raz
                        Console.WriteLine($"Znaleziono rekordy dla miejsca odlotu: {odlot}");
                        Console.WriteLine("ID Rezerwacji | Miejsce Odlotu | Miejsce Przylotu | Data Odlotu | Data Przylotu | Ilość KM");
                        Console.WriteLine(new String('-', 80)); // Linia oddzielająca
                        found = true;
                    }

                    string formattedLine = String.Format("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}",   // Formatowanie i wyświetlenie znalezionego rekordu.
                        fields[0], fields[1], fields[2], fields[3], fields[4], fields[5]);
                    Console.WriteLine(formattedLine);
                }
            }

            if (!found)  // Jeśli nie znaleziono pasujących rekordów, wyświetl informację.
            {
                Console.WriteLine("Nie znaleziono rekordów dla miejsca odlotu: " + odlot);
            }
            Console.WriteLine("Nacisnij dowolny klawisz, aby kontynułowac.....");
            Console.ReadKey();
        }

        public void wyszukajpodpcelowych(string cel) // Wyszukuje w bazie danych rekordy na podstawie miejsca przylotu.
        {
            connectdb(); // dane zostały wczytane
            bool found = false;

            foreach (var fields in allFields) // Iteracja przez wszystkie rekordy w poszukiwaniu przylotu.
            {
                if (fields.Length > 0 && fields[2] == cel)
                {
                    if (!found)
                    {
                        // Wyświetlanie nagłówków kolumn tylko raz
                        Console.WriteLine($"Znaleziono rekordy dla miejsca przylotu: {cel}");
                        Console.WriteLine("ID Rezerwacji | Miejsce Odlotu | Miejsce Przylotu | Data Odlotu | Data Przylotu | Ilość KM");
                        Console.WriteLine(new String('-', 80)); // Linia oddzielająca
                        found = true;
                    }

                    string formattedLine = String.Format("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}",
                        fields[0], fields[1], fields[2], fields[3], fields[4], fields[5]);  // Formatowanie i wyświetlenie znalezionego rekordu
                    Console.WriteLine(formattedLine);
                }
            }

            if (!found) // Jeśli nie znaleziono pasujących rekordów, wyświetl informację
            {
                Console.WriteLine("Nie znaleziono rekordów dla miejsca przylotu: " + cel);
            }
            Console.WriteLine("Nacisnij dowolny klawisz, aby kontynułowac.....");
            Console.ReadKey();
        }



    }
}
