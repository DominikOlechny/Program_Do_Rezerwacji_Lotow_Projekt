using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Do_Rezerwacji_Lotow
{
    public class FlightSeat
    {
        public string ReservedBy { get; set; }
        public int FlightId { get; set; }
        public string SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
        public char SeatClass { get; set; } 
        public bool HasInsurance { get; set; }
        public bool HasExtraLuggage { get; set; }

    }
}
