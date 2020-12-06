

namespace Estacionamento_V2.Entities
{
    public class Park
    {
        public int Id_Park { get; set; }
        public int Total_Capacity { get; set; }
        public int Occupied_Spots { get; set; }
        public int Avaible_Spots { get; set; }

        public Park()
        {
            Id_Park = 1;
            Total_Capacity = 50;
            Occupied_Spots = 0;
            Avaible_Spots = Total_Capacity;

        }
    }
}
