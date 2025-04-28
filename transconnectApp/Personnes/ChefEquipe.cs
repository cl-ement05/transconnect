namespace transconnect
{
    public class ChefEquipe : Salarie
    {
        private List<Chauffeur> chauffeursSousResponsabilite;

        public ChefEquipe(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                      string adressePostale, string email, string telephone,
                      DateTime dateEntree, decimal salaire)
        : base(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone, dateEntree, Salarie.chefEquipe, salaire)
        {
            chauffeursSousResponsabilite = new List<Chauffeur>();
        }

        public List<Chauffeur> ChauffeursSousResponsabilite
        {
            get { return chauffeursSousResponsabilite; }
            set { chauffeursSousResponsabilite = value; }
        }

        public void AssignerChauffeur(Chauffeur chauffeur)
        {
            chauffeursSousResponsabilite.Add(chauffeur);
        }

        public void SupprimerChauffeur(Chauffeur chauffeur)
        {
            chauffeursSousResponsabilite.Remove(chauffeur);
        }

        public static ChefEquipe CreerNouveau(DataState dataState) {
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
            return new ChefEquipe(
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
