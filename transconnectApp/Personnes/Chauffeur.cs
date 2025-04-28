namespace transconnect
{
    public class Chauffeur : Salarie
    {
        private List<Commande> livraisonsEffectuees;
        private double tarifHoraire;

        public Chauffeur(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                         string adressePostale, string email, string telephone,
                         DateTime dateEntree, decimal salaire)
            : base(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone, dateEntree, chauffeur, salaire)
        {
            livraisonsEffectuees = new List<Commande>();
            tarifHoraire = CalculerTarifHoraire();
        }

        public List<Commande> LivraisonsEffectuees
        {
            get { return livraisonsEffectuees; }
            set { livraisonsEffectuees = value; }
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
            foreach(Commande commande in livraisonsEffectuees) {
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

        public bool EstDisponible(DateTime date)
        {
            foreach (Commande c in livraisonsEffectuees)
            {
                if (c.DateCommande.Date == date.Date)
                    return false;
            }
            return true;
        }

        public static Chauffeur CreerNouveau(DataState dataState) {
            Console.WriteLine("Veuillez saisir les informations du nouveau salarié.");
            PersonneDataHolder data = CreerPersonne(dataState);
            
            decimal salaire = 0;
            bool validSalaire = false;
            while (!validSalaire)
            {
                Console.Write("Salaire : ");
                string salaireInput = Console.ReadLine()!;
                try
                {
                    salaire = Convert.ToDecimal(salaireInput);
                    validSalaire = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Format invalide ! Veuillez réessayer.");
                }
            }

            DateTime dateEntree;
            Console.Write("Date d'entrée (JJ/MM/AAAA) : ");
            while (!DateTime.TryParse(Console.ReadLine(), out dateEntree)) {
                Console.WriteLine("Format invalide ! Veuillez réessayer.");
            }
            return new Chauffeur(
                data.numeroSS,
                data.nom,
                data.prenom,
                data.dateNaissance,
                data.adressePostale,
                data.email,
                data.telephone,
                dateEntree,
                salaire
            );
        }
    }
}
