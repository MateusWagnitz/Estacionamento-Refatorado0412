using Estacionamento_V2.Service;
using Estacionamento_V2.Entities;
using System;
using System.Threading;
using Estacionamento_V2.Entities.Enums;

namespace Estacionamento_V2
{
    #region Problemas
    // criar ticket rápido não está listando os carros, apenas criando uma index para os tickets

    #endregion


    class Program
    {

        static Services service = new Services();

        static void Main(string[] args)
        {
            MainMenu();

        }
        #region Main menu
        static void MainMenu()
        {

            Park park = service.ParkingLot();
            Console.WriteLine("***********TECHER PARK***********");
            Console.WriteLine($"Avaible spots: {park.Avaible_Spots} - ");
            Console.WriteLine();
            Console.WriteLine("Choose a option:");            
            Console.WriteLine("1 - Ticket Control");
            Console.WriteLine("2 - Costumer registration");
            Console.WriteLine("3 - Parked cars");
            Console.WriteLine("4 - Balance");
            Console.WriteLine("5 - Exit");
            Console.WriteLine("*********************************");

            var caseSwitch = int.Parse(Console.ReadLine());

            try
            {
                switch (caseSwitch)
                {
                    case 1:
                        TicketControl();
                        break;
                    case 2:
                        ClientMenu();
                        break;
                    case 3:
                        ParkedCarList();
                        break;
                    case 4:
                        BalanceMenu();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;                   
                    default:
                        Console.WriteLine("Invalid Option, please select a valid option");
                        Thread.Sleep(1000);
                        Console.Clear();
                        MainMenu();
                        break;
                }

            }
            catch (Exception e)
            {

                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine();
                Console.WriteLine("Press any key to return");
                Console.WriteLine();
                

                MainMenu();
            }

            #endregion

        #region Ticket Control

            static void TicketControl()
            {
                Console.Clear();
                Console.WriteLine("***********TICKET CONTROL***********");                
                Console.WriteLine();
                Console.WriteLine("Choose a option:");
                Console.WriteLine("1 - Create Fast ticket");
                Console.WriteLine("2 - Close a ticket");
                Console.WriteLine("3 - Active Ticket list");
                Console.WriteLine("4 - Closed Ticket list");
                Console.WriteLine("5 - Return to main menu");
                Console.WriteLine("*********************************");


                var caseSwitch = int.Parse(Console.ReadLine());

                switch (caseSwitch)
                {
                    case 1:
                        CreateTicket();
                        break;
                    case 2:
                        CloseTicket();
                        break;
                    case 3:
                        ActiveTicketsList();
                        break;
                    case 4:
                        ClosedTicketsList();
                        break;
                    case 5:
                        Console.Clear();
                        MainMenu();
                        break;
                    default:
                        Console.WriteLine("Insert a valid option");
                        Thread.Sleep(1000);
                        Console.Clear();
                        TicketControl();
                        break;
                }      
                

                static void CreateTicket()
                {
                    Console.Clear();
                    Console.WriteLine("Insert the car plate: ");
                    var plate = Console.ReadLine();
                    Console.WriteLine();

                    
                    Console.WriteLine("Insert the car brand: ");
                    var brand = Console.ReadLine();
                    Console.WriteLine();

                    
                    Console.WriteLine("Insert the car model: ");
                    var model = Console.ReadLine();
                    Console.WriteLine();

                    
                    service.TicketRegister(plate, brand, model);

                    Console.Clear();
                    TicketControl();

                }


                static void CloseTicket()
                {
                    Console.Clear();
                    Console.WriteLine("Insiert the ticket ID: ");
                    var id_Ticket = Console.ReadLine();

                    service.CloseTicket(id_Ticket);

                    Console.Clear();
                    TicketControl();
                }

                static void ActiveTicketsList()
                {
                    Console.Clear();

                    string tickets = service.CurrentActiveTicketsList();

                    Console.Write(tickets);

                    Console.WriteLine("Press anything to return...");
                    Console.ReadLine();

                    TicketControl();

                }

                static void ClosedTicketsList()
                {
                    Console.Clear();

                    string tickets = service.ClosedTicketsList();

                    Console.Write(tickets);

                    Console.WriteLine("Press anything to return...");
                    Console.ReadLine();

                    TicketControl();
                }

            }

        }
        #endregion

        #region ClientMenu
        static void ClientMenu()
        {
            Console.Clear();
            Console.WriteLine("*********CLIENT CONTROL********");
            Console.WriteLine("1 - Client Registry");
            Console.WriteLine("2 - Client List");
            Console.WriteLine("3 - Return");           
            Console.WriteLine("*******************************");
            Console.WriteLine();

            var caseSwitch = int.Parse(Console.ReadLine());

            switch (caseSwitch)
            {
                case 1:
                    ClientRegistration();
                    break;
                case 2:
                    ClientList();
                    break;
                case 3:
                    MainMenu();
                    break;               
                default:
                    Console.WriteLine("\nSelect a valid option...");
                    Thread.Sleep(1000);
                    ClientMenu();
                    break;
            }
        }

        static void ClientRegistration()
        {
            Console.Clear();
            Console.WriteLine("Insir a Client Name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Insert the Client CPF");
            var cpf = Console.ReadLine();

            service.ClientRegistry(name, cpf);

            Console.Clear();
            ClientMenu();
        }

        static void ClientList()
        {
            Console.Clear();

            string clientes = service.TheClientList();


            Console.Write(clientes);

            Console.WriteLine("Press anything to return...");
            Console.ReadLine();

            ClientMenu();
        }



        static void ListCar()
        {
            Console.Clear();

            string cars = service.TheCarList();

            Console.Write(cars);
            Console.WriteLine("Press anything to return...");
            Console.ReadLine();

            ParkedCarList();
        }

        #endregion

        #region Parked Car List

        static void ParkedCarList()
        {
            Console.Clear();
            Park park = service.ParkingLot();

            

            Console.WriteLine("*******************************PARKING LOT CONTROL******************************");
            Console.WriteLine();
            Console.WriteLine($"Total Capacity: {park.Total_Capacity} - " +
                              $"Occupied spots: { park.Occupied_Spots } - " +
                              $"Avaible spots: { park.Avaible_Spots }");
            Console.WriteLine();
            Console.WriteLine("********************************************************************************");

            string parkList = service.ParkList();

            Console.WriteLine(parkList);



            Console.Write("Press anything to return...");
            Console.ReadLine();

            MainMenu();
        }

        #endregion

        #region Balance
        static void BalanceMenu()
        {
            Console.Clear();
            Console.WriteLine("***********BALANCE MENU*********");
            Console.WriteLine("1 - Select a period");
            Console.WriteLine("2 - Return");
            Console.WriteLine();
            Console.WriteLine($"Today's Balance: { service.PeriodBalance(DateTime.Today, DateTime.Now).ToString("F2") }");
            Console.WriteLine();
            Console.WriteLine("********************************");
            Console.WriteLine();

            var caseSwitch = int.Parse(Console.ReadLine());

            switch (caseSwitch)
            {
                case 1:
                    ValorPorPeriodo();
                    break;
                case 2:
                    MainMenu();
                    break;                    
                default:
                    Console.WriteLine("\n Insert a valid value...");
                    Thread.Sleep(1000);
                    BalanceMenu();
                    break;
            }

            static void ValorPorPeriodo()
            {
                Console.Clear();
                Console.WriteLine("Enter the initial date: (DD/MM/YYYY)");
                var date = Console.ReadLine();

                //Checking the if the date is correct.
                while (service.ValidateDateTime(date) == false)
                {
                    Console.WriteLine("\nInsert a valid date!");
                    date = Console.ReadLine();
                }
                DateTime inicialDate = DateTime.Parse(date);


                Console.WriteLine("Enter the end date: (DD/MM/AAAA)");
                date = Console.ReadLine();

                // the first entry must be lower then a second
                while (service.ValidateFinalDateTime(date, inicialDate) == false)
                {
                    Console.WriteLine("\nInsert a valid date!");
                    date = Console.ReadLine();
                }
                DateTime dataFinal = DateTime.Parse(date);

                //Receiving the overall balance by a period
                double valor = service.PeriodBalance(inicialDate, dataFinal);

                Console.Clear();
                Console.WriteLine($"Balance within the period: {valor.ToString("F2")} ");
                Console.WriteLine("Press anything to return...");
                Console.ReadLine();

                BalanceMenu();
            }
        }

        #endregion
    }
}
