﻿using Program_Do_Rezerwacji_Lotow.Program_Do_Rezerwacji_Lotow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Do_Rezerwacji_Lotow
{
    public class ReservationManager // Klasa ReservationManager zarządza rezerwacjami miejsc na loty.
    {
        // Ścieżka do pliku CSV i lista przechowująca rezerwacje.
        LoginPanel loginPanel = new LoginPanel();
        private string csvFilePath;
        private List<FlightSeat> flightSeats;
        private string username;


        public ReservationManager(string username)  // Konstruktor inicjalizuje menedżera rezerwacji 
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
                    ReservedBy = parts[3]
                };
                flightSeats.Add(flightSeat);
            }
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

            selectedSeat.IsAvailable = false;
            selectedSeat.ReservedBy = this.username; // Rezerwacja miejsca przez zalogowanego użytkownika
            UpdateCsvFile();
            Console.WriteLine($"Rezerwacja miejsca {seatNumber} na lot ID {flightId} została dokonana przez {this.username}.");
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
            lines.AddRange(flightSeats.Select(f => $"{f.FlightId},{f.SeatNumber},{(f.IsAvailable ? "Available" : "Occupied")},{f.ReservedBy}"));
            File.WriteAllLines(csvFilePath, lines);
        }
    }

    public class FlightSeat
    {
        public string ReservedBy { get; set; }
        public int FlightId { get; set; }
        public string SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
    }
}