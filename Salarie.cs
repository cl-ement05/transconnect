namespace transconnect
{
    public abstract class Salarie : Personne
    {
        private DateTime dateEntree;
        private string poste;
        private decimal salaire;

        public Salarie(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                       string adressePostale, string email, string telephone,
                       DateTime dateEntree, string poste, decimal salaire)
            : base(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone)
        {
            this.dateEntree = dateEntree;
            this.poste = poste;
            this.salaire = salaire;
        }

        public DateTime DateEntree
        {
            get { return dateEntree; }
            set { dateEntree = value; }
        }

        public string Poste
        {
            get { return poste; }
            set { poste = value; }
        }

        public decimal Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }

        public override string ToString()
        {
            return base.ToString() + $"Poste : {poste}, Salaire : {salaire} €, Date d'entrée : {dateEntree.ToShortDateString()}\n";
        }
    }
}