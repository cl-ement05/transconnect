namespace transconnect
{
    public class StatutReclamation
    {
        public static readonly string EnAttente = "EnAttente";
        public static readonly string EnCours = "EnCours";
        public static readonly string Résolue = "Résolue";
        public static readonly string Rejetée = "Rejetée";

        
        public static List<string> TousLesStatuts()
        {
            return new List<string> { EnAttente, EnCours, Résolue, Rejetée };
        }
    }


    public class Reclamation
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string Sujet { get; set; }
        public string Description { get; set; }
        public DateTime DateReclamation { get; set; }
        public string Statut { get; set; }

        public DateTime? DateTraitement { get; set; }
        public string? Reponse { get; set; }

        public Reclamation(int id, Client client, string sujet, string description)
        {
            Id = id;
            Client = client;
            Sujet = sujet;
            Description = description;
            DateReclamation = DateTime.Now;
            Statut = StatutReclamation.EnAttente;
        }

        public void PasserEnCours()
        {
            Statut = StatutReclamation.EnCours;
        }

        public void MarquerCommeRésolue(string reponse)
        {
            Statut = StatutReclamation.Résolue;
            Reponse = reponse;
            DateTraitement = DateTime.Now;
        }

        public void MarquerCommeRejetée(string justification)
        {
            Statut = StatutReclamation.Rejetée;
            Reponse = justification;
            DateTraitement = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Réclamation #{Id} - {Sujet} - {Statut} - Client : {Client.Nom} {Client.Prenom}";
        }
    }

}