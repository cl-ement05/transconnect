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
            } catch (Exception e) {
                Console.WriteLine("Une erreur est survenue : " + e.Message);
                return false;
            }
        }

        public static DataState? Load(string path) {
            try {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<DataState>(json, new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All});
            } catch (IOException e) {
                Console.WriteLine("Une erreur est survenue : " + e.Message);
                return null;
            } catch (Exception e) {
                Console.WriteLine("Une erreur est survenue : " + e.Message);
                return null;
            }
        }
    }
}
