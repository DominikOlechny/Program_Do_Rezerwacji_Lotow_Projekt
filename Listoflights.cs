using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Do_Rezerwacji_Lotow
{
    public abstract class Listoflights
    {
        public List<string[]> allFields = new List<string[]>();
        public void connectdb()   /// Łączy się z bazą danych przez wczytanie danych z pliku CSV.
        {
            string currentDirectory = Directory.GetCurrentDirectory(); // Ścieżka do bieżącego katalogu aplikacji.
            string filePath = Path.Combine(currentDirectory, "rezerwacje_lotow_nopl.csv");    // Pełna ścieżka do pliku z rezerwacjami lotów

            try
            {
                // Otwarcie pliku CSV i wczytanie danych do listy allFields.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
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
