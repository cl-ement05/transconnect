namespace transconnect
{
    public class Module_Commande
    {
        private List<Commande> commandes ;
        private List<Client> clients;
        private List<Chauffeur> chauffeurs;

        public Module_Commande(List<Client> clients, List<Chauffeur> chauffeurs)
        {
            this.commandes = new List<Commande>();
            this.clients = clients;
            this.chauffeurs = chauffeurs;
        }

        public void Creer_commande(Client client, string villeDepart, string villeArrivee, DateTime dateCommande, Chauffeur chauffeur, decimal tarifHoraire)
        {
        // A ajouter : 
        // Graphe : calcul du trajet + calcul km parcoururus
        // Calcul prix de la commande : km parcourus + tarif horaire

            Client? clientExistant = clients.Find(c => c.NumeroSS == client.NumeroSS);
            if (clientExistant == null)
            {
                Console.WriteLine("Client non trouvé. Veuillez saisir les informations du nouveau client.");
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
                string numeroSS = client.NumeroSS;

                Client nouveauClient = new Client(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone);
                clients.Add(nouveauClient);
                Console.WriteLine("Nouveau client ajouté.");
                clientExistant = nouveauClient;

            }
            else
            {
                Console.WriteLine("Client trouvé !");
            }

            Chauffeur? chauffeurSelectionne = chauffeurs.Find(ch => ch.EstDisponible(dateCommande));
            if(chauffeurSelectionne == null)
            {
                Console.WriteLine("Aucun chaufeur n'est disponible à la date demandée : "+ dateCommande);
                return;
            }

            int numeroCommande = commandes.Count + 1;

            /*Commande commande = new Commande(numeroCommande, client, villeDepart, villeArrivee, prixCommande, chauffeurSelectionne, dateCommande);
            /commandes.Add(commande);
            client.HistoriqueCommandes.Add(commande);
            chauffeurSelectionne.LivraisonsEffectuees.Add(commande);

            Console.WriteLine("Commande créée avec succès :");
            Console.WriteLine(commande.ToString());
            */
        
        }
    }
}