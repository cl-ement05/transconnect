namespace transconnect{
    public abstract class Personne
    {
        private string numeroSS; 
        private string nom  ;
        private string prenom  ;
        private DateTime dateNaissance  ;
        private string adressePostale  ;
        private string email  ;
        private string telephone  ;

        public Personne(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                    string adressePostale, string email, string telephone)
        {
            this.numeroSS = numeroSS;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.adressePostale = adressePostale;
            this.email = email;
            this.telephone = telephone;
        }

        public string NumeroSS
        {
            get { return numeroSS; }
            set { numeroSS = value; }
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }
        public DateTime DateNaissance
        {
            get { return dateNaissance; }
            set { dateNaissance = value; }
        }
        public string AdressePostale
        {
            get { return adressePostale; }
            set { adressePostale = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

    

        public override string ToString()
        {
            return $"Nom : {nom}, Prénom : {prenom}, Date de naissance : {dateNaissance.ToShortDateString()}, Adresse : {adressePostale}, Email : {email}, Téléphone : {telephone}";
        }
    }

}