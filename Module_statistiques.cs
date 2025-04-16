namespace transconnect
{
    public class Module_statistiques
    {
        private List<Commande> commandes;
        private List<Client> clients;
        private List<Chauffeur> chauffeurs;

        public Module_statistiques(List<Commande> commandes, List<Client> clients, List<Chauffeur> chauffeurs)
        {
            this.commandes=commandes;
            this.clients=clients;
            this.chauffeurs=chauffeurs;
        }

        public void  AfficherLivraisonsParChauffeur()
        {
            Console.WriteLine("Nombre de livraison par chaffeur : ");
            foreach(Chauffeur c in chauffeurs)
            {
                int nb_livraisons=c.LivraisonsEffectuees.Count;
                Console.WriteLine(c.Nom + " " + c.Prenom + " :"+ nb_livraisons + " livraisons");
            }
        }

        public void AfficherCommandesParPeriode(DateTime debut, DateTime fin)
        {
            Console.WriteLine("Liste des commandes entre "+ debut.ToShortDateString()+ " et " + fin.ToShortDateString());
            foreach(Commande c in commandes)
            {
                if (c.DateCommande.Date >= debut.Date && c.DateCommande<=fin.Date)
                {
                    Console.WriteLine(c);
                }
            }
        }

        // A compléter : Afficher la moyenne des prix des commandes
        public void AfficherMoyennePrixCommandes()
        {
            if(commandes.Count==0)
            {
                Console.WriteLine("Aucune commande");
                return;
            }

            double somme=0;
            foreach(Commande c in commandes)
            {
                //somme+= prix commandes
            }
            double moyenne=somme/commandes.Count;
            Console.WriteLine("Moyenne des prix des commandes : "+ moyenne+ "€");
        }


        // A compléter : Moyenne des comptes clients
        public void AfficherMoyenneComptesClients()
        {
            if(clients.Count==0)
            {
                Console.WriteLine("Aucun client");
                return;
            }

            double somme=0;
            foreach (Client c in clients)
            {
                double totalclient=0;
                // Calcul prix
                somme+=totalclient;
            }
            double moyenne=somme/clients.Count;
            Console.WriteLine("Moyenne des comptes clients : "+ moyenne+ " € ");
            
        }

        public void AffichercommandesPourClient(string numeroSS)
        {
            Client? client=clients.Find(c=> c.NumeroSS==numeroSS);
            if(client==null)
            {
                Console.WriteLine("Client introuvable");
                return;
            }

            if(client.HistoriqueCommandes.Count==0)
            {
                Console.WriteLine("Aucune commande enregistrée pour ce client");
                return;
            }

            Console.WriteLine("Liste des commandes pour le client "+ client.Nom + " "+ client.Prenom + " : ");

            foreach (Commande c in client.HistoriqueCommandes)
            {
                Console.WriteLine("\t"+ c);
            }
        }
    }
}