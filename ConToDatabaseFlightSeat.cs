using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Do_Rezerwacji_Lotow
{
    /// <summary>
    /// Klasa abstrakcyjna służąca do zarządzania listą rezerwacji miejsc na loty.
    /// </summary>
    abstract class ConToDatabaseFlightSeat

    {
      
        // Ścieżka do pliku CSV i lista przechowująca rezerwacje.
        protected string csvFilePath; /// Ścieżka do pliku CSV zawierającego dane rezerwacji.
        protected List<FlightSeat> flightSeats; /// Lista przechowująca rezerwacje miejsc na loty.
        protected string username; /// Nazwa użytkownika zarządzającego rezerwacjami.
        protected ConToDatabaseFlightSeat(string username)  
        {
            this.username = username;
            this.csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "miejsca_w_samolocie.csv"); /// Konstruktor inicjalizujący obiekt klasy z nazwą użytkownika i wczytujący dane rezerwacji z pliku CSV.
            LoadFlightSeats();
        }


        protected void LoadFlightSeats() /// Wczytuje rezerwacje miejsc z pliku CSV do listy flightSeats.
        {
            flightSeats = new List<FlightSeat>();

            if (!File.Exists(csvFilePath)) //SPRAWDZENIE CZY istnieje plik
            {
                Console.WriteLine("Nie znaleziono pliku CSV."); //plik nie istnieje
                return;
            }

            var lines = File.ReadAllLines(csvFilePath); //odczytanie pliku, zapisanie go jako zmina lines

            foreach (var line in lines.Skip(1)) // Pominięcie nagłówka pliku CSV
            {
                var parts = line.Split(','); //odzielenie danych po przecinku, zapisanie ich jako parts
                var flightSeat = new FlightSeat
                {
                    FlightId = int.Parse(parts[0]), //numer id lotu
                    SeatNumber = parts[1], //numer siedzenia
                    IsAvailable = parts[2] == "Available", //czy miejsce jest wolne
                    ReservedBy = parts[3], //kto zarezerwował
                };
                flightSeats.Add(flightSeat); //dodanie miejsca do listy miejsc
            }
        }
    }
}
