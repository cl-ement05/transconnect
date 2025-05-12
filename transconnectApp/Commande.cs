namespace transconnect
{
    public class Commande
    {
        private int numeroCommande;
        private Client client;
        private string villeDepart;
        private string villeArrivee;
        private Vehicule vehicule;
        private Chauffeur chauffeur;
        private DateTime dateCommande;
        private static int lastNumber = 1;

        public Commande(Client client, string villeDepart, string villeArrivee,
                        Vehicule vehicule, Chauffeur chauffeur, DateTime dateCommande)
        {
            this.numeroCommande = lastNumber;
            this.client = client;
            this.villeDepart = villeDepart;
            this.villeArrivee = villeArrivee;
            this.vehicule = vehicule;
            this.chauffeur = chauffeur;
            this.dateCommande = dateCommande;
            lastNumber++;
        }

        public int NumeroCommande { get { return numeroCommande; } set { numeroCommande = value; } }
        public Client Client { get { return client; } set { client = value; } }
        public string VilleDepart { get { return villeDepart; } set { villeDepart = value; } }
        public string VilleArrivee { get { return villeArrivee; } set { villeArrivee = value; } }
        public Vehicule Vehicule { get { return vehicule; } set { vehicule = value; } }
        public Chauffeur Chauffeur { get { return chauffeur; } set { chauffeur = value; } }
        public DateTime DateCommande { get { return dateCommande; } set { dateCommande = value; } }

        /// <summary>
        /// Avoir le prix de la commande
        /// </summary>
        /// <param name="numeroCommande"></param>
        /// <returns></returns>
        public double CalculerPrixCommande(DataState dataState)
        {
            Noeud<string>? nd = dataState.graphe.verticies.FirstOrDefault(n => n.data == villeDepart);
            Noeud<string>? na = dataState.graphe.verticies.FirstOrDefault(n => n.data == villeArrivee);
            if (nd is not null && na is not null)
            {
                List<Noeud<string>> chemin;
                int distance;
                (chemin, distance) = dataState.graphe.Dijkstra(nd, na);
                double prix = distance * chauffeur.TarifHoraire;
                return prix;
            }
            else
            {
                Console.WriteLine("Impossible de calculer le prix : ville inconnue.");
                return 0;
            }
        }

        /// <summary>
        /// Modification possibles pour une commande : trajet (ville départ et/ou arrivée), date de commande 
        /// </summary>
        /// <param name="numeroCommande"></param>
        public void Modifier_commande(DataState dataState)
        {
            bool continueA = true;
            while (continueA)
            {
                Console.WriteLine("\nCommande actuelle :");
                Console.WriteLine(ToString());
                Console.WriteLine("\nQue souhaitez-vous modifier ?");
                Console.WriteLine("1. Modifier le trajet (ville de départ et d'arrivée)");
                Console.WriteLine("2. Modifier la date de commande");
                Console.WriteLine("3. Appliquer une réduction sur le prix de la commande");
                Console.WriteLine("4. Terminer la modification");
                Console.Write("Entrez votre choix (1-4) : ");

                string choix = Console.ReadLine()!;

                switch (choix)
                {
                    case "1":
                        bool trajetModifie = false;
                        Console.Write("Nouvelle ville de départ (laisser vide pour conserver '" + villeDepart + "') : ");
                        string nvVilleDepart = Console.ReadLine()!;
                        if (nvVilleDepart != "")
                        {
                            trajetModifie = true;
                        }

                        Console.Write("Nouvelle ville d'arrivée (laisser vide pour conserver '" + villeArrivee + "') : ");
                        string nvVilleArrivee = Console.ReadLine()!;
                        if (nvVilleArrivee != "")
                        {
                            trajetModifie = true;
                        }

                        if (trajetModifie)
                        {
                            Noeud<string>? nd = dataState.graphe.verticies.FirstOrDefault(n => n.data == nvVilleDepart);
                            Noeud<string>? na = dataState.graphe.verticies.FirstOrDefault(n => n.data == nvVilleArrivee);
                            if (nd is not null && na is not null)
                            {
                                List<Noeud<string>> nouveauChemin;
                                int nouvelleDistance;
                                villeDepart = nvVilleDepart;
                                villeArrivee = nvVilleArrivee;

                                (nouveauChemin, nouvelleDistance) = dataState.graphe.Dijkstra(nd, na);
                                Console.WriteLine("Nouvelle distance : " + nouvelleDistance + " km");

                                double nouveauPrix = nouvelleDistance * chauffeur.TarifHoraire;
                                Console.WriteLine("Nouveau prix : " + nouveauPrix + " euros");
                            }
                            else
                            {
                            Console.WriteLine("Impossible de recalculer, ville inconnue.");
                            }
                        }
                        break;

                    case "2":
                        DateTime nvdate;
                        Console.Write("Saisir nouvelle date de commande (JJ/MM/AAAA) : ");
                        while (!DateTime.TryParse(Console.ReadLine(), out nvdate)) {
                            Console.WriteLine("Format invalide ! Veuillez réessayer.");
                        }
                        if (nvdate != dateCommande)
                        {
                            bool continueModif = true;
                            Vehicule? v = null;
                            Chauffeur? nvchauffeur = null;
                            if (!vehicule.EstDisponible(dataState, nvdate)) {
                                Console.WriteLine("Le véhicule n'est plus disponible. Veuillez en sélectionner un nouveau");
                                v = Vehicule.SelectionnerVehicule(dataState, nvdate);
                                if (v is null) {
                                    Console.WriteLine("Aucun véhicule disponible, la date ne sera pas modifiée");
                                    continueModif = false;
                                }                                
                            }
                            if (!chauffeur.EstDisponible(dataState, nvdate) && continueModif)
                            {
                                Console.WriteLine("Le chauffeur actuel n'est pas disponible à cette date");
                                nvchauffeur = dataState.chauffeurs.Find(ch => ch.EstDisponible(dataState, nvdate));
                                if (nvchauffeur is null)
                                {
                                    Console.WriteLine("Aucun chauffeur n'est disponible, la date ne sera pas modifié");
                                    continueModif = false;
                                }
                                else
                                {
                                    Console.WriteLine("Nouveau chaffeur assigné : " + nvchauffeur.Nom + " " + nvchauffeur.Prenom);
                                    dateCommande = nvdate;

                                    Noeud<string>? nd = dataState.graphe.verticies.FirstOrDefault(n => n.data == villeDepart);
                                    Noeud<string>? na = dataState.graphe.verticies.FirstOrDefault(n => n.data == villeArrivee);
                                    if (nd is not null && na is not null)
                                    {
                                        List<Noeud<string>> cheminRecalcule;
                                        int distanceRecalculee;
                                        (cheminRecalcule, distanceRecalculee) = dataState.graphe.Dijkstra(nd, na);
                                        double nouveauPrix = distanceRecalculee * nvchauffeur.TarifHoraire;
                                        Console.WriteLine("Nouveau prix après changement de chauffeur : " + nouveauPrix + " euros");
                                    }
                                }
                            }
                            
                            if (continueModif)
                            {
                                dateCommande = nvdate;
                                if (v is not null) vehicule = v;
                                if (nvchauffeur is not null) chauffeur = nvchauffeur;
                            }
                        }
                        break;
                    
                    case "3":
                        try
                        {
                            Console.Write("Quel pourcentage de réduction voulez-vous appliquer ? ");
                            string? saisie = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(saisie))
                                throw new FormatException("La saisie est vide.");

                            if (!float.TryParse(saisie, out float pourcentage))
                                throw new FormatException("La saisie n'est pas un nombre valide.");

                            if (pourcentage <= 0 || pourcentage > 100)
                                throw new ArgumentOutOfRangeException("Le pourcentage doit être entre 1 et 100.");

                            double prixInitial = CalculerPrixCommande(dataState);
                            double montantReduction = prixInitial * (pourcentage / 100);
                            double nvPrix = prixInitial - montantReduction;

                            Console.WriteLine($"Réduction de {pourcentage:F2}% appliquée ({montantReduction:F2} €).");
                            Console.WriteLine($"Le nouveau prix est de {nvPrix:F2} €");
                        }
                        catch (FormatException fe)
                        {
                            Console.WriteLine("Erreur de format : " + fe.Message);
                        }
                        catch (ArgumentOutOfRangeException aoore)
                        {
                            Console.WriteLine("Erreur : " + aoore.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Une erreur inattendue est survenue : " + ex.Message);
                        }
                        break;

                    case "4":
                        continueA = false;
                        break;

                    default:
                        Console.WriteLine("Choix invalide. Veuillez entrer un choix valide (1-3).");
                        break;
                }
            }
        }

        /// <summary>
        /// Afficher le plan de route
        /// </summary>
        /// <param name="numerocommande"></param>
        public void AfficherPlanDeRoute (DataState dataState)
        {
            Noeud<string>? noeudDepart = dataState.graphe.verticies.FirstOrDefault(n => n.data == villeDepart);
            Noeud<string>? noeudArrivee = dataState.graphe.verticies.FirstOrDefault(n => n.data == villeArrivee);

            if (noeudDepart is null || noeudArrivee is null)
            {
                Console.WriteLine("Impossible d'afficher le plan de route : ville inconnue.");
                return;
            } 

            List<Noeud<string>> chemin;
            int a;
            (chemin, a) = dataState.graphe.Dijkstra(noeudDepart, noeudArrivee);

            Console.WriteLine("Plan de route de " + villeDepart+ " à " + villeArrivee);   
            for(int i = 0; i < chemin.Count; i++)
            {
                if (i != 0) Console.WriteLine(" - " + chemin[i].data);
                else Console.WriteLine(chemin[i].data);
            }

            dataState.graphe.drawGraph();
        }

        public void AjouterCommander(DataState dataState) {
            if (!dataState.commandes.Contains(this)) {
                dataState.commandes.Add(this);
                Console.WriteLine("Commande ajoutée avec succès");
            } else {
                Console.WriteLine("Commande déjà présente dans la liste");
            }
        }

        public void SupprimerCommander(DataState dataState) {
            if (!dataState.commandes.Remove(this)) {
                Console.WriteLine("Commande absente de l'historique");
            } else {
                Console.WriteLine("Commande supprimée avec succès");
            }
        }

        /// <summary>
        /// Création d'une commande avec chauffeur, client, ville départ, ville d'arrivée, et un véhicule
        /// </summary>
        /// <param name="client"></param>
        /// <param name="villeDepart"></param>
        /// <param name="villeArrivee"></param>
        /// <param name="dateCommande"></param>
        /// <param name="vehicule"></param>
        public static Commande? Creer_commande(DataState dataState)
        {
            Console.Write("Saisissez le numéro de SS du client qui passe commande : ");
            string numeroSS = Console.ReadLine()!;
            
            Client? clientExistant = Client.RechercherClientNSS(dataState, numeroSS);
            if (clientExistant is null)
            {
                Console.WriteLine("Client non trouvé. Vous devez d'abord créer le client avant de pouvoir créer une commande");
                return null;
            }

            DateTime dateCommande;
            Console.Write("Saisir date de commande (JJ/MM/AAAA) : ");
            while (!DateTime.TryParse(Console.ReadLine(), out dateCommande)) {
                Console.WriteLine("Format invalide ! Veuillez réessayer.");
            }

            Chauffeur? chauffeurSelectionne = dataState.chauffeurs.Find(ch => ch.EstDisponible(dataState, dateCommande));
            if(chauffeurSelectionne is null)
            {
                Console.WriteLine("Aucun chaufeur n'est disponible à la date demandée : "+ dateCommande);
                return null;
            }

            Vehicule? vehiculeSelectionne = Vehicule.SelectionnerVehicule(dataState, dateCommande);
            if(vehiculeSelectionne is null)
            {
                Console.WriteLine("Aucun véhicule n'est disponible à la date demandée : "+ dateCommande);
                return null;
            }

            Console.Write("Saisissez la ville de départ de la commande : ");
            string villeDepart = Console.ReadLine()!;
            Console.Write("Saisissez la ville d'arrivée de la commande : ");
            string villeArrivee = Console.ReadLine()!;

            Noeud<string>? noeudDepart= dataState.graphe.verticies.FirstOrDefault(n => n.data == villeDepart);
            if(noeudDepart is null)
            {
                Console.WriteLine("Ville de départ inconnue : "+ villeDepart);
                return null;
            }

            Noeud<string>? noeudArrivee = dataState.graphe.verticies.FirstOrDefault(n=>n.data==villeArrivee);
            if(noeudArrivee is null)
            {
                Console.WriteLine("Ville d'arrivée inconnue : "+ villeArrivee);
                return null;
            }

            List<Noeud<string>> chemin;
            int distancekm;
            (chemin,distancekm) = dataState.graphe.Dijkstra(noeudDepart,noeudArrivee);

            Console.WriteLine("Distance estimée : "+distancekm+" km");

            Commande commande = new Commande(clientExistant, villeDepart, villeArrivee, vehiculeSelectionne, chauffeurSelectionne, dateCommande);

            Console.WriteLine($"Prix de la commande : {commande.CalculerPrixCommande(dataState):F2} euros");

            Console.WriteLine("Commande créée avec succès");
            return commande;
        }

        public static void AfficherCommandes(DataState dataState) {
            if (dataState.commandes.Count == 0) {
                Console.WriteLine("Aucune commande");
            } else {
                foreach(Commande c in dataState.commandes) {
                    Console.WriteLine(c);
                }
            }
        }

        public static Commande? RechercherCommande(DataState dataState, int numero) {
            return dataState.commandes.Find(c => c.numeroCommande == numero);
        }

        public override string ToString()
        {
            return "Commande : " + numeroCommande + " de " + client.Nom + " " + client.Prenom + "| " + villeDepart + " -> " + villeArrivee + 
            " | Date : " + dateCommande.ToShortDateString() + " | Véhicule : " + vehicule.Immatriculation + " | Chauffeur : " + 
            chauffeur.Nom + " " + chauffeur.Prenom;
        }
    }
}
