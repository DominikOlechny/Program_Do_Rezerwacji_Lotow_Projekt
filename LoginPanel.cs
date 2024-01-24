namespace Program_Do_Rezerwacji_Lotow
{
    namespace Program_Do_Rezerwacji_Lotow
    {
        // Klasa LoginPanel jest odpowiedzialna za proces autentykacji użytkownika w aplikacji.
        // Zawiera metody do logowania się do systemu i zarządzania informacjami uwierzytelniającymi.
        public class LoginPanel : Listofusers
        {
            
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

           
          
        }

    }
}
