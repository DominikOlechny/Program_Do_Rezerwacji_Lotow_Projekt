using Program_Do_Rezerwacji_Lotow.Program_Do_Rezerwacji_Lotow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Program_Do_Rezerwacji_Lotow
{ // Klasa MainMenu reprezentuje główne menu programu i zarządza nawigacją użytkownika.
    public class MainMenu
    {
        // Instancje menedżerów odpowiedzialnych za różne aspekty aplikacji.
        private readonly FlightManager flightManager;
        private readonly ReservationManager reservationManager;
        private string username; // Add a field to store username


        public MainMenu(string username)   // Konstruktor MainMenu inicjalizuje menedżerów i inne komponenty.
        {
            this.username = username;
            flightManager = new FlightManager();
            reservationManager = new ReservationManager(username);
        }
        // Metoda DisplayMenu wyświetla menu i obsługuje interakcje użytkownika.
        public void DisplayMenu()
        {
            while (true) // Logika wyświetlania menu
            {
   
                Console.WriteLine("\nProszę wybrać opcję:");
                Console.WriteLine("1. Wyszukaj loty");
                Console.WriteLine("2. Dokonaj rezerwacji");
                Console.WriteLine("3. Anuluj rezerwację");
                Console.WriteLine("4. Zobacz listę pasażerów");
                Console.WriteLine("5. Oblicz płatność");
                Console.WriteLine("6. Wyjdź");

                Console.Write("Wprowadź swój wybór: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        flightManager.SearchFlights();
                        break;
                    case "2":
                        Console.WriteLine("Dokonaj rezerwacji");
                        reservationManager.MakeReservation();
                        break;
                    case "3":
                        Console.WriteLine("Anuluj rezerwację");
                        reservationManager.CancelReservation();
                        break;
                    case "4":
                        Console.WriteLine("Wyświetl swoje rezerwacje");
                        reservationManager.DisplayUserReservations();
                        break;
                    case "5":
                        Console.WriteLine("Oblicz płatność");
                        break;
                    case "6":
                        Console.WriteLine("Wychodzenie z systemu rezerwacji miejsc w samolotach...");
                        return;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Proszę wprowadzić numer od 1 do 6.");
                        break;
                }
            }
        }
    }
}
