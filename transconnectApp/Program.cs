namespace transconnect {
    public class Program {        
        public static void Main(string[] args) 
        {
            Dictionary<string, List<(string data, int weight)>> dict = new Dictionary<string, List<(string data, int weight)>>() {
                {"A", [("B", 3), ("C", 5)]},
                {"B", [("A", 3), ("C", 7), ("F", 6)]},
                {"C", [("A", 5), ("B", 7), ("D", 2)]},
                {"D", [("C", 2), ("E", 3)]},
                {"E", [("D", 3)]},
                {"F", [("B", 6)]},
            };
            DataState dataState = new DataState(dict);
            
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

            directeur.Embaucher(dataState); 
            chefEquipe.Embaucher(dataState, directeur);
            chauffeur1.Embaucher(dataState, chefEquipe);
            chauffeur2.Embaucher(dataState, chefEquipe);


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

            createGraphFromCSV("distances.csv")!.drawGraph();

            

        }

        public static Graph<string>? createGraphFromCSV(string path) {
            int counter = 0; 
            string[,] lines;
            try
            {
                string[] readLines = File.ReadAllLines(path);
                lines = new string[readLines.Length-1, readLines[0].Split(",").Length];
                while (counter < readLines.Length - 1)
                {
                    string[] broke = readLines[counter + 1].Split(","); //skipping header
                    for (int i = 0; i < broke.Length; i++)
                    {
                        lines[counter, i] = broke[i];
                    }
                    counter++;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            Dictionary<string, List<(string data, int weight)>> dict = new Dictionary<string, List<(string data, int weight)>>();
            for (int i = 0; i < lines.GetLength(0); i++) {
                try {
                    if (!dict.ContainsKey(lines[i, 0])) dict[lines[i, 0]] = new List<(string data, int weight)>();
                    dict[lines[i, 0]].Add((lines[i, 1], Convert.ToInt32(lines[i, 2])));
                } catch (Exception)
                {
                    Console.WriteLine("File format is invalid!");
                    return null;
                }
            }

            return new Graph<string>(dict);
        }
    }
}
