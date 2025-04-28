namespace transconnect {
    public class Program {        
        public static void Main(string[] args) 
        {
            Console.WriteLine("Bienvenue sur transconnect");
            Console.Write("Veuillez écrire le nom COMPLET du fichier CSV contenant les infos sur les villes : ");
            string path = Console.ReadLine()!;

            Graph<string>? graph = Graph<string>.createGraphFromCSV(path);
            if (graph is null) {
                //error message already printed in method
                Console.WriteLine("");
                string[] files = Directory.GetFiles(Environment.CurrentDirectory, "*.csv", SearchOption.AllDirectories);
                if (files.Length > 0) {
                    Console.WriteLine("Liste des fichiers .CSV trouvés dans le répertoire :");
                    foreach (string file in files) {
                        Console.WriteLine(file);
                    }
                    Console.WriteLine("Veuillez réessayer");
                    return;
                } else {
                    Console.WriteLine("Aucun fichier CSV dans le répertoire courant");
                    Console.WriteLine("Veuillez réessayer");
                    return;
                }
            }

            DataState dataState = new DataState(graph);

            Console.WriteLine("Vous pouvez commencer avec une entreprise vide ou bien charger quelques donnée pré-définies." +
            "Que souhaitez-vous faire ? (vide/charger)");

            string answer = Console.ReadLine()!;
            switch (answer.ToLower()) {
                case "vide":

                    break;
                case "charger":
                    Directeur directeur = new Directeur("123456789", "Dupont", "Jean", new DateTime(1980, 5, 20),
                                                "10 rue des Lilas", "jean.dupont@example.com", "0601020304",
                                                new DateTime(2010, 1, 1), 5000);

                    Chauffeur chauffeur1 = new Chauffeur("987654321", "Martin", "Paul", new DateTime(1990, 8, 15),
                                                        "20 avenue des Roses", "paul.martin@example.com", "0605060708",
                                                        new DateTime(2015, 6, 1), 2500);

                    Chauffeur chauffeur2 = new Chauffeur("456789123", "Durand", "Alice", new DateTime(1995, 3, 10),
                                                        "30 boulevard des Champs", "alice.durand@example.com", "0612345678",
                                                        new DateTime(2020, 9, 1), 2000);

                    ChefEquipe chefEquipe = new ChefEquipe("789123456", "Lemoine", "Claire", new DateTime(1985, 7, 25),
                                                        "50 rue des Fleurs", "claire.lemoine@example.com", "0611223344",
                                                        new DateTime(2018, 4, 15), 3000);

                    Chauffeur chauffeur3 = new Chauffeur("123456789", "Garnier", "Luc", new DateTime(1988, 11, 3),
                                    "40 impasse des Tilleuls", "luc.garnier@example.com", "0622334455",
                                    new DateTime(2016, 2, 10), 2800);

                    Chauffeur chauffeur4 = new Chauffeur("987654321", "Rousseau", "Marie", new DateTime(1992, 4, 18),
                                                        "60 allée des Saules", "marie.rousseau@example.com", "0633445566",
                                                        new DateTime(2019, 7, 22), 2200);

                    ChefEquipe chefEquipe2 = new ChefEquipe("456789123", "Dubois", "Pierre", new DateTime(1983, 9, 12),
                                                            "70 rue des Peupliers", "pierre.dubois@example.com", "0644556677",
                                                            new DateTime(2017, 5, 1), 3200);


                    directeur.Embaucher(dataState); 
                    chefEquipe.Embaucher(dataState, directeur);
                    chauffeur1.Embaucher(dataState, chefEquipe);
                    chauffeur2.Embaucher(dataState, chefEquipe);
                    chefEquipe2.Embaucher(dataState, directeur);
                    chauffeur4.Embaucher(dataState, chefEquipe2);


                    // Création de clients
                    Client client1 = new Client("123456789", "Dupont", "Jean", new DateTime(1980, 5, 20),
                                                "10 rue des Lilas, Paris", "jean.dupont@example.com", "0601020304");

                    Client client2 = new Client("987654321", "Martin", "Paul", new DateTime(1990, 8, 15),
                                                "20 avenue des Roses, Lyon", "paul.martin@example.com", "0605060708");

                    Client client3 = new Client("456789123", "Durand", "Alice", new DateTime(1995, 3, 10),
                                                "30 boulevard des Champs, Marseille", "alice.durand@example.com", "0612345678");

                    // Ajouter des clients
                    client1.AjouterClient(dataState);
                    client2.AjouterClient(dataState);
                    client3.AjouterClient(dataState);

                    // Création de véhicules
                    Vehicule vehicule1 = new Voiture("AB-123-CD", "Rouge", "Renault",3);
                    Vehicule vehicule2 = new Camionette("EF-456-GH", "Bleu", "Mercedes","Transport de marchandises", Vehicule.vehiculeMaintenance);
                    Vehicule vehicule3 = new CamionFrigorifique("IJ-789-KL", "Noir", "Peugeot", 1000, 100, Vehicule.vehiculeOccupe);

                    vehicule1.AjouterVehicule(dataState);
                    vehicule2.AjouterVehicule(dataState);
                    vehicule3.AjouterVehicule(dataState);

                    Commande commande1 = new Commande(client1, "Paris", "Lyon", vehicule1, chauffeur1, new DateTime(2023, 10, 1));
                    Commande commande2 = new Commande(client2, "Marseille", "Toulouse", vehicule1, chauffeur2, new DateTime(2023, 10, 5));
                    Commande commande3 = new Commande(client3, "Bordeaux", "Nantes", vehicule1, chauffeur3, new DateTime(2023, 10, 10));
                    break;
                default:
                    Console.WriteLine("Choix invalide");
                    Console.WriteLine("Veuillez réessayer");
                    return;
            }

            bool continueApp = true;
            while (continueApp) {
                Console.WriteLine("\nMenu principal");
                Console.WriteLine("1 : Module Client");
                Console.WriteLine("2 : Module Salarié");
                Console.WriteLine("3 : Module Commande");
                Console.WriteLine("4 : Module Stats");
                Console.WriteLine("5 : Module Réclamation");
                Console.WriteLine("6 : Afficher le graph des villes");
                Console.WriteLine("7 : Quitter");
                Console.Write("Faites votre choix : ");
                string nbr = Console.ReadLine()!;
                
                switch (nbr) {
                    case "1":
                        Module_Client(dataState);
                        break;
                    case "2":
                        Module_Salarie(dataState);
                        break;
                    case "3":
                        Module_Commande(dataState);
                        break;
                    case "4":
                        Module_Stat(dataState);
                        break;
                    case "5":
                        Module_Reclamation(dataState);
                        break;
                    case "6":
                        dataState.graphe.drawGraph();
                        break;
                    case "7":
                        continueApp = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }                    
        }

        public static void Module_Client(DataState dataState) {
            bool continueApp = true;
            while (continueApp) {
                Console.WriteLine("\nModule client");
                Console.WriteLine("1 : Affichage par ordre alphabétique");
                Console.WriteLine("2 : Affichage par ville");
                Console.WriteLine("3 : Affichage par montant d'achat cumulés");
                Console.WriteLine("4 : Modification de client");
                Console.WriteLine("5 : Créer un client");
                Console.WriteLine("6 : Trajets favoris d'un client");
                Console.WriteLine("7 : Supprimer un client");
                Console.WriteLine("8 : Revenir au menu principal");

                string nbr = Console.ReadLine()!;
                string numeroSS = "";
                switch(nbr) {
                    case "1":
                        Client.AfficherClients(dataState);
                        break;
                    case "2":
                        Console.Write("Quelle ville souhaitez-vous chercher : ");
                        string ville = Console.ReadLine()!;
                        Client.AfficherParVille(dataState, ville);
                        break;
                    case "3":
                        Client.AfficherParMontantCommande(dataState);
                        break;
                    case "4":
                        Console.WriteLine("Saisissez le numéro de SS du client à modifier : ");
                        numeroSS = Console.ReadLine()!;
                        Client? client = Client.RechercherClientNSS(dataState, numeroSS);
                        if (client is null) {
                            Console.WriteLine("Client non trouvé");
                        } else {
                            client.ModifierInfos();
                        }
                        break;
                    case "5":
                        dataState.clients.Add(Client.CreerNouveau(dataState));
                        break;
                    case "6":
                        Console.WriteLine("Saisissez le numéro de SS du client cherché : ");
                        numeroSS = Console.ReadLine()!;
                        client = Client.RechercherClientNSS(dataState, numeroSS);
                        if (client is null) {
                            Console.WriteLine("Client non trouvé");
                        } else {
                            client.AfficherTrajetsFavoris(dataState);
                        }
                        break;
                    case "7":
                        Console.WriteLine("Saisissez le numéro de SS du client à supprimer : ");
                        numeroSS = Console.ReadLine()!;
                        client = Client.RechercherClientNSS(dataState, numeroSS);
                        if (client is null) {
                            Console.WriteLine("Client non trouvé");
                        } else {
                            client.SupprimerClient(dataState);
                        }
                        break;
                    case "8":
                        continueApp = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
        }

        public static void Module_Salarie(DataState dataState) {
            bool continueApp = true;
            while (continueApp) {
                Console.WriteLine("\nModule salarié");
                Console.WriteLine("1 : Embaucher un salarié");
                Console.WriteLine("2 : Licencier un salarié");
                Console.WriteLine("3 : Modifier un salarié");
                Console.WriteLine("4 : Afficher l'organigramme");
                Console.WriteLine("5 : Revenir au menu principal");

                string nbr = Console.ReadLine()!;
                string numeroSS = "";
                switch(nbr) {
                    case "1":
                        if (dataState.directeur is not null) {
                            Console.Write(@$"Veuillez choisir le poste à créer 
                            ({Salarie.chefEquipe}, {Salarie.directeur}, {Salarie.chauffeur}) : ");
                            string choice = Console.ReadLine()!;
                            switch (choice) {
                                case Salarie.directeur:
                                    if (dataState.directeur is not null) {
                                        Console.WriteLine("Erreur : il y a déjà un directeur");
                                    } else {
                                        Directeur.CreerNouveau(dataState).Embaucher(dataState);
                                    }
                                    break;
                                case Salarie.chefEquipe:
                                    ChefEquipe.CreerNouveau(dataState).Embaucher(dataState, dataState.directeur);
                                    break;
                                case Salarie.chauffeur:
                                    Console.WriteLine("Saisissez le numéro de SS du chef du chauffeur : ");
                                    numeroSS = Console.ReadLine()!;
                                    Salarie? chef = Salarie.RechercherSalarieParNumeroSS(dataState, numeroSS);
                                    if (chef is null) {
                                        Console.WriteLine("Chef non trouvé");
                                    } else {
                                        Chauffeur.CreerNouveau(dataState).Embaucher(dataState, chef);
                                    }
                                    break;
                            }
                        } else {
                            Console.WriteLine("Votre entreprise est vide, commencez par créer un directeuré");
                            Directeur.CreerNouveau(dataState).Embaucher(dataState);
                        }
                        break;
                    case "2":
                        Console.WriteLine("Saisissez le numéro de SS du salarié à licencier : ");
                        numeroSS = Console.ReadLine()!;
                        Salarie? salarie = Salarie.RechercherSalarieParNumeroSS(dataState, numeroSS);
                        if (salarie is null) {
                            Console.WriteLine("Salarié non trouvé");
                        } else {
                            salarie.Licencier(dataState);
                        }
                        break;
                    case "3":
                        Console.WriteLine("Saisissez le numéro de SS du salarié à modifier : ");
                        numeroSS = Console.ReadLine()!;
                        salarie = Salarie.RechercherSalarieParNumeroSS(dataState, numeroSS);
                        if (salarie is null) {
                            Console.WriteLine("Salarié non trouvé");
                        } else {
                            salarie.ModifierInfos();
                        }
                        break;
                    case "4":
                        dataState.organigramme.Afficher();
                        break;
                    case "5":
                        continueApp = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
        }

        public static void Module_Commande(DataState dataState) {
            bool continueApp = true;
            while (continueApp) {
                Console.WriteLine("\nModule commande");
                Console.WriteLine("1 : Créer une commande");
                Console.WriteLine("2 : Modifier une commande");
                Console.WriteLine("3 : Calculer plan de route");
                Console.WriteLine("4 : Calculer prix");
                Console.WriteLine("5 : Revenir au menu principal");

                string nbr = Console.ReadLine()!;
                switch(nbr) {
                    case "1":
                        Commande.Creer_commande(dataState);
                        break;
                    case "2":
                        Console.WriteLine("Saisissez le numéro de la commande à modifier : ");
                        string numero = Console.ReadLine()!;
                        try {
                            int nb = int.Parse(numero);
                            Commande? commande = Commande.RechercherCommande(dataState, nb);
                            if (commande is null) {
                                Console.WriteLine("Commande non trouvé");
                            } else {
                                commande.Modifier_commande(dataState);
                            }
                        } catch (Exception) {
                            Console.WriteLine("Numéro invalide");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Saisissez le numéro de la commande pour laquelle vous voulez le plan : ");
                        numero = Console.ReadLine()!;
                        try {
                            int nb = int.Parse(numero);
                            Commande? commande = Commande.RechercherCommande(dataState, nb);
                            if (commande is null) {
                                Console.WriteLine("Commande non trouvé");
                            } else {
                                commande.AfficherPlanDeRoute(dataState);
                            }
                        } catch (Exception) {
                            Console.WriteLine("Numéro invalide");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Saisissez le numéro de la commande pour laquelle vous voulez le prix : ");
                        numero = Console.ReadLine()!;
                        try {
                            int nb = int.Parse(numero);
                            Commande? commande = Commande.RechercherCommande(dataState, nb);
                            if (commande is null) {
                                Console.WriteLine("Commande non trouvé");
                            } else {
                                Console.WriteLine($"Prix de la commande : {commande.CalculerPrixCommande(dataState)}");
                            }
                        } catch (Exception) {
                            Console.WriteLine("Numéro invalide");
                        }
                        break;
                    case "5":
                        continueApp = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
        }

        public static void Module_Stat(DataState dataState) {
            bool continueApp = true;
            Module_statistiques stats = new Module_statistiques(dataState);
            while (continueApp) {
                Console.WriteLine("\nModule statistiques");
                Console.WriteLine("1 : Afficher livraisons par chauffeur");
                Console.WriteLine("2 : Afficher commandes par période de temps");
                Console.WriteLine("3 : Afficher moyenne des prix des commandes");
                Console.WriteLine("4 : Afficher la moyenne des comptes clients");
                Console.WriteLine("5 : Revenir au menu principal");

                string nbr = Console.ReadLine()!;
                switch(nbr) {
                    case "1":
                        stats.AfficherLivraisonsParChauffeur();
                        break;
                    case "2":
                        Console.Write("Date de début de période (JJ/MM/AAAA) : ");
                        DateTime debut;
                        DateTime fin;
                        while (!DateTime.TryParse(Console.ReadLine(), out debut)) {
                            Console.WriteLine("Format invalide ! Veuillez réessayer.");
                        }
                        Console.Write("Date de fin de période (JJ/MM/AAAA) : ");
                        while (!DateTime.TryParse(Console.ReadLine(), out fin)) {
                            Console.WriteLine("Format invalide ! Veuillez réessayer.");
                        }
                        stats.AfficherCommandesParPeriode(debut, fin);
                        break;
                    case "3":
                        stats.AfficherMoyennePrixCommandes();
                        break;
                    case "4":
                        stats.AfficherMoyenneComptesClients();
                        break;
                    case "5":
                        continueApp = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
        }

        public static void Module_Reclamation(DataState dataState) {
            bool continueApp = true;
            while (continueApp) {
                Console.WriteLine("\nModule réclamation");
                Console.WriteLine("1 : Créer réclamation");
                Console.WriteLine("2 : Afficher liste des réclamations");
                Console.WriteLine("3 : Rejeter réclamation");
                Console.WriteLine("4 : Résoudre réclamation");
                Console.WriteLine("5 : Revenir au menu principal");

                string nbr = Console.ReadLine()!;
                switch(nbr) {
                    case "1":
                        Reclamation.CreerReclamation(dataState);
                        break;
                    case "2":
                        Reclamation.AfficherReclamations(dataState);
                        break;
                    case "3":
                        Console.WriteLine("Saisissez le numéro de la réclamation que vous souhaitez rejeter : ");
                        string numero = Console.ReadLine()!;
                        try {
                            int nb = int.Parse(numero);
                            Reclamation? reclamation = Reclamation.RechercherReclamation(dataState, nb);
                            if (reclamation is null) {
                                Console.WriteLine("Commande non trouvé");
                            } else {
                                Console.Write("Veuillez écrire le motif de rejet : ");
                                reclamation.MarquerCommeRejetee(Console.ReadLine()!);
                            }
                        } catch (Exception) {
                            Console.WriteLine("Numéro invalide");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Saisissez le numéro de la réclamation que vous souhaitez résoudre : ");
                        numero = Console.ReadLine()!;
                        try {
                            int nb = int.Parse(numero);
                            Reclamation? reclamation = Reclamation.RechercherReclamation(dataState, nb);
                            if (reclamation is null) {
                                Console.WriteLine("Commande non trouvé");
                            } else {
                                Console.Write("Veuillez écrire la réponse pour le client : ");
                                reclamation.MarquerCommeResolue(Console.ReadLine()!);
                            }
                        } catch (Exception) {
                            Console.WriteLine("Numéro invalide");
                        }
                        break;
                    case "5":
                        continueApp = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
        }
    }
}
