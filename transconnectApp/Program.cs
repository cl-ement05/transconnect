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

            DataState dataState;

            Console.Write("Vous pouvez commencer avec une entreprise vide ou bien charger le dernier état sauvegardé " +
            "Que souhaitez-vous faire (vide/charger) ? ");

            string answer = Console.ReadLine()!;
            switch (answer.ToLower()) {
                case "vide":
                    dataState = new DataState(graph);
                    break;
                case "charger":
                    DataState? loaded = DataState.Load(".data.json");
                    if (loaded is null) {
                        Console.WriteLine("Une erreur est survenue lors de la lecture du fichier de données, vous commencerez donc avec une entreprise vide");
                        dataState = new DataState(graph);
                    } else {
                        dataState = loaded;
                        Console.WriteLine("Données chargées avec succès");
                    }
                    // dataState = DataState.predefinedState(graph);
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
                Console.WriteLine("6 : Module Véhicule");
                Console.WriteLine("7 : Afficher le graph des villes");
                Console.WriteLine("8 : Sauvegarder l'état actuel des données");
                Console.WriteLine("9 : Quitter");
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
                        Module_Vehicule(dataState);
                        break;
                    case "7":
                        dataState.graphe.drawGraph();
                        break;
                    case "8":
                        if (dataState.Save(".data.json")) {
                            Console.WriteLine("Sauvegarde effectuée");
                        }
                        break;
                    case "9":
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
                        Client.AfficherParNom(dataState);
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
                        Client.CreerNouveau().AjouterClient(dataState);
                        Console.WriteLine("Client créé avec succés");
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
                            Console.WriteLine("Client supprimé");

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
                            Console.Write($"Veuillez choisir le poste à créer " +
                            $"({Salarie.chefEquipe}, {Salarie.directeur}, {Salarie.chauffeur}) : ");
                            string choice = Console.ReadLine()!;
                            switch (choice) {
                                case Salarie.directeur:
                                    if (dataState.directeur is not null) {
                                        Console.WriteLine("Erreur : il y a déjà un directeur");
                                    } else {
                                        Directeur.CreerNouveau().Embaucher(dataState);
                                    }
                                    break;
                                case Salarie.chefEquipe:
                                    ChefEquipe.CreerNouveau().Embaucher(dataState, dataState.directeur);
                                    break;
                                case Salarie.chauffeur:
                                    Console.WriteLine("Saisissez le numéro de SS du chef du chauffeur : ");
                                    numeroSS = Console.ReadLine()!;
                                    Salarie? chef = Salarie.RechercherSalarieParNumeroSS(dataState, numeroSS);
                                    if (chef is null) {
                                        Console.WriteLine("Chef non trouvé");
                                    } else {
                                        Chauffeur.CreerNouveau().Embaucher(dataState, chef);
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Choix invalide");
                                    break;
                            }
                        } else {
                            Console.WriteLine("Votre entreprise est vide, commencez par créer un directeuré");
                            Directeur.CreerNouveau().Embaucher(dataState);
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
                        dataState.organigramme.Afficher(dataState.organigramme.Racine); 
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
                Console.WriteLine("5 : Lister les commandes");
                Console.WriteLine("6 : Revenir au menu principal");

                string nbr = Console.ReadLine()!;
                switch(nbr) {
                    case "1":
                        Commande? c = Commande.Creer_commande(dataState);
                        if (c is not null) {
                            c.AjouterCommander(dataState);
                        }
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
                        Commande.AfficherCommandes(dataState);
                        break;
                    case "6":
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
                        Reclamation? c = Reclamation.CreerReclamation(dataState);
                        if (c is not null) {
                            c.AjouterReclamation(dataState);
                        }
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
                                Console.WriteLine("Réclamation non trouvé");
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
                                Console.WriteLine("Réclamation non trouvé");
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

        public static void Module_Vehicule(DataState dataState) {
            bool continueApp = true;
            while (continueApp) {
                Console.WriteLine("\nModule véhicule");
                Console.WriteLine("1 : Afficher liste des véhicules");
                Console.WriteLine("2 : Créer un véhicule");
                Console.WriteLine("3 : Supprimer un véhicule");
                Console.WriteLine("4 : Changer un statut de véhicule");
                Console.WriteLine("5 : Revenir au menu principal");

                string nbr = Console.ReadLine()!;
                string immat = "";
                switch(nbr) {
                    case "1":
                        Vehicule.AfficherVehicules(dataState);
                        break;
                    case "2":
                        Console.WriteLine("Veuillez choisir le type de véhicule à créer : \n1. Voiture\n2. Camionette\n3. Camion Benne\n4." +
                        " Camion Citerne\n5. Camion Frigorifique");
                        string choice = Console.ReadLine()!;
                        switch(choice) {
                            case "1":
                                Voiture.CreerNouveau().AjouterVehicule(dataState);
                                break;
                            case "2":
                                Camionette.CreerNouveau().AjouterVehicule(dataState);
                                break;
                            case "3":
                                CamionBenne.CreerNouveau().AjouterVehicule(dataState);
                                break;
                            case "4":
                                CamionCiterne.CreerNouveau().AjouterVehicule(dataState);
                                break;
                            case "5":
                                CamionFrigorifique.CreerNouveau().AjouterVehicule(dataState);
                                break;
                            default:
                                Console.WriteLine("Choix invalide");
                                break;
                        }
                        break;
                    case "3":
                        Console.WriteLine("Saisissez l'immat du véhicule à supprimer : ");
                        immat = Console.ReadLine()!;
                        Vehicule? v = Vehicule.RechercherVehicule(dataState, immat);
                        if (v is null) {
                            Console.WriteLine("Véhicule non trouvé");
                        } else {
                            v.SupprimerVehicule(dataState);
                        }
                        break;
                    case "4":
                        Console.WriteLine("Saisissez l'immat du véhicule à modifier : ");
                        immat = Console.ReadLine()!;
                        v = Vehicule.RechercherVehicule(dataState, immat);
                        if (v is null) {
                            Console.WriteLine("Véhicule non trouvé");
                        } else {
                            v.ChangerStatut(dataState);

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

        public static int ParseInt(string prompt) {
            int nb = 0;
            bool valid = false;
            while (!valid)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()!;
                try
                {
                    nb = Convert.ToInt32(input);
                    valid = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Format invalide ! Veuillez réessayer.");
                }
            }
            return nb;
        }
    }
}
