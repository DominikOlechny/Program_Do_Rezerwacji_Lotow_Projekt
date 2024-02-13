using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Do_Rezerwacji_Lotow
{
    /// <summary>
    /// Klasa wczytująca listę zarejestrowanych userów z CSV i autotykująca logowanie
    /// </summary>
    abstract class ConToDBListofusers
    {
        private static string loggedInUsername;
        protected static (bool isAuthenticated, string username) VerifyCredentialsFromCsv(string username, string password)
        {
            string currentDirectory = Directory.GetCurrentDirectory(); // Pobranie aktualnego katalogu, w którym działa aplikacja.
            string csvFilePath = Path.Combine(currentDirectory, "login_credentials.csv");    // Utworzenie pełnej ścieżki do pliku CSV zawierającego dane uwierzytelniające.

            try
            { // Odczytanie wszystkich linii z pliku CSV.
                var lines = File.ReadAllLines(csvFilePath);
                foreach (var line in lines)
                // Przejście przez każdą linię w celu wyszukania pasujących danych uwierzytelniających.
                {
                    var parts = line.Split(',');  // Podział linii na części oddzielone przecinkami.
                    if (parts.Length == 2 && parts[0] == username && parts[1] == password)   // Sprawdzenie, czy linia zawiera dokładnie dwie części i czy pasują one do podanych danych.
                    {
                        Console.WriteLine("Logowanie pomyślne!\n");
                        // Jeśli dane są zgodne, zwrócenie wyniku pomyślnego uwierzytelnienia.
                        loggedInUsername = username;
                        return (true, username);
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Błąd podczas odczytu pliku."); // Obsługa potencjalnych błędów odczytu pliku.
            }
            // Jeśli dane uwierzytelniające nie są znalezione, informowanie o nieudanym logowaniu.
            Console.WriteLine("Nieprawidłowa nazwa użytkownika lub hasło.");
            // Zwrócenie wyniku nieudanego uwierzytelnienia.
            return (false, null);
        }
    }
}
