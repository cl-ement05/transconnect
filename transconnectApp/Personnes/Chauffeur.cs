namespace transconnect
{
    public class Chauffeur : Salarie
    {
        private double tarifHoraire;

        public Chauffeur(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                         string adressePostale, string email, string telephone,
                         DateTime dateEntree, decimal salaire)
            : base(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone, dateEntree, chauffeur, salaire)
        {
            tarifHoraire = CalculerTarifHoraire();
        }

        public double TarifHoraire
        {
            get { return tarifHoraire; }
            private set { tarifHoraire = value; }
        }

        private double CalculerTarifHoraire()
        {
            int anciennete = DateTime.Now.Year - DateEntree.Year;
            if (DateTime.Now < DateEntree.AddYears(anciennete)) anciennete--;
            return anciennete * 1;
        }

        public int kmParcours(DataState dataState) {
            int distance = 0;
            foreach(Commande commande in dataState.commandes.FindAll(c => c.Chauffeur.Equals(this))) {
                Noeud<string>? nd = dataState.graphe.verticies.FirstOrDefault(n => n.data == commande.VilleDepart);
                Noeud<string>? na = dataState.graphe.verticies.FirstOrDefault(n => n.data == commande.VilleArrivee);
                if (nd is not null && na is not null)
                {
                    List<Noeud<string>> chemin;
                    int dst;
                    (chemin, dst) = dataState.graphe.Dijkstra(nd, na);
                    distance += dst;
                }
            }
            
            return distance;
        }

        public bool EstDisponible(DataState dataState, DateTime date)
        {
            foreach (Commande c in dataState.commandes.FindAll(c => c.Chauffeur.Equals(this)))
            {
                if (c.DateCommande.Date == date.Date)
                    return false;
            }
            return true;
        }

        public static new Chauffeur CreerNouveau() {
            Console.WriteLine("Veuillez saisir les informations du nouveau salarié.");
            PersonneDataHolder data = Personne.CreerNouveau();
            
            decimal salaire = Convert.ToDecimal(Program.ParseInt("Salaire : "));

            return new Chauffeur(
                data.numeroSS,
                data.nom,
                data.prenom,
                data.dateNaissance,
                data.adressePostale,
                data.email,
                data.telephone,
                DateTime.Now,
                salaire
            );
        }
    }
}
