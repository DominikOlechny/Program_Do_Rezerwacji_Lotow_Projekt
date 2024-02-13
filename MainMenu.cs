namespace Program_Do_Rezerwacji_Lotow
   
{ /// <summary>
///Klasa MainMenu reprezentuje główne menu programu i zarządza nawigacją użytkownika po jego zalogowaniu.
/// </summary>
    internal class MainMenu : FlightManagerMenu
    {
        // Instancje menedżerów odpowiedzialnych za różne aspekty aplikacji.
        private readonly ReservationManager reservationManager;
        private string username;


        public MainMenu(string username)   // Konstruktor MainMenu inicjalizuje menedżerów i inne komponenty.
        {
            this.username = username;
            this.reservationManager = new ReservationManager(username);
        }
        // Metoda DisplayMenu wyświetla menu i obsługuje interakcje użytkownika.
        public void DisplayMenu()
        {
            while (true) // Logika wyświetlania menu
            {

                Console.WriteLine("\nProszę wybrać opcję:"); 
                Console.WriteLine("1. Wyszukaj loty"); //OPCJA 1
                Console.WriteLine("2. Dokonaj rezerwacji"); //OPCJA 2 
                Console.WriteLine("3. Anuluj rezerwację"); //OPCJA 3
                Console.WriteLine("4. Sprawdź Swoje Rezerwacje"); //OPCJA 4
                Console.WriteLine("5. [ADMIN] Sprawdz rezerwacje innych uzytkownikow"); //OPCJA 5
                Console.WriteLine("6. [ADMIN] Sprawdz liste pasażerów lotu"); //OPCJA 6
                Console.WriteLine("7. Wyjdz"); //OPCJA 7

                Console.Write("Wprowadź swój wybór: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SearchFlights(); //Wyszukaj loty
                        break;
                    case "2":
                        Console.WriteLine("Dokonaj rezerwacji");
                        reservationManager.MakeReservation(); //Dokonaj rezerwacji
                        break;
                    case "3":
                        Console.WriteLine("Anuluj rezerwację");
                        reservationManager.CancelReservation();//Anuluj rezerwację
                        break;
                    case "4":
                        Console.WriteLine("Wyświetl swoje rezerwacje");
                        reservationManager.DisplayUserReservations();//Sprawdź Swoje Rezerwacje
                        break;
                    case "5":
                        Console.WriteLine("Sprawdz rezerwacje innych uzytkownikow");
                        reservationManager.DisplayUserReservationsExternal(); //ADMIN] Sprawdz rezerwacje innych uzytkownikow
                        break;
                         case "6":
                        Console.WriteLine("Sprawdź listę pasażerów lotu");
                        reservationManager.DisplayPassengerList();
                        break;
                    case "7":
                        Console.WriteLine("Wychodzenie z systemu rezerwacji miejsc w samolotach..."); //WYJSCIE
                        return;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Proszę wprowadzić numer od 1 do 6."); //POWRÓT PETLI MENU
                        break;
                }
            }
        }
    }
}
