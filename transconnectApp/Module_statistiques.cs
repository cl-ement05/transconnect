namespace transconnect
{
    public class Module_statistiques
    {
        DataState dataState;

        public Module_statistiques(DataState dataState)
        {
            this.dataState = dataState;
        }


        /// <summary>
        /// Afficher le nombre de livraisons par chauffeur
        /// </summary>
        public void AfficherLivraisonsParChauffeur()
        {
            Console.WriteLine("Nombre de livraison par chaffeur : ");
            foreach(Chauffeur c in dataState.chauffeurs)
            {
                int nb_livraisons=c.LivraisonsEffectuees.Count;
                Console.WriteLine(c.Nom + " " + c.Prenom + " :" + nb_livraisons + " livraisons");
            }
        }

        /// <summary>
        /// Afficher les commandes sur une période donnée
        /// </summary>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        public void AfficherCommandesParPeriode(DateTime debut, DateTime fin)
        {
            Console.WriteLine("Liste des commandes entre "+ debut.ToShortDateString() + " et " + fin.ToShortDateString());
            foreach(Commande c in dataState.commandes)
            {
                if (c.DateCommande.Date >= debut.Date && c.DateCommande <= fin.Date)
                {
                    Console.WriteLine(c);
                }
            }
        }

        /// <summary>
        /// Afficher la moyenne des prix des commandes
        /// </summary>
        public void AfficherMoyennePrixCommandes()
        {
            if(dataState.commandes.Count == 0)
            {
                Console.WriteLine("Aucune commande");
                return;
            }

            double somme = 0;
            foreach(Commande c in dataState.commandes)
            {
                somme += c.CalculerPrixCommande(dataState);
            }
            double moyenne = somme / dataState.commandes.Count;
            Console.WriteLine("Moyenne des prix des commandes : " + moyenne + "€");
        }

        /// <summary>
        /// Afficher la moyenne des comptes clients
        /// </summary>
        public void AfficherMoyenneComptesClients()
        {
            if(dataState.clients.Count == 0)
            {
                Console.WriteLine("Aucun client");
                return;
            }

            double somme = 0;
            foreach (Client c in dataState.clients)
            {
                double totalclient=0;
                somme+=totalclient;
            }
            double moyenne=somme / dataState.clients.Count;
            Console.WriteLine("Moyenne des comptes clients : " + moyenne + " € ");
            
        }

        /// <summary>
        /// Afficher les commandes pour un client donné
        /// </summary>
        /// <param name="numeroSS"></param>
        public void AffichercommandesPourClient(string numeroSS)
        {
            Client? client = Client.RechercherClientNSS(dataState, numeroSS);
            if(client is null)
            {
                Console.WriteLine("Client introuvable");
                return;
            }

            if(dataState.commandes.FindAll(c => c.Client.Equals(client)).Count() == 0)
            {
                Console.WriteLine("Aucune commande enregistrée pour ce client");
                return;
            }

            Console.WriteLine("Liste des commandes pour le client " + client.Nom + " " + client.Prenom + " : ");

            foreach (Commande c in dataState.commandes.FindAll(c => c.Client.Equals(client)))
            {
                Console.WriteLine("\t" + c);
            }
        }
    }
}
