using Estacionamento_V2.Entities.Enums;
using System.Collections.Generic;


namespace Estacionamento_V2.Entities
{
    class Client
    {
        public string Id_Cpf { get; private set; }
        public string Name { get; set; }
        public ClientStatus ClientStatus { get; set; }


        public List<Client> ClientList = new List<Client>();

        public Client() { }

        public Client(string id_Cpf, string name, ClientStatus clientStatus)
        {
            Id_Cpf = id_Cpf;
            Name = name;
            ClientStatus = clientStatus;
        }
    }
}
