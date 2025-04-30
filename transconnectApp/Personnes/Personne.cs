namespace transconnect{
    public struct PersonneDataHolder {
            public PersonneDataHolder(string numeroSS, string nom, string prenom, DateTime dateNaissance,
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
            
            public string numeroSS; 
            public string nom  ;
            public string prenom  ;
            public DateTime dateNaissance  ;
            public string adressePostale  ;
            public string email  ;
            public string telephone  ;
        }
    
    public abstract class Personne
    {
        protected readonly string numeroSS; 
        protected string nom  ;
        protected string prenom  ;
        protected DateTime dateNaissance  ;
        protected string adressePostale  ;
        protected string email  ;
        protected string telephone  ;

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

        public virtual void ModifierInfos() {
            Console.WriteLine("Modification des informations de " + Nom + " " + Prenom 
            + ". Appuyez sur Entrée pour conserver la valeur actuelle.");

            Console.Write($"Nom ({this.nom}) : ");
            string nom = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(nom)) this.nom = nom;

            Console.Write($"Prénom ({this.prenom}) : ");
            string prenom = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(prenom)) this.prenom = prenom;

            Console.Write($"Date de naissance ({this.dateNaissance:dd/MM/yyyy}) (JJ/MM/AAAA) : ");
            string dateNaissanceInput = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(dateNaissanceInput))
            {
                if (DateTime.TryParse(dateNaissanceInput, out DateTime dateNaissance))
                {
                    this.dateNaissance = dateNaissance;
                }
                else
                {
                    Console.WriteLine("Date invalide. La valeur actuelle est conservée.");
                }
            }

            Console.Write($"Adresse postale ({this.adressePostale}) : ");
            string adressePostale = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(adressePostale)) this.adressePostale = adressePostale;

            Console.Write($"Email ({this.email}) : ");
            string email = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(email)) this.email = email;

            Console.Write($"Téléphone ({this.telephone}) : ");
            string telephone = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(telephone)) this.telephone = telephone;

            Console.WriteLine("Les informations ont été mises à jour.");
        }

        protected static PersonneDataHolder CreerPersonne(DataState dataState) {
            bool validSS = false;
            string numeroSS = "";
            while (!validSS) {  
                Console.Write("Numéro de SS : ");
                numeroSS = Console.ReadLine()!;
                if (dataState.clients.Any(c => c.NumeroSS == numeroSS) ||
                dataState.salaries.Any(c => c.NumeroSS == numeroSS)) {
                    Console.WriteLine("Erreur : ce numéro de SS est déjà dans la base de données");
                } else {
                    validSS = true;
                }
            }
            
            Console.Write("Nom : ");
            string nom = Console.ReadLine()!;
            Console.Write("Prénom : ");
            string prenom = Console.ReadLine()!;
            DateTime? dateNaissance = null;
            bool valid = false;
            while (!valid) {
                Console.Write("Date de naissance (JJ/MM/AAAA) : ");
                try {
                    dateNaissance = DateTime.Parse(Console.ReadLine()!);
                    valid = true;
                } catch (FormatException) {
                    Console.WriteLine("Format invalide ! Veuillez réessayer.");
                }
            }
            Console.Write("Adresse postale : ");
            string adressePostale = Console.ReadLine()!;
            Console.Write("Email : ");
            string email = Console.ReadLine()!;
            Console.Write("Téléphone : ");
            string telephone = Console.ReadLine()!;

            return new PersonneDataHolder(numeroSS, nom, prenom, (DateTime)dateNaissance!, adressePostale, email, telephone);
        }

        public string NumeroSS
        {
            get { return numeroSS; }
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
            return $"{numeroSS} Nom : {nom}, Prénom : {prenom}, Date de naissance : {dateNaissance.ToShortDateString()}, Adresse : {adressePostale}, Email : {email}, Téléphone : {telephone}";
        }
    }
}
