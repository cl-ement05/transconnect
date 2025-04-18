namespace transconnect
{
    public class Module_Commande
    {
        private List<Commande> commandes ;
        private List<Client> clients;
        private List<Chauffeur> chauffeurs;
        private Graph<string> graphe;

        public List<Commande> Commandes
        {
            get { return commandes; }
            set { commandes = value; }
        }

        public Module_Commande(List<Client> clients, List<Chauffeur> chauffeurs,Graph<string> graphe)
        {
            this.commandes = new List<Commande>();
            this.clients = clients;
            this.chauffeurs = chauffeurs;
            this.graphe = graphe;
        }

        /// <summary>
        /// Création d'une commande avec chauffeur, client, ville départ, ville d'arrivée, et un véhicule
        /// </summary>
        /// <param name="client"></param>
        /// <param name="villeDepart"></param>
        /// <param name="villeArrivee"></param>
        /// <param name="dateCommande"></param>
        /// <param name="vehicule"></param>
        public void Creer_commande(Client client, string villeDepart, string villeArrivee, DateTime dateCommande, Module_Vehicule vehicule)
        {
            Client? clientExistant = clients.Find(c => c.NumeroSS == client.NumeroSS);
            if (clientExistant is null)
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
            if(chauffeurSelectionne is null)
            {
                Console.WriteLine("Aucun chaufeur n'est disponible à la date demandée : "+ dateCommande);
                return;
            }

            Vehicule vehiculeSelectionne = vehicule.SelectionnerVehicule();

            Noeud<string>? noeudDepart=graphe.verticies.FirstOrDefault(n => n.data == villeDepart);
            if(noeudDepart is null)
            {
                Console.WriteLine("Ville de départ inconnue : "+ villeDepart);
                return;
            }

            Noeud<string>? noeudArrivee=graphe.verticies.FirstOrDefault(n=>n.data==villeArrivee);
            if(noeudArrivee is null)
            {
                Console.WriteLine("Ville d'arrivée inconnue : "+ villeArrivee);
                return;
            }

            List<Noeud<string>> chemin;
            int distancekm;
            (chemin,distancekm)=graphe.Dijkstra(noeudDepart,noeudArrivee);

            Console.WriteLine("Distance estimée : "+distancekm+" km");

            double prix=distancekm * chauffeurSelectionne.TarifHoraire;
            Console.WriteLine("Prix de la commande : "+prix+" €");


            int numeroCommande = commandes.Count + 1;

            Commande commande = new Commande(numeroCommande, clientExistant, villeDepart, villeArrivee, vehiculeSelectionne, chauffeurSelectionne, dateCommande);
            commandes.Add(commande);
            client.HistoriqueCommandes.Add(commande);
            chauffeurSelectionne.LivraisonsEffectuees.Add(commande);

            Console.WriteLine("Commande créée avec succès :");
            Console.WriteLine(commande.ToString());
        
        }

        /// <summary>
        /// Modification possibles pour une commande : trajet (ville départ et/ou arrivée), date de commande 
        /// </summary>
        /// <param name="numeroCommande"></param>
        public void Modifier_commande(int numeroCommande)
        {
            Commande? commande=commandes.Find(c => c.NumeroCommande==numeroCommande);
            if(commande is null)
            {
                Console.WriteLine("Commande introuvable");
                return;
            }

            bool continueA = true;
            while (continueA)
            {
                Console.WriteLine("\nCommande actuelle :");
                Console.WriteLine(commande.ToString());
                Console.WriteLine("\nQue souhaitez-vous modifier ?");
                Console.WriteLine("1. Modifier le trajet (ville de départ et d'arrivée)");
                Console.WriteLine("2. Modifier la date de commande");
                Console.WriteLine("3. Terminer la modification");
                Console.Write("Entrez votre choix (1-3) : ");

                string choix = Console.ReadLine()!;

                if(choix=="1")
                {
                    bool trajetModifie = false;
                    Console.Write("Nouvelle ville de départ (laisser vide pour conserver '" + commande.VilleDepart + "') : ");
                    string nvVilleDepart = Console.ReadLine()!;
                    if (nvVilleDepart != "")
                    {
                        commande.VilleDepart = nvVilleDepart;
                        trajetModifie = true;
                    }

                    Console.Write("Nouvelle ville d'arrivée (laisser vide pour conserver '" + commande.VilleArrivee + "') : ");
                    string nvVilleArrivee = Console.ReadLine()!;
                    if (nvVilleArrivee != "")
                    {
                        commande.VilleArrivee = nvVilleArrivee;
                        trajetModifie = true;
                    }

                    if(trajetModifie)
                    {
                        Noeud<string>? nd = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleDepart);
                        Noeud<string>? na = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleArrivee);
                        if (nd is not null && na is not null)
                        {
                            List<Noeud<string>> nouveauChemin;
                            int nouvelleDistance;
                            (nouveauChemin, nouvelleDistance) = graphe.Dijkstra(nd, na);
                            Console.WriteLine("Nouvelle distance : " + nouvelleDistance + " km");

                            double nouveauPrix = nouvelleDistance * commande.Chauffeur.TarifHoraire;
                            Console.WriteLine("Nouveau prix : " + nouveauPrix + " €");
                        }
                        else
                        {
                            Console.WriteLine("Impossible de recalculer, ville inconnue.");
                        }
                    }
                }
                else if(choix=="2")
                {   
                    Console.Write("Saisir nouvelle date de commande (JJ/MM/AAAA)");
                    string nvDateStr = Console.ReadLine()!;
                    if (nvDateStr != "")
                    {
                        DateTime nvdate=Convert.ToDateTime(nvDateStr);
                        if(nvdate != commande.DateCommande)
                        {
                            if(!commande.Chauffeur.EstDisponible(nvdate))
                            {
                                Console.WriteLine("Le chauffeur actuel n'est pas disponible à cette date");
                                Chauffeur? nvchauffeur=chauffeurs.Find(ch => ch.EstDisponible(nvdate));
                                if(nvchauffeur is null)
                                {
                                    Console.WriteLine("Aucun chauffeur n'est disponible, la date ne sera pas modifié");
                                }
                                else
                                {
                                    Console.WriteLine("Nouveau chaffeur assigné : " + nvchauffeur.Nom + " " + nvchauffeur.Prenom);
                                    commande.Chauffeur.LivraisonsEffectuees.Remove(commande);
                                    commande.Chauffeur=nvchauffeur;
                                    nvchauffeur.LivraisonsEffectuees.Add(commande);
                                    commande.DateCommande=nvdate;

                                    Noeud<string>? nd = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleDepart);
                                    Noeud<string>? na = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleArrivee);
                                    if (nd is not null && na is not null)
                                    {
                                        List<Noeud<string>> cheminRecalcule;
                                        int distanceRecalculee;
                                        (cheminRecalcule, distanceRecalculee) = graphe.Dijkstra(nd, na);
                                        double nouveauPrix = distanceRecalculee * nvchauffeur.TarifHoraire;
                                        Console.WriteLine("Nouveau prix après changement de chauffeur : " + nouveauPrix + " €");
                                    }
                                }
                            } else {
                                commande.DateCommande=nvdate;
                            }
                        }
                    }
                }

                else if (choix == "3")
                {
                    Console.WriteLine("Modification terminée.");
                    continueA = false;
                }
                else
                {
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                }
            }
        }

        /// <summary>
        /// Avoir le prix de la commande
        /// </summary>
        /// <param name="numeroCommande"></param>
        /// <returns></returns>
        public double CalculerPrixCommande(int numeroCommande)
        {
            Commande? commande=commandes.Find(c =>c.NumeroCommande == numeroCommande);
            if(commande is null)
            {
                Console.WriteLine("Commande introuvable");
                return 0;
            }

            Noeud<string>? nd = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleDepart);
            Noeud<string>? na = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleArrivee);
            if (nd is not null && na is not null)
            {
                List<Noeud<string>> chemin;
                int distance;
                (chemin, distance) = graphe.Dijkstra(nd, na);
                double prix = distance * commande.Chauffeur.TarifHoraire;
                return prix;
            }
            else
            {
                Console.WriteLine("Impossible de calculer le prix : ville inconnue.");
                return 0;
            }
        }

        /// <summary>
        /// Afficher le plan de route
        /// </summary>
        /// <param name="numerocommande"></param>
        public void AfficherPlanDeRoute (int numerocommande)
        {
            Commande? commande = commandes.Find(c => c.NumeroCommande == numerocommande);
            if(commande is null)
            {
                Console.WriteLine("Commande introuvable");
                return;
            }

            Noeud<string>? noeudDepart = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleDepart);
            Noeud<string>? noeudArrivee = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleArrivee);

            if (noeudDepart is null || noeudArrivee is null)
            {
                Console.WriteLine("Impossible d'afficher le plan de route : ville inconnue.");
                return;
            } 

            List<Noeud<string>> chemin;
            int a;
            (chemin, a)=graphe.Dijkstra(noeudDepart,noeudArrivee);

            Console.WriteLine("Plant de route de " + commande.VilleDepart+ " à "+commande.VilleArrivee);   
            foreach(Noeud<string> noeud in chemin)
            {
                Console.WriteLine(" - "+noeud.data);
            }

            graphe.drawGraph();
        }
    }
}