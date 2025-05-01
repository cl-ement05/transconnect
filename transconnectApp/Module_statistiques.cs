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
            foreach(Chauffeur chf in dataState.chauffeurs)
            {
                int nb_livraisons = dataState.commandes.FindAll(c => c.Chauffeur.Equals(chf)).Count;
                Console.WriteLine(chf.Nom + " " + chf.Prenom + " : " + nb_livraisons + " livraisons");
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
            Console.WriteLine("Moyenne des prix des commandes : " + moyenne + " euros");
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

            foreach (Client c in dataState.clients)
            {
                double totalclient=0;
                List<Commande> cmds = dataState.commandes.FindAll(cmd => cmd.Client.Equals(c));
                foreach (Commande cmd in cmds)
                {
                    totalclient += cmd.CalculerPrixCommande(dataState);
                }
                double moyenne = totalclient / cmds.Count();
                Console.WriteLine($"Moyenne des comptes du client : {c.Prenom} {c.Nom} " + moyenne + " euros ");
            }
            
        }
    }
}
