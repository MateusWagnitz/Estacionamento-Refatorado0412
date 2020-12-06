using System;
using System.Threading;
using Estacionamento_V2.Entities;
using Estacionamento_V2.Entities.Enums;

namespace Estacionamento_V2.Service
{

    class Services
    {
        Ticket ticket = new Ticket();
        Car car = new Car();
        Park park = new Park();
        Client client = new Client();

        public void ClientRegistry(string name, string cpf)
        {
            //checking if the current CPF was already listed
            Client cli = SearchClient(cpf);

            if (cli == null)
            {
                cli = new Client(cpf, name, ClientStatus.Monthly);
                client.ClientList.Add(cli);

                System.Console.WriteLine("\nRegistration success!");
                Thread.Sleep(1000);
            }
            //if cli is null, means that the client already have a register
            else
            {
                System.Console.WriteLine("\nThere is a registration for this client already!");
                Thread.Sleep(1000);
            }
        }



        public string TheCarList()
        {
            string ListCar = "";

            foreach (var carro in car.CarList)
            {
                Client owner = SearchClient(carro.Id_Owner);

                ListCar += "Placa: " + carro.Id_Plate + "\n" +
                      "Marca: " + carro.Brand + "\n" +
                      "Modelo: " + carro.Model + "\n" +
                      "Dono: " + owner.Id_Cpf + " - " + owner.Name +
                      "\n\n";
            }

            return ListCar;
        }

        public string TheClientList()
        {
            string listCli = "";

            foreach (var client in client.ClientList)
            {
                listCli += client.Id_Cpf + " - " + client.Name + " - " + client.ClientStatus + "\n";

            }
            return listCli;
        }

        public void TicketRegister(string plate, string brand, string model)
        {
            //Car carro = SearchCar(plate);
            Car carro = new Car();

            if (carro != null)
            {
                if (park.Occupied_Spots < park.Avaible_Spots)
                {

                    Ticket tic = new Ticket(
                        (ticket.TicketList.Count + 1).ToString(),
                                                    carro.Id_Plate,
                                                    //car.Brand,
                                                    //car.Model,
                                                    DateTime.Now);

                    ticket.TicketList.Add(tic);
                    park.Occupied_Spots++;
                    park.Avaible_Spots--;

                    Console.WriteLine();
                    Console.WriteLine("Ticked created with success!");

                }

                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Park currently full!");
                    Thread.Sleep(1000);

                }
            }
            else
            {
                System.Console.WriteLine("Insert a valid car....");
                Thread.Sleep(1000);
            }

        }

        public string CurrentActiveTicketsList()
        {
            string listaTic = "";

            foreach (var ticket in ticket.TicketList)
            {
                if (ticket.ExitTime == null)
                {
                    Car carro = SearchCar(ticket.Id_Car);
                    Client cli = SearchClient(carro.Id_Owner);

                    listaTic += "Ticket: " + ticket.Id_Ticket + "\n" +
                                "Car: " + ticket.Id_Car + " - " +
                                car.Brand + " - " + car.Model + "\n" +
                                "Owner: " + cli.Id_Cpf + " - " + cli.Name 
                                + "\n" +
                                "Entrance Time: " + ticket.EntranceTime + "\n" +
                                "Exit Time: " + ticket.ExitTime +
                                "\n\n";
                }
            }

            return listaTic;
        }

        public void CloseTicket(string id_Ticket)
        {
            int index = SearchTicket(id_Ticket);

            if (index >= 0)
            {
                TimeSpan duracao = DateTime.Now.Subtract(ticket.TicketList[index].EntranceTime);
                ticket.TicketList[index].ExitTime = DateTime.Now;

                ticket.TicketList[index].Value = 1.50 * (Math.Ceiling(duracao.TotalMinutes / 15));

                park.Occupied_Spots--;
                park.Avaible_Spots++;

                Console.WriteLine();
                System.Console.WriteLine("Ticket ended with success!");
                Thread.Sleep(1000);
            }
            else
            {
                System.Console.WriteLine("Ticket not found!");
                Thread.Sleep(1000);
            }
        }

        public string ClosedTicketsList()
        {
            string listaTic = "";

            foreach (var ticket in ticket.TicketList)
            {
                if (ticket.ExitTime != null)
                {
                    Car carro = SearchCar(ticket.Id_Car);
                    Client cli = SearchClient(carro.Id_Owner);

                    listaTic += "Ticket: " + ticket.Id_Ticket + "\n" +
                    "Car: " + ticket.Id_Car + " - " +
                    carro.Brand + " - " + carro.Model + "\n" +
                    "Dono: " + cli.Id_Cpf + " - " + cli.Name + "\n" +
                    "Entrada: " + ticket.EntranceTime + "\n" +
                    "Saida: " + ticket.ExitTime + "\n" +
                    "Valor: " + ticket.Value +
                    "\n\n";
                }
            }

            return listaTic;
        }



        private Car SearchCar(string id_Car)
        {
            for (int i = 0; i < car.CarList.Count; i++)
            {
                if (car.CarList[i].Id_Plate == id_Car)
                {
                    return car.CarList[i];
                }
            }
            return null;
        }

        private Client SearchClient(string id_Cliente)
        {
            for (int i = 0; i <= client.ClientList.Count - 1; i++)
            {
                if (client.ClientList[i].Id_Cpf == id_Cliente)
                {
                    return client.ClientList[i];
                }
            }
            return null;
        }

        private int SearchTicket(string id_Ticket)
        {
            for (int i = 0; i <= ticket.TicketList.Count - 1; i++)
            {
                if (ticket.TicketList[i].Id_Ticket == id_Ticket)
                {
                    return i;
                }
            }
            return -1;
        }


        public Park ParkingLot() 
        {
            return park;
        }

        public string ParkList()
        {
            string parkList = "";

            foreach (var ticket in ticket.TicketList)
            {
                if (ticket.ExitTime == null) //validador
                {
                    Car carro = SearchCar(ticket.Id_Car);
                    Client cli = SearchClient(car.Id_Owner);

                    parkList += $"Car: {ticket.Id_Car} - {car.Brand} - {car.Model} \n Owner: {cli.Id_Cpf} - {cli.Name} \n\n";
                }
            }

            return parkList;
        }

        public double PeriodBalance(DateTime inicialTime, DateTime finalTime)
        {
            double total = 0.00;
            foreach (var ticket in ticket.TicketList)
            {
                if (ticket.ExitTime >= inicialTime && ticket.ExitTime <= finalTime)
                {
                    total += ticket.Value;
                }
            }

            return total;
        }

        //Entry Date validation Service
        public bool ValidateDateTime(string date)
        {
            if (DateTime.TryParse(date, out DateTime result))
                return true;
            return false;
        }

        //Closing Date validation Service
        public bool ValidateFinalDateTime(string date, DateTime inicialTime)
        {
            if (DateTime.TryParse(date, out DateTime result))
                if (DateTime.Parse(date) > inicialTime)
                    return true;
                else
                    Console.WriteLine("\nThe final date cant be lower then the initial time!");
            return false;
        }







    }
}
