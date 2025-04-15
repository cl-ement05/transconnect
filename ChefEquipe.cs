namespace transconnect
{
    public class ChefEquipe : Salarie
    {
        private List<Chauffeur> chauffeursSousResponsabilite;

        public ChefEquipe(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                      string adressePostale, string email, string telephone,
                      DateTime dateEntree, decimal salaire)
        : base(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone, dateEntree, "Chef d'Équipe", salaire)
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
    }

}