using System.Diagnostics;

namespace transconnect {
    public class Program {

        public static void test_Module_Salarie()
        {
            Module_Salarie moduleSalarie = new Module_Salarie();

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

            moduleSalarie.EmbaucherSalarie(directeur); 
            moduleSalarie.EmbaucherSalarie(chefEquipe, directeur); 
            moduleSalarie.EmbaucherSalarie(chauffeur1, chefEquipe); 
            moduleSalarie.EmbaucherSalarie(chauffeur2, chefEquipe);


            Console.WriteLine("Organigramme après embauche :");
            moduleSalarie.AfficherOrganigramme();
             

            // Rechercher un salarié par son numéro de sécurité sociale
            Console.WriteLine("\nRecherche d'un salarié par numéro de sécurité sociale :");
            Salarie? salarieRecherche = moduleSalarie.RechercherSalarieParNumeroSS("987654321");
            if (salarieRecherche is not null)
            {
                Console.WriteLine($"Salarié trouvé : {salarieRecherche.ToString()}");
            }
            else
            {
                Console.WriteLine("Salarié introuvable.");
            }

             

            // Modifier les informations d'un salarié
            Console.WriteLine("\nModification des informations d'un salarié :");
            moduleSalarie.ModifierSalarieParNumeroSS("987654321", "Martin", "Paul", new DateTime(1990, 8, 15),
                                                     "25 avenue des Roses", "paul.martin@newmail.com", "0611223344", 3000);


             

            // Afficher la liste des salariés après modification
            Console.WriteLine("\nListe des salariés après modification :");
            moduleSalarie.AfficherSalaries();
             
            // Licencier un salarié
            Console.WriteLine("\nLicenciement d'un salarié :");
            moduleSalarie.LicencierSalarie(chauffeur1);
             
            // Afficher l'organigramme après licenciement
            Console.WriteLine("\nOrganigramme après licenciement :");
            moduleSalarie.AfficherOrganigramme(); 
                       

            
        }

        public static void test_Module_Client()
        {
            // Création du module des clients
            Module_Client moduleClient = new Module_Client();

            // Création de clients
            Client client1 = new Client("123456789", "Dupont", "Jean", new DateTime(1980, 5, 20),
                                        "10 rue des Lilas, Paris", "jean.dupont@example.com", "0601020304");

            Client client2 = new Client("987654321", "Martin", "Paul", new DateTime(1990, 8, 15),
                                        "20 avenue des Roses, Lyon", "paul.martin@example.com", "0605060708");

            Client client3 = new Client("456789123", "Durand", "Alice", new DateTime(1995, 3, 10),
                                        "30 boulevard des Champs, Marseille", "alice.durand@example.com", "0612345678");

            // Ajouter des clients
            moduleClient.AjouterClient(client1);
            moduleClient.AjouterClient(client2);
            moduleClient.AjouterClient(client3);

            // Afficher la liste des clients
            Console.WriteLine("Liste des clients après ajout :");
            moduleClient.AfficherClients();

            // Rechercher un client par numéro de sécurité sociale
            Console.WriteLine("\nRecherche d'un client par numéro de sécurité sociale :");
            Client? clientRecherche = moduleClient.RechercherClientNSS("987654321");
            if (clientRecherche is not null)
            {
                Console.WriteLine($"Client trouvé : {clientRecherche.ToString()}");
            }
            else
            {
                Console.WriteLine("Client introuvable.");
            }

            // Modifier les informations d'un client
            Console.WriteLine("\nModification des informations d'un client :");
            moduleClient.ModifierClientParNumeroSS("987654321", "Martin", "Paul", new DateTime(1990, 8, 15),
                                                   "25 avenue des Roses, Lyon", "paul.martin@newmail.com", "0611223344");

            // Afficher la liste des clients après modification
            Console.WriteLine("\nListe des clients après modification :");
            moduleClient.AfficherClients();

            // Supprimer un client
            Console.WriteLine("\nSuppression d'un client :");
            moduleClient.SupprimerClient(client1);

            // Afficher la liste des clients après suppression
            Console.WriteLine("\nListe des clients après suppression :");
            moduleClient.AfficherClients();

            // Trier les clients par nom
            Console.WriteLine("\nClients triés par nom :");
            moduleClient.AfficherParNom();

            // Trier les clients par ville
            Console.WriteLine("\nClients triés par ville (Lyon) :");
            moduleClient.AfficherParVille("Lyon");

            // Trier les clients par montant de commande
            Console.WriteLine("\nClients triés par montant de commande :");
            Graph<string> graph = new Graph<string>(new Dictionary<string, List<(string, int)>>());
            Module_Commande moduleCommande = new Module_Commande(new List<Client> { client2, client3 }, new List<Chauffeur>(), graph);
            moduleClient.AfficherParMontantCommande(moduleCommande);
        }

        public static void test_Module_Statut_Vehicule()
        {
            // Création de véhicules
            Vehicule vehicule1 = new Voiture("AB-123-CD", "Rouge", "Renault",3);
            Vehicule vehicule2 = new Camionette("EF-456-GH", "Bleu", "Mercedes","Transport de marchandises");
            Vehicule vehicule3 = new CamionFrigorifique("IJ-789-KL", "Noir", "Peugeot", 1000, 100);

            // Création du module des statuts des véhicules
            Module_Statut_Vehicule moduleStatutVehicule = new Module_Statut_Vehicule();

            // Ajouter des véhicules avec leurs statuts
            moduleStatutVehicule.AjouterVehicule(vehicule1, "Disponible");
            moduleStatutVehicule.AjouterVehicule(vehicule2, "En maintenance");
            moduleStatutVehicule.AjouterVehicule(vehicule3, "En cours d'utilisation");

            // Afficher les statuts des véhicules
            Console.WriteLine("Statuts des véhicules après ajout :");
            moduleStatutVehicule.AfficherStatuts();

            // Mettre à jour le statut d'un véhicule
            moduleStatutVehicule.MettreAJourStatut(vehicule1, "En cours d'utilisation");
            moduleStatutVehicule.MettreAJourStatut(vehicule2, "Disponible");

            // Obtenir le statut d'un véhicule
            Console.WriteLine($"\nStatut du véhicule {vehicule1.Immatriculation} : {moduleStatutVehicule.ObtenirStatut(vehicule1)}");
            Console.WriteLine($"Statut du véhicule {vehicule2.Immatriculation} : {moduleStatutVehicule.ObtenirStatut(vehicule2)}");

            // Afficher les statuts après mise à jour
            Console.WriteLine("\nStatuts des véhicules après mise à jour :");
            moduleStatutVehicule.AfficherStatuts();
        }
        public static void clement()
        {
                int[,] data = { 
                {0, 3, 5, 0, 0, 0},
                {3, 0, 1, 0, 0, 6},
                {5, 1, 0, 2, 0, 0},
                {0, 0, 2, 0, 2, 0},
                {0, 0, 0, 2, 0, 0},
                {0, 6, 0, 0, 0, 0} };
            char[] labels = {'A', 'B', 'C', 'D', 'E', 'F'};
            Graph<char> graph = new Graph<char>(data, labels);
            Dictionary<char, List<(char data, int weight)>> dict = new Dictionary<char, List<(char data, int weight)>>() {
                {'A', [('B', 3), ('C', 5)]},
                {'B', [('A', 3), ('C', 7), ('F', 6)]},
                {'C', [('A', 5), ('B', 7), ('D', 2)]},
                {'D', [('C', 2), ('E', 3)]},
                {'E', [('D', 3)]},
                {'F', [('B', 6)]},
            };
            Graph<char> graph2 = new Graph<char>(dict);
            Noeud<char> noeud = graph2.verticies.Find((e) => e.data == 'A')!;
            Noeud<char> noeud2 = graph2.verticies.Find((e) => e.data == 'E')!;
            int var = graph2.FloydWarshall(noeud, noeud2);
            Console.WriteLine(var);
        }
        public static void Main(string[] args) 
        {
            
            //test_Module_Salarie();
            //test_Module_Client();
            //test_Module_Statut_Vehicule();
        }
    }
}
