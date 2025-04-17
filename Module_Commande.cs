namespace transconnect
{
    public class Module_Commande
    {
        private List<Commande> commandes ;
        private List<Client> clients;
        private List<Chauffeur> chauffeurs;
        private Graph<string> graphe;

        public Module_Commande(List<Client> clients, List<Chauffeur> chauffeurs,Graph<string> graphe)
        {
            this.commandes = new List<Commande>();
            this.clients = clients;
            this.chauffeurs = chauffeurs;
            this.graphe = graphe;
        }

        public void Creer_commande(Client client, string villeDepart, string villeArrivee, DateTime dateCommande, Chauffeur chauffeur, decimal tarifHoraire, Module_Vehicule vehicule)
        {
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

            Vehicule vehiculeSelectionne = vehicule.SelectionnerVehicule();

            Noeud<string>? noeudDepart=graphe.verticies.FirstOrDefault(n => n.data == villeDepart);
            if(noeudDepart == null)
            {
                Console.WriteLine("Ville de départ inconnue : "+ villeDepart);
                return;
            }

            Noeud<string>? noeudArrivee=graphe.verticies.FirstOrDefault(n=>n.data==villeArrivee);
            if(noeudArrivee==null)
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

        public void Modifier_commande(int numeroCommande)
        {
            Commande? commande=commandes.Find(c => c.NumeroCommande==numeroCommande);
            if(commande == null)
            {
                Console.WriteLine("Commande introuvable");
                return;
            }

            while (true)
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
                        if (nd != null && na != null)
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
                                if(nvchauffeur==null)
                                {
                                    Console.WriteLine("Aucun chauffeur n'est disponible, la date ne sera pas modifié");
                                }
                                else
                                {
                                    Console.WriteLine("Nouveau chaffeur assigné : " + nvchauffeur.Nom + " " + nvchauffeur.Prenom);
                                    commande.Chauffeur.LivraisonsEffectuees.Remove(commande);
                                    commande.Chauffeur=nvchauffeur;
                                    nvchauffeur.LivraisonsEffectuees.Add(commande);

                                    Noeud<string>? nd = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleDepart);
                                    Noeud<string>? na = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleArrivee);
                                    if (nd != null && na != null)
                                    {
                                        List<Noeud<string>> cheminRecalcule;
                                        int distanceRecalculee;
                                        (cheminRecalcule, distanceRecalculee) = graphe.Dijkstra(nd, na);
                                        double nouveauPrix = distanceRecalculee * nvchauffeur.TarifHoraire;
                                        Console.WriteLine("Nouveau prix après changement de chauffeur : " + nouveauPrix + " €");
                                    }
                                }
                            }
                        }
                        else
                        {
                            commande.DateCommande=nvdate;
                        }
                    }
                }

                else if (choix == "3")
                {
                    Console.WriteLine("Modification terminée.");
                    break;
                }
                else
                {
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                }
            }
        }

        public void AfficherPrixCommande(int numeroCommande)
        {
            Commande? commande=commandes.Find(c =>c.NumeroCommande == numeroCommande);
            if(commande == null)
            {
                Console.WriteLine("Commande introuvable");
                return;
            }

            Noeud<string>? nd = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleDepart);
            Noeud<string>? na = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleArrivee);
            if (nd != null && na != null)
            {
                List<Noeud<string>> chemin;
                int distance;
                (chemin, distance) = graphe.Dijkstra(nd, na);
                double prix = distance * commande.Chauffeur.TarifHoraire;
                Console.WriteLine("Le prix de la commande " + numeroCommande + " est de " + prix + " €.");
            }
            else
            {
                Console.WriteLine("Impossible de calculer le prix : ville inconnue.");
            }
        }

        public void AfficherPlanDeRoute (int numerocommande)
        {
            Commande? commande = commandes.Find(c => c.NumeroCommande == numerocommande);
            if(commande == null)
            {
                Console.WriteLine("Commande introuvable");
                return;
            }

            Noeud<string>? noeudDepart = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleDepart);
            Noeud<string>? noeudArrivee = graphe.verticies.FirstOrDefault(n => n.data == commande.VilleArrivee);

            if (noeudDepart == null || noeudArrivee == null)
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

            GraphDrawer<string> drawer = new GraphDrawer<string>(graphe);
            Application.Run(drawer);
        }
    }
}