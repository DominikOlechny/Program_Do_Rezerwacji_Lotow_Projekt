using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Do_Rezerwacji_Lotow
{
    namespace Program_Do_Rezerwacji_Lotow
    {
        // Klasa LoginPanel jest odpowiedzialna za proces autentykacji użytkownika w aplikacji.
        // Zawiera metody do logowania się do systemu i zarządzania informacjami uwierzytelniającymi.
        public class LoginPanel
        {
            private string loggedInUsername;
            //User_527,*xwR(t|A^#XM - testowy user do testowa
            //lub admin,admin
            public (bool isAuthenticated, string username) Authenticate()  // Metoda Authenticate służy do uwierzytelniania użytkownika.
                                                                           // Metoda Authenticate wykonuje proces logowania, prosząc użytkownika o podanie nazwy użytkownika i hasła.
                                                                           // Zwraca krotkę składającą się z dwóch elementów:
                                                                           // 1. isAuthenticated (bool) - flaga wskazująca, czy uwierzytelnienie się powiodło,
                                                                           // 2. username (string) - nazwa użytkownika, która została podana podczas procesu logowania.
            {
                // Wyświetl monit o wprowadzenie nazwy użytkownika i hasła.
                Console.WriteLine("Proszę się zalogować.");

                Console.Write("Nazwa użytkownika: ");    
                string username = Console.ReadLine();

                Console.Write("Hasło: ");
                string password = Console.ReadLine();
                
                return VerifyCredentialsFromCsv(username, password);    // Zwróć wynik uwierzytelnienia oraz nazwę użytkownika.
            }

            private (bool isAuthenticated, string username) VerifyCredentialsFromCsv(string username, string password)
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
            public string GetLoggedInUsername()
            {
                return loggedInUsername;
            }
        }

    }
}
