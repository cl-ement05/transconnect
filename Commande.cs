namespace transconnect
{
    public class Commande
    {
        private int numeroCommande;
        private Client client;
        private string villeDepart;
        private string villeArrivee;
        private decimal prix;
        private Chauffeur chauffeur;
        private DateTime dateCommande;

        public Commande(int numeroCommande, Client client, string villeDepart, string villeArrivee,
                        decimal prix, Chauffeur chauffeur, DateTime dateCommande)
        {
            this.numeroCommande = numeroCommande;
            this.client = client;
            this.villeDepart = villeDepart;
            this.villeArrivee = villeArrivee;
            this.prix = prix;
            this.chauffeur = chauffeur;
            this.dateCommande = dateCommande;
        }

        public int NumeroCommande
        {
            get { return numeroCommande; }
            set { numeroCommande = value; }
        }

        public Client Client
        {
            get { return client; }
            set { client = value; }
        }

        public string VilleDepart
        {
            get { return villeDepart; }
            set { villeDepart = value; }
        }

        public string VilleArrivee
        {
            get { return villeArrivee; }
            set { villeArrivee = value; }
        }

        public decimal Prix
        {
            get { return prix; }
            set { prix = value; }
        }

        public Chauffeur Chauffeur
        {
            get { return chauffeur; }
            set { chauffeur = value; }
        }

        public DateTime DateCommande
        {
            get { return dateCommande; }
            set { dateCommande = value; }
        }

        public override string ToString()
        {
            return $"[Commande #{NumeroCommande}] {VilleDepart} → {VilleArrivee} | {Prix} € | {DateCommande.ToShortDateString()}";
        }
    }
}
