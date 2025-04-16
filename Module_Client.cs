using System.Net.WebSockets;

namespace transconnect
{
    public class Module_Client
    {
        private List<Client> clients;
        
        public Module_Client()
        {
            clients = new List<Client>();
        }

        public List<Client> Clients
        {
            get { return clients; }
            set { clients = value; }
        }

        public void AjouterClient(Client client)
        {
            clients.Add(client);
        }

        public void SupprimerClient(Client client)
        {
            clients.Remove(client);
        }

        public Client? RechercherClient(Client client)
        {
            return clients.Find(c => c.NumeroSS == client.NumeroSS);
        }
        
        public Client? RechercherClientNSS(string numeroSS)
        {
            return clients.Find(c => c.NumeroSS == numeroSS);
        }

        public void ModifierClientParNumeroSS(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                                      string adressePostale, string email, string telephone)
        {
            Client? client = RechercherClientNSS(numeroSS);
            if (client != null)
            {
                client.Nom = nom;
                client.Prenom = prenom;
                client.DateNaissance = dateNaissance;
                client.AdressePostale = adressePostale;
                client.Email = email;
                client.Telephone = telephone;
            }
            else
            {
                Console.WriteLine($"Client avec le numéro SS {numeroSS} introuvable.");
            }
        }

        public void AfficherClients()
        {
            Console.WriteLine("Liste des clients :");
            foreach (Client client in clients)
            {
                Console.WriteLine(client.ToString());
            }
        }

        public void AfficherParNom()
        {
            Console.WriteLine("IClients par Nom :");
            foreach (Client c in clients.OrderBy(c => c.Nom))
            {
                Console.WriteLine(c.ToString());
            }
        }

        public void AfficherParVille(string ville)
        {
            Console.WriteLine($"Clients par Ville : {ville}");
            foreach (Client c in clients.Where(c => c.AdressePostale.Contains(ville)))
            {
                Console.WriteLine(c.ToString());
            }
        }

        /// <summary>
        /// Affiche les clients par montant de commande.
        /// </summary>
        public void AfficherParMontant()
        {
            
        }




    









    }


    
}