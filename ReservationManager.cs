using Program_Do_Rezerwacji_Lotow.Program_Do_Rezerwacji_Lotow;

namespace Program_Do_Rezerwacji_Lotow
{
    /// <summary>
    /// Klasa ReservationManager służy do zarządzania rezerwacjami miejsc na loty.
    /// Umożliwia tworzenie, anulowanie i wyświetlanie rezerwacji dla zalogowanego użytkownika.
    /// </summary>

    class ReservationManager : ConToDatabaseFlightSeat // Klasa ReservationManager zarządza rezerwacjami miejsc na loty.
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy ReservationManager z podaną nazwą użytkownika.
        /// </summary>
        /// 


        

        public ReservationManager(string username) : base(username)///Nazwa użytkownika zarządzającego rezerwacjami
        {
        }

        /// <summary>
        /// Pozwala użytkownikowi na dokonanie rezerwacji miejsca na lot.
        /// Procedura obejmuje wybór lotu, miejsca, dodatków i obliczenie kosztu rezerwacji.
        /// </summary>
        public void MakeReservation() //Wykonywanie rezerwacji
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
            Console.WriteLine("\nCennik:");
            Console.WriteLine("\nKlasa A - 200 ZL \nKlasa B - 150 ZL \nKlasa C - 100 ZL \nKlasa D - 50 ZL \n");
            Console.Write("Wybierz numer miejsca: ");
            string seatNumber = Console.ReadLine();

            var selectedSeat = availableSeats.FirstOrDefault(s => s.SeatNumber == seatNumber);
            if (selectedSeat == null) //sprawdzenie czy miejsce instenieje 
            {
                Console.WriteLine("Nieprawidłowy numer miejsca.");
                return;
            }

            // Pobieranie klasy miejsca na podstawie numeru miejsca
            char seatClass = selectedSeat.SeatNumber[1]; //klasa jest drugim znakiem numeru miejsca
            selectedSeat.SeatClass = seatClass;

            // Pytanie o ubezpieczenie
            Console.WriteLine("Czy chcesz dodać ubezpieczenie? (tak/nie) KOSZT: 50 ZL");
            selectedSeat.HasInsurance = Console.ReadLine().Trim().ToLower() == "tak";

            // Pytanie o nadwagowy bagaż
            Console.WriteLine("Czy masz nadwagowy bagaż? (tak/nie) KOSZT: 25 ZL:");
            selectedSeat.HasExtraLuggage = Console.ReadLine().Trim().ToLower() == "tak";

            // Obliczanie kosztów
            decimal cost = CalculateReservationCost(seatClass, selectedSeat.HasInsurance, selectedSeat.HasExtraLuggage);
            Console.WriteLine($"Koszt rezerwacji: {cost} PLN");

            selectedSeat.IsAvailable = false; //zmiana pola na zarezerwowane
            selectedSeat.ReservedBy = this.username; // Rezerwacja miejsca przez zalogowanego użytkownika

            UpdateCsvFile(); //updejt pliku CSV
            Console.WriteLine($"Rezerwacja miejsca {seatNumber} na lot ID {flightId} została dokonana przez {this.username}.");
        }
        private decimal CalculateReservationCost(char seatClass, bool hasInsurance, bool hasExtraLuggage)
        {
            decimal baseCost = seatClass switch
            {
                'A' => 200m, // ceny dla różnych klass A-200, B-150,C100,D50 ceny nie maja uzależnonego mnożnika od długości lotu (do wdrożenia)
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
            /// <summary>
            /// Pozwala użytkownikowi na anulowanie istniejącej rezerwacji.
            /// Użytkownik musi wybrać lot i miejsce, które chce anulować.
            Console.WriteLine("Wybierz ID lotu, dla którego chcesz anulować rezerwację:");
            int flightId = int.Parse(Console.ReadLine());

            var userReservedSeats = flightSeats.Where(f => f.FlightId == flightId && !f.IsAvailable && f.ReservedBy == this.username).ToList();

            if (!userReservedSeats.Any()) //sprawdzenie czy sa rezerwacje na tego usera na ten lot
            {
                Console.WriteLine("Nie masz żadnych rezerwacji na ten lot.");
                return;
            }

            Console.WriteLine("Twoje zarezerwowane miejsca:"); //wyswietlenie zarezerwawanych miejsc
            foreach (var seat in userReservedSeats)
            {
                Console.WriteLine($"Miejsce numer: {seat.SeatNumber}");
            }

            Console.WriteLine("Wybierz numer miejsca do anulowania:");
            string seatNumber = Console.ReadLine();

            var selectedSeat = userReservedSeats.FirstOrDefault(s => s.SeatNumber == seatNumber);
            if (selectedSeat == null) //sprawdzenie czy wskazane miejsce jest zarezerwowane przez usera lub czy numer jest prawidłowy
            {
                Console.WriteLine("Nieprawidłowy numer miejsca lub nie masz rezerwacji na to miejsce."); 
                return;
            }

            selectedSeat.IsAvailable = true;
            selectedSeat.ReservedBy = null; // Usunięcie nazwy użytkownika z rezerwacji
            UpdateCsvFile(); //aktualizacja pliku CSV
            Console.WriteLine($"Rezerwacja miejsca {seatNumber} na lot ID {flightId} została anulowana.");
        }

        public void DisplayUserReservations()
        {

            /// <summary>
            /// Wyświetla wszystkie rezerwacje dokonane przez zalogowanego użytkownika.
            /// </summary>
            var userReservations = flightSeats
                .Where(f => f.ReservedBy == this.username && !f.IsAvailable)
                .ToList(); //wyswietlenie rezerwacji dla akutalnie zalogowanego usera

            if (!userReservations.Any())
            {
                Console.WriteLine("Nie masz żadnych rezerwacji.");
                return;
            }

            Console.WriteLine($"Rezerwacje dla użytkownika: " + username);
            foreach (var reservation in userReservations)
            {
                Console.WriteLine($"Lot ID: {reservation.FlightId}, Miejsce: {reservation.SeatNumber}");
            }
        }

        public void DisplayUserReservationsExternal() //Panel admina do podgladniecia rezerwacji 
        {
            if (this.username == "admin") //sprawdzenie czy zalogowany user to admin
            {
                string external_username;
                Console.Write("Podaj login usera ktorego rezerwacje chcesz sprawdzic: ");
                external_username = Console.ReadLine();
                var userReservations = flightSeats
                    .Where(f => f.ReservedBy == external_username && !f.IsAvailable)
                    .ToList(); //wyswietlenie rezerwacji szukanego usera

                if (!userReservations.Any()) //jak nie ma rezerwacji
                {
                    Console.WriteLine("Nie masz żadnych rezerwacji.");
                    return;
                }

                Console.WriteLine($"Rezerwacje dla użytkownika: " + external_username); //wyswietlenie info
                foreach (var reservation in userReservations)
                {
                    Console.WriteLine($"Lot ID: {reservation.FlightId}, Miejsce: {reservation.SeatNumber}");
                }
            }
            else { Console.WriteLine("Nie jestes adminem!"); } //wyrzucenie informacji w razie braku konta admin
        }

        public void DisplayPassengerList()
        {
            if (this.username == "admin")
            {
                Console.Write("Podaj ID lotu, którego rezerwacje chcesz sprawdzić: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int flightId))
                {
                    var passengers = flightSeats.Where(f => f.FlightId == flightId);

                    if (!passengers.Any())
                    {
                        Console.WriteLine("Brak pasażerów dla tego lotu.");
                        return;
                    }

                    Console.WriteLine($"Lista pasażerów dla lotu ID: {flightId}");
                    foreach (var passenger in passengers)
                    {
                        Console.WriteLine($"Miejsce: {passenger.SeatNumber}, Pasażer: {passenger.ReservedBy}");
                    }
                }
                else
                {
                    Console.WriteLine("Podano nieprawidłowe ID lotu. Proszę wprowadzić numer.");
                }
            }
            else
            {
                Console.WriteLine("Nie jesteś adminem");
            }
            return;
        }


        private void UpdateCsvFile() //aktualizacja pliku CSV
        {
            /// <summary>
            /// Aktualizuje plik CSV zawierający dane rezerwacji po każdej operacji rezerwacji lub anulowania.
            /// </summary>
            var lines = new List<string> { "FlightId,SeatNumber,Availability,ReservedBy" }; 
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