namespace Program_Do_Rezerwacji_Lotow
{
    /// <summary>
    /// Klasa Connectdb zarządza połączeniem z bazą danych, która jest reprezentowana przez plik CSV.
    /// Dziedziczy po klasie Listoflights, wykorzystuje dane lotów.
    /// </summary>
    internal class UseDatabaseListOfFlights : ConToDatabaseListOfFlights 
    {
        
        // Lista przechowująca wszystkie rekordy z pliku CSV listy lotow.

        /// Metoda writebase służy do wyświetlania zawartości bazy danych w konsoli.
        
        protected void writebase() /// Jest to podstawowa funkcjonalność diagnostyczna, pozwalająca na szybką weryfikację obecnych danych lotów.
        {
            // Implementacja metody.

            Console.WriteLine("ID Rezerwacji | Miejsce Odlotu | Miejsce Przylotu | Data Odlotu | Data Przylotu | Ilość KM"); // Wyświetlanie nagłówków dla każdej kolumny.
            Console.WriteLine(new String('-', 80)); // Linia oddzielająca

            if (allFields.Count == 0)// Sprawdzenie czy lista danych nie jest pusta.
            {
                Console.WriteLine("Brak danych do wyświetlenia."); // Informacja dla użytkownika, gdy lista jest pusta.
                return;
            }


            // Wyświetlenie danych z listy.
            foreach (var fields in allFields.Skip(1)) // Iteracja przez wszystkie rekordy i ich wyświetlanie.
            {
                if (fields.Length >= 6)
                {
                   
                    Console.WriteLine(String.Format("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}",
                        fields[0], fields[1], fields[2], fields[3], fields[4], fields[5]));
                }
                else
                {
                    Console.WriteLine("Niekompletny rekord: " + String.Join(" | ", fields));
                }
                

            }

            Console.WriteLine("Nacisnij dowolny klawisz, aby kontynułowac.....");
            Console.ReadKey();
        }
        protected void searchbyid(string id)  // Wyszukuje w bazie danych rekordy o podanym ID rezerwacji.
        {
            
            bool found = false;

            foreach (var fields in allFields.Skip(1)) // Iteracja przez listę w poszukiwaniu rekordów o podanym ID.
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

        protected void flighof(string odlot) // Wyszukuje w bazie danych rekordy na podstawie miejsca odlotu.
        {
           
            bool found = false;

            foreach (var fields in allFields.Skip(1))// Iteracja przez wszystkie rekordy w poszukiwaniu odlotu.
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

        protected void flighton(string cel) // Wyszukuje w bazie danych rekordy na podstawie miejsca przylotu.
        {
          
            bool found = false;

            foreach (var fields in allFields.Skip(1)) // Iteracja przez wszystkie rekordy w poszukiwaniu przylotu.
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
