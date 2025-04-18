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

        /// <summary>
        /// Ajoute un client à la liste des clients.
        /// </summary>
        /// <param name="client"></param>

        public void AjouterClient(Client client)
        {
            clients.Add(client);
        }

        /// <summary>
        /// Supprime un client de la liste des clients.
        /// </summary>
        /// <param name="client"></param>

        public void SupprimerClient(Client client)
        {
            clients.Remove(client);
        }

        /// <summary>
        /// Recherche un client dans la liste (deux clients sont égaux s'ils ont le meme numéro de sécu)
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>

        public Client? RechercherClient(Client client)
        {
            return clients.Find(c => c.Equals(client));
        }

        /// <summary>
        /// Recherche un client par son numéro de sécurité sociale.
        /// </summary>
        /// <param name="numeroSS"></param>
        /// <returns></returns>
        
        public Client? RechercherClientNSS(string numeroSS)
        {
            return clients.Find(c => c.NumeroSS == numeroSS);
        }

        /// <summary>
        /// Modifie les informations d'un client par son numéro de sécurité sociale.
        /// </summary>
        /// <param name="numeroSS"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="dateNaissance"></param>
        /// <param name="adressePostale"></param>
        /// <param name="email"></param>
        /// <param name="telephone"></param>

        public void ModifierClientParNumeroSS(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                                      string adressePostale, string email, string telephone)
        {
            Client? client = RechercherClientNSS(numeroSS);
            if (client is not null)
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

        /// <summary>
        /// Affiche la liste des clients.
        /// </summary>

        public void AfficherClients()
        {
            Console.WriteLine("Liste des clients :");
            foreach (Client client in clients)
            {
                Console.WriteLine(client.ToString());
            }
        }

        /// <summary>
        /// Affiche la liste des clients triés sur le nom.
        /// </summary>

        public void AfficherParNom()
        {
            Console.WriteLine("IClients par Nom :");
            foreach (Client c in clients.OrderBy(c => c.Nom))
            {
                Console.WriteLine(c.ToString());
            }
        }


        /// <summary>
        /// Affiche les clients résidant dans la ville passée en paramètre
        /// </summary>
        /// <param name="ville"></param>

        public void AfficherParVille(string ville)
        {
            Console.WriteLine($"Clients par Ville : {ville}");
            foreach (Client c in clients.Where(c => c.AdressePostale.Contains(ville)))
            {
                Console.WriteLine(c.ToString());
            }
        }

        /// <summary>
        /// Affiche les clients triés par ordre décroissant de montant de commande.
        /// </summary>
        public void AfficherParMontantCommande(Module_Commande moduleCommande)  
        {
            Console.WriteLine("Clients par Montant de Commande :");
            List<Client> clientsTrie = clients.OrderByDescending(c => 
            {
                double montantTotal = 0;
                foreach (Commande commande in moduleCommande.Commandes)
                {
                    if (commande.Client.NumeroSS == c.NumeroSS)
                    {
                        montantTotal += moduleCommande.CalculerPrixCommande(commande.NumeroCommande);
                    }
                }
                return montantTotal;
            }).ToList();

            foreach (Client c in clientsTrie)
            { 
                Console.WriteLine(c.ToString());
            }
        }




    









    }


    
}