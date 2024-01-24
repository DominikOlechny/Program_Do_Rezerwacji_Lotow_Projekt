using Program_Do_Rezerwacji_Lotow.Program_Do_Rezerwacji_Lotow;

namespace Program_Do_Rezerwacji_Lotow
{
    public class ReservationManager : Listofreservations // Klasa ReservationManager zarządza rezerwacjami miejsc na loty.
    {
        public ReservationManager(string username) : base(username)
        {
        }
        public void MakeReservation()
        {
            Console.WriteLine("Wybierz ID lotu:");
            int flightId = int.Parse(Console.ReadLine());

            var availableSeats = flightSeats.Where(f => f.FlightId == flightId && f.IsAvailable).ToList();

            if (!availableSeats.Any())
            {
                Console.WriteLine("Brak dostępnych miejsc na ten lot.");
                return;
            }

            Console.WriteLine("Dostępne miejsca:");
            foreach (var seat in availableSeats)
            {
                Console.WriteLine($"Miejsce numer: {seat.SeatNumber}");
            }

            Console.WriteLine("Wybierz numer miejsca:");
            string seatNumber = Console.ReadLine();

            var selectedSeat = availableSeats.FirstOrDefault(s => s.SeatNumber == seatNumber);
            if (selectedSeat == null)
            {
                Console.WriteLine("Nieprawidłowy numer miejsca.");
                return;
            }

            // Pobieranie klasy miejsca na podstawie numeru miejsca
            char seatClass = selectedSeat.SeatNumber[1]; // Zakładając, że klasa jest pierwszym znakiem numeru miejsca
            selectedSeat.SeatClass = seatClass;

            // Pytanie o ubezpieczenie
            Console.WriteLine("Czy chcesz dodać ubezpieczenie? (tak/nie)");
            selectedSeat.HasInsurance = Console.ReadLine().Trim().ToLower() == "tak";

            // Pytanie o nadwagowy bagaż
            Console.WriteLine("Czy masz nadwagowy bagaż? (tak/nie)");
            selectedSeat.HasExtraLuggage = Console.ReadLine().Trim().ToLower() == "tak";

            // Obliczanie kosztów
            decimal cost = CalculateReservationCost(seatClass, selectedSeat.HasInsurance, selectedSeat.HasExtraLuggage);
            Console.WriteLine($"Koszt rezerwacji: {cost} PLN");

            selectedSeat.IsAvailable = false;
            selectedSeat.ReservedBy = this.username; // Rezerwacja miejsca przez zalogowanego użytkownika

            UpdateCsvFile();
            Console.WriteLine($"Rezerwacja miejsca {seatNumber} na lot ID {flightId} została dokonana przez {this.username}.");
        }
        private decimal CalculateReservationCost(char seatClass, bool hasInsurance, bool hasExtraLuggage)
        {
            decimal baseCost = seatClass switch
            {
                'A' => 200m, // Przykładowe ceny dla różnych klas
                'B' => 150m,
                'C' => 100m,
                'D' => 50m,
               _ => 0m,
            }; ;

            decimal insuranceCost = hasInsurance ? 50m : 0m; // Dodatkowa opłata za ubezpieczenie
            decimal luggageCost = hasExtraLuggage ? 25m : 0m; // Dodatkowa opłata za nadwagowy bagaż
            decimal totalcost = baseCost + insuranceCost + luggageCost;
            return baseCost + insuranceCost + luggageCost;
        }
        public void CancelReservation()
        {
            Console.WriteLine("Wybierz ID lotu, dla którego chcesz anulować rezerwację:");
            int flightId = int.Parse(Console.ReadLine());

            var userReservedSeats = flightSeats.Where(f => f.FlightId == flightId && !f.IsAvailable && f.ReservedBy == this.username).ToList();

            if (!userReservedSeats.Any())
            {
                Console.WriteLine("Nie masz żadnych rezerwacji na ten lot.");
                return;
            }

            Console.WriteLine("Twoje zarezerwowane miejsca:");
            foreach (var seat in userReservedSeats)
            {
                Console.WriteLine($"Miejsce numer: {seat.SeatNumber}");
            }

            Console.WriteLine("Wybierz numer miejsca do anulowania:");
            string seatNumber = Console.ReadLine();

            var selectedSeat = userReservedSeats.FirstOrDefault(s => s.SeatNumber == seatNumber);
            if (selectedSeat == null)
            {
                Console.WriteLine("Nieprawidłowy numer miejsca lub nie masz rezerwacji na to miejsce.");
                return;
            }

            selectedSeat.IsAvailable = true;
            selectedSeat.ReservedBy = null; // Usunięcie nazwy użytkownika z rezerwacji
            UpdateCsvFile();
            Console.WriteLine($"Rezerwacja miejsca {seatNumber} na lot ID {flightId} została anulowana.");
        }

        public void DisplayUserReservations()
        {
            var userReservations = flightSeats
                .Where(f => f.ReservedBy == this.username && !f.IsAvailable)
                .ToList();

            if (!userReservations.Any())
            {
                Console.WriteLine("Nie masz żadnych rezerwacji.");
                return;
            }

            Console.WriteLine($"Rezerwacje dla użytkownika" + username);
            foreach (var reservation in userReservations)
            {
                Console.WriteLine($"Lot ID: {reservation.FlightId}, Miejsce: {reservation.SeatNumber}");
            }
        }

        private void UpdateCsvFile()
        {
            var lines = new List<string> { "FlightId,SeatNumber,Availability,ReservedBy" }; // Dodano nagłówek dla ReservedBy
            lines.AddRange(flightSeats.Select(f =>
        $"{f.FlightId}," +
        $"{f.SeatNumber}," +
        $"{(f.IsAvailable ? "Available" : "Occupied")}," +
        $"{f.ReservedBy}," +
        $"{(f.HasInsurance ? "Yes" : "No")}," +
        $"{(f.HasExtraLuggage ? "Yes" : "No")}"
    ));
            File.WriteAllLines(csvFilePath, lines);
        } 
    }



    
}