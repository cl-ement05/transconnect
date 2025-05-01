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

        public static new ChefEquipe CreerNouveau() {
            Console.WriteLine("Veuillez saisir les informations du nouveau salarié.");
            PersonneDataHolder data = Personne.CreerNouveau();
            
            decimal salaire = Convert.ToDecimal(Program.ParseInt("Salaire : "));

            return new ChefEquipe(
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
