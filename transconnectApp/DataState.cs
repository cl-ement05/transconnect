using Newtonsoft.Json;

namespace transconnect {
    /// <summary>
    /// Data holder class
    /// </summary>
    public class DataState {
        public List<Commande> commandes { get; set; }
        public List<Reclamation> reclamations { get; set; }
        public List<Client> clients { get; set; }
        public List<Salarie> salaries { get; set; }
        public Organigramme organigramme { get; set; }
        public Graph<string> graphe { get; set; }
        public List<Vehicule> flotte { get; set; }

        public List<Chauffeur> chauffeurs {
            get {
                List<Chauffeur> chauffeurs = new List<Chauffeur>();
                foreach (Salarie salarie in salaries) {
                    if (salarie.GetType() == typeof(Chauffeur)) {
                        chauffeurs.Add((Chauffeur)salarie);
                    }
                }
                return chauffeurs;
            }
        }

        public Directeur? directeur {
            get {
                foreach (Salarie salarie in salaries) {
                    if (salarie.GetType() == typeof(Directeur)) {
                        return (Directeur) salarie;
                    }
                }
                return null;
            }
        }


        public DataState(Graph<string> graph) {
            commandes = new List<Commande>();
            clients = new List<Client>();
            salaries = new List<Salarie>();
            organigramme = new Organigramme();
            flotte = new List<Vehicule>();
            reclamations = new List<Reclamation>();
            this.graphe = graph;
        }

        public bool Save(string path) {
            try {
                JsonSerializerSettings settings = new() { Formatting = Formatting.Indented, TypeNameHandling = TypeNameHandling.All };
                string json = JsonConvert.SerializeObject(this, settings);
                File.WriteAllText(path, json);
                return true;
            } catch (Exception) {
                Console.WriteLine("Une erreur est survenue, veuillez réessayer");
                return false;
            }
        }

        public static DataState? Load(string path) {
            try {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<DataState>(json, new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All});
            } catch (IOException e) {
                Console.WriteLine(e.Message);
                return null;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Returns a datastate object filled with predefined vars
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static DataState predefinedState(Graph<string> graph) {
            
            DataState dataState = new DataState(graph);
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

            dataState.commandes.Add(commande1);
            dataState.commandes.Add(commande2);
            dataState.commandes.Add(commande3);
            
            return dataState;
        }
    }
}
