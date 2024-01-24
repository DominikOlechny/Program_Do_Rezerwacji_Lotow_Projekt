using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Do_Rezerwacji_Lotow
{
    public abstract class Listofreservations
    {
        // Ścieżka do pliku CSV i lista przechowująca rezerwacje.
        protected string csvFilePath;
        protected List<FlightSeat> flightSeats;
        protected string username;
        public Listofreservations(string username)  // Konstruktor inicjalizuje menedżera rezerwacji 
        {
            this.username = username;
            this.csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "miejsca_w_samolocie.csv");
            LoadFlightSeats();
        }


        private void LoadFlightSeats()
        {
            flightSeats = new List<FlightSeat>();

            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine("Nie znaleziono pliku CSV.");
                return;
            }

            var lines = File.ReadAllLines(csvFilePath);

            foreach (var line in lines.Skip(1)) // Skip the header
            {
                var parts = line.Split(',');
                var flightSeat = new FlightSeat
                {
                    FlightId = int.Parse(parts[0]),
                    SeatNumber = parts[1],
                    IsAvailable = parts[2] == "Available",
                    ReservedBy = parts[3],
                };
                flightSeats.Add(flightSeat);
            }
        }
    }
}
