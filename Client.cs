namespace transconnect
{
    public class Client : Personne
    {
        private List<Commande> historiqueCommandes;

        public Client(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                    string adressePostale, string email, string telephone)
                    : base(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone)
        {
            historiqueCommandes = new List<Commande>();
        }

        public List<Commande> HistoriqueCommandes
        {
            get { return historiqueCommandes; }
            set { historiqueCommandes = value; }
        }

        public decimal MontantTotalAchats()
        {
            decimal total = 0;
            foreach (Commande commande in historiqueCommandes)
            {
                total += commande.Prix;
            }
            return total;
        }

        public override string ToString()
        {
            return base.ToString() + "Montant total des achats : " + MontantTotalAchats() + " €";
        
        }
}

}