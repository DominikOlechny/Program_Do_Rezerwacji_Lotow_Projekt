using Program_Do_Rezerwacji_Lotow.Program_Do_Rezerwacji_Lotow;

namespace Program_Do_Rezerwacji_Lotow
{
    class Program : LoginPanel
    {
        /// <summary>
        /// Klasa Program jest punktem wejścia aplikacji.
        /// </summary>
        
        static void Main(string[] args)   // Metoda Main to główna metoda uruchamiająca aplikację.
        {
            
            (bool isAuthenticated, string username) = Authenticate(); //

            if (!isAuthenticated)
            {
                Console.WriteLine("Logowanie nieudane. Program zostanie zakończony.");
                return;
            }

            Console.WriteLine($"Witamy użytkownika {username}!");
            // Dalej można wykorzystać nazwę użytkownika w logice programu
            MainMenu mainMenu = new MainMenu(username);
            mainMenu.DisplayMenu(); //wyswietlenie glownego menu
        }
    }
}