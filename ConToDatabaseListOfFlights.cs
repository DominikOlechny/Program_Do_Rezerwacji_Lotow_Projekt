using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Do_Rezerwacji_Lotow
{
    abstract class ConToDatabaseListOfFlights
    /// <summary>
    /// Połączenie z bazą danych z pliku csv
    /// </summary>
    {
        protected List<string[]> allFields = new List<string[]>(); //lista kolumn z pliku CSV
        protected void connectdb()   /// Łączy się z bazą danych przez wczytanie danych z pliku CSV.
        {
            allFields.Clear(); // wyczyszczenie listy na poczatku, brak tego powodował problemy z wyswietlaniem/dublowanie informacji
            string currentDirectory = Directory.GetCurrentDirectory(); //Wywołanie Ścieżki do bieżącego katalogu aplikacji.
            string filePath = Path.Combine(currentDirectory, "rezerwacje_lotow_nopl.csv");    // Sciezka + Nazwa pliku

            try
            {
                // Otwarcie pliku CSV i wczytanie danych do listy allFields.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();               //Zaczytanie Danych do tablicy, kolumny odzielone są , w pliku CSV
                        string[] fields = line.Split(',');
                        allFields.Add(fields);
                    }
                } 
            }
            catch (Exception e)
            {
                // Obsługa wyjątków związanych z odczytem pliku.
                Console.WriteLine("Błąd podczas odczytu pliku:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
