using System;
using System.Collections.Generic;

namespace Estacionamento_V2.Entities
{
    public class Ticket
    {
        public string Id_Ticket { get; private set; }
        public string Id_Car { get; private set; }
        public DateTime EntranceTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public double Value { get; set; }


        public List<Ticket> TicketList = new List<Ticket>();

        public Ticket()
        {

        }

        public Ticket(string id_Ticket, string id_Car, DateTime entranceTime)
        {
            Id_Ticket = id_Ticket;
            Id_Car = id_Car;
            EntranceTime = entranceTime;
            ExitTime = null;
            Value = 0.00;
        }
    }
}
