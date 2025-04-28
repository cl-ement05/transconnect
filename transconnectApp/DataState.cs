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

        /// <summary>
        /// natural constructor
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="labels"></param>
        public DataState(Graph<string> graph) {
            commandes = new List<Commande>();
            clients = new List<Client>();
            salaries = new List<Salarie>();
            organigramme = new Organigramme();
            flotte = new List<Vehicule>();
            reclamations = new List<Reclamation>();
            this.graphe = graph;
        }
    }
}
