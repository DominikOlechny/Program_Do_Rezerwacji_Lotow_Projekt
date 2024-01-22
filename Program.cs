using Program_Do_Rezerwacji_Lotow;
using Program_Do_Rezerwacji_Lotow.Program_Do_Rezerwacji_Lotow;
using System;

namespace Program_Do_Rezerwacji_Lotow
{
    class Program // Klasa Program jest punktem wejścia aplikacji.
    {
        static void Main(string[] args)   // Metoda Main to główna metoda uruchamiająca aplikację.
        {
            LoginPanel loginPanel = new LoginPanel();
            (bool isAuthenticated, string username) = loginPanel.Authenticate();
        
                if (!isAuthenticated)
            {
                Console.WriteLine("Logowanie nieudane. Program zostanie zakończony.");
                return;
            }

            Console.WriteLine($"Witamy użytkownika {username}!");
            // Dalej można wykorzystać nazwę użytkownika w logice programu
            MainMenu mainMenu = new MainMenu(username);
            mainMenu.DisplayMenu();
        }
    }
}