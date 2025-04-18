namespace transconnect
{
    public class Commande
    {
        private int numeroCommande;
        private Client client;
        private string villeDepart;
        private string villeArrivee;
        private Vehicule vehicule;
        private Chauffeur chauffeur;
        private DateTime dateCommande;

        public Commande(int numeroCommande, Client client, string villeDepart, string villeArrivee,
                        Vehicule vehicule, Chauffeur chauffeur, DateTime dateCommande)
        {
            this.numeroCommande = numeroCommande;
            this.client = client;
            this.villeDepart = villeDepart;
            this.villeArrivee = villeArrivee;
            this.vehicule = vehicule;
            this.chauffeur = chauffeur;
            this.dateCommande = dateCommande;
        }

        public int NumeroCommande { get { return numeroCommande; } set { numeroCommande = value; } }
        public Client Client { get { return client; } set { client = value; } }
        public string VilleDepart { get { return villeDepart; } set { villeDepart = value; } }
        public string VilleArrivee { get { return villeArrivee; } set { villeArrivee = value; } }
        public Vehicule Vehicule { get { return vehicule; } set { vehicule = value; } }
        public Chauffeur Chauffeur { get { return chauffeur; } set { chauffeur = value; } }
        public DateTime DateCommande { get { return dateCommande; } set { dateCommande = value; } }

        public override string ToString()
        {
            return "Commande : " + numeroCommande + " | " + villeDepart + " -> " + villeArrivee + " | Date : " + dateCommande.ToShortDateString() + " | Véhicule : " + vehicule;
        }
    }
}
