using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Do_Rezerwacji_Lotow
{
    /// <summary>
    /// Reprezentuje miejsce na pokładzie samolotu, zawierając informacje o rezerwacji, dostępności oraz dodatkowych opcjach.
    /// wykorzystywane wlasciwosci sa zarowno w klasie CONtodatapbaselistofusers jak i reservation menager
    /// </summary>
    class FlightSeat
    {
        public int FlightId { get; set; }  // Identyfikator lotu
        public string SeatNumber { get; set; } // Numer miejsca
        public bool IsAvailable { get; set; } // Dostępność miejsca
        public string ReservedBy { get; set; } // Użytkownik, który zarezerwował miejsce
        public char SeatClass { get; set; } // Klasa miejsca
        public bool HasInsurance { get; set; } // Czy ubezpieczenie zostało dokupione
        public bool HasExtraLuggage { get; set; } // Czy dodatkowy bagaż został dokupiony

    }
}
