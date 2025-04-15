namespace transconnect
{
    public class Chauffeur : Salarie
    {
    private List<Commande> livraisonsEffectuees;

    public Chauffeur(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                     string adressePostale, string email, string telephone,
                     DateTime dateEntree, decimal salaire)
        : base(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone, dateEntree, "Chauffeur", salaire)
    {
        livraisonsEffectuees = new List<Commande>();
    }

    public List<Commande> LivraisonsEffectuees
    {
        get { return livraisonsEffectuees; }
        set { livraisonsEffectuees = value; }
    }

    public bool EstDisponible(DateTime date)
    {
        foreach (Commande c in livraisonsEffectuees)
        {
            if (c.DateCommande.Date == date.Date)
                return false;
        }
        return true;
    }
}

}