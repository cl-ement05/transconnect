namespace transconnect
{
    public class Directeur : Salarie
    {     
        public Directeur(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                         string adressePostale, string email, string telephone,
                         DateTime dateEntree, decimal salaire)
            : base(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone, dateEntree, Salarie.directeur, salaire)
        {
        }

        public static new Directeur CreerNouveau() {
            Console.WriteLine("Veuillez saisir les informations du nouveau salarié.");
            PersonneDataHolder data = Personne.CreerNouveau();
            
            decimal salaire = Convert.ToDecimal(Program.ParseInt("Salaire : "));

            return new Directeur(
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
