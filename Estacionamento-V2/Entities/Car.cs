using System.Collections.Generic;


namespace Estacionamento_V2.Entities
{
    public class Car
    {
        public string Id_Plate { get; private set; }
        public string Id_Owner { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }


        public List<Car> CarList = new List<Car>();

        public Car() { }
       
        public Car(string id_Plate, string id_Owner, string brand, string model)
        {
            Id_Plate = id_Plate;
            Id_Owner = id_Owner;
            Brand = brand;
            Model = model;
        }





    }
}
