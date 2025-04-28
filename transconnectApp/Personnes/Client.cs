namespace transconnect
{
    public class Client : Personne
    {

        public Client(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                    string adressePostale, string email, string telephone)
                    : base(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone)
        {
            
        }

        /// <summary>
        /// Ajoute ce client à la liste des clients.
        /// </summary>
        /// <param name="client"></param>
        public void AjouterClient(DataState dataState)
        {
            dataState.clients.Add(this);
        }

        /// <summary>
        /// Supprime ce client de la liste des clients.
        /// </summary>
        /// <param name="client"></param>
        public void SupprimerClient(DataState dataState)
        {
            dataState.clients.Remove(this);
        }

        public void AfficherTrajetsFavoris(DataState dataState) {
            Dictionary<(string, string), int> trajets = new Dictionary<(string, string), int>();
            dataState.commandes.ForEach(c => trajets[(c.VilleDepart, c.VilleArrivee)] += 1);
            trajets.OrderByDescending(c => c.Value).ToList().ForEach(
                e => Console.WriteLine($"{e.Key.Item1} -> {e.Key.Item2}"));
        }

        /// <summary>
        /// Afficher les commandes pour un client donné
        /// </summary>
        /// <param name="numeroSS"></param>
        public void AffichercommandesPourClient(DataState dataState)
        {
            if(dataState.commandes.FindAll(c => c.Client.Equals(this)).Count() == 0)
            {
                Console.WriteLine("Aucune commande enregistrée pour ce client");
                return;
            }

            Console.WriteLine("Liste des commandes pour le client " + nom + " " + prenom + " : ");

            foreach (Commande c in dataState.commandes.FindAll(c => c.Client.Equals(this)))
            {
                Console.WriteLine("\t" + c);
            }
        }

        /// <summary>
        /// Affiche la liste des clients.
        /// </summary>
        public static void AfficherClients(DataState dataState)
        {
            Console.WriteLine("Liste des clients :");
            foreach (Client client in dataState.clients)
            {
                Console.WriteLine(client.ToString());
            }
        }

        
        /// <summary>
        /// Recherche un client par son numéro de sécurité sociale.
        /// </summary>
        /// <param name="numeroSS"></param>
        /// <returns></returns>
        public static Client? RechercherClientNSS(DataState dataState, string numeroSS)
        {
            return dataState.clients.Find(c => c.NumeroSS == numeroSS);
        }

        /// <summary>
        /// Affiche la liste des clients triés sur le nom.
        /// </summary>
        public static void AfficherParNom(DataState dataState)
        {
            Console.WriteLine("IClients par Nom :");
            foreach (Client c in dataState.clients.OrderBy(c => c.Nom))
            {
                Console.WriteLine(c.ToString());
            }
        }


        /// <summary>
        /// Affiche les clients résidant dans la ville passée en paramètre
        /// </summary>
        /// <param name="ville"></param>
        public static void AfficherParVille(DataState dataState, string ville)
        {
            Console.WriteLine($"Clients par Ville : {ville}");
            List<Client> liste = dataState.clients.Where(c => c.AdressePostale.Contains(ville)).ToList();
            if (liste.Count == 0) {
                Console.WriteLine("Aucun client trouvé");
            } else {
                foreach (Client c in liste)
                {
                    Console.WriteLine(c.ToString());
                }
            }
        }

        public static Client CreerNouveau(DataState dataState) {
            Console.WriteLine("Veuillez saisir les informations du nouveau client.");
            PersonneDataHolder data = CreerPersonne(dataState);
            return new Client(
                data.numeroSS,
                data.nom,
                data.prenom,
                data.dateNaissance,
                data.adressePostale,
                data.email,
                data.telephone
            );
        }

        /// <summary>
        /// Affiche les clients triés par ordre décroissant de montant de commande.
        /// </summary>
        public static void AfficherParMontantCommande(DataState dataState)  
        {
            Console.WriteLine("Clients par Montant de Commande :");
            List<Client> clientsTrie = dataState.clients.OrderByDescending(c => 
            {
                double montantTotal = 0;
                foreach (Commande commande in dataState.commandes)
                {
                    if (commande.Client.NumeroSS == c.NumeroSS)
                    {
                        montantTotal += commande.CalculerPrixCommande(dataState);
                    }
                }
                return montantTotal;
            }).ToList();

            foreach (Client c in clientsTrie)
            { 
                Console.WriteLine(c.ToString());
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is Client otherClient)
            {
                return this.NumeroSS == otherClient.NumeroSS;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return NumeroSS.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        
        }
    }
}
