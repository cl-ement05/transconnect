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

                    /*if(trajetModifie)
                    {
                        // Redéterminer le nouveau parcours
                        // Recalculer le prix
                    }*/
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
    }
}