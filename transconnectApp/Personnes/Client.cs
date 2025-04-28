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
        /// <summary>
        /// Modifie les informations du client.
        /// </summary>
        public void ModifierClient()
        {
            Console.WriteLine("Modification des informations du client. Appuyez sur Entrée pour conserver la valeur actuelle.");

            Console.Write($"Nom ({this.nom}) : ");
            string nom = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(nom)) this.nom = nom;

            Console.Write($"Prénom ({this.prenom}) : ");
            string prenom = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(prenom)) this.prenom = prenom;

            Console.Write($"Date de naissance ({this.dateNaissance:dd/MM/yyyy}) (JJ/MM/AAAA) : ");
            string dateNaissanceInput = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(dateNaissanceInput))
            {
                if (DateTime.TryParse(dateNaissanceInput, out DateTime dateNaissance))
                {
                    this.dateNaissance = dateNaissance;
                }
                else
                {
                    Console.WriteLine("Date invalide. La valeur actuelle est conservée.");
                }
            }

            Console.Write($"Adresse postale ({this.adressePostale}) : ");
            string adressePostale = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(adressePostale)) this.adressePostale = adressePostale;

            Console.Write($"Email ({this.email}) : ");
            string email = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(email)) this.email = email;

            Console.Write($"Téléphone ({this.telephone}) : ");
            string telephone = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(telephone)) this.telephone = telephone;

            Console.WriteLine("Les informations du client ont été mises à jour.");
        }

        public (string, string) TrajetFavori(DataState dataState) {
            Dictionary<(string, string), int> trajets = new Dictionary<(string, string), int>();
            dataState.commandes.ForEach(c => trajets[(c.VilleDepart, c.VilleArrivee)] += 1);
            return trajets.OrderByDescending(c => c.Value).First().Key;
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

        public static Client CreateClient() {
            Console.WriteLine("Veuillez saisir les informations du nouveau client.");
            Console.Write("Nom : ");
            string nom = Console.ReadLine()!;
            Console.Write("Prénom : ");
            string prenom = Console.ReadLine()!;
            Console.Write("Date de naissance (JJ/MM/AAAA) : ");
            DateTime dateNaissance = Convert.ToDateTime(Console.ReadLine()!);
            Console.Write("Adresse postale : ");
            string adressePostale = Console.ReadLine()!;
            Console.Write("Email : ");
            string email = Console.ReadLine()!;
            Console.Write("Téléphone : ");
            string telephone = Console.ReadLine()!;
            Console.Write("Numéro de SS : ");
            string numeroSS = Console.ReadLine()!;

            Client nouveauClient = new Client(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone);
            return nouveauClient;
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
