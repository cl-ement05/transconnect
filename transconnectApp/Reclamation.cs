namespace transconnect
{    public class Reclamation
    {
        public const string EnAttente = "EnAttente";
        public const string EnCours = "EnCours";
        public const string Resolue = "Résolue";
        public const string Rejetee = "Rejetée";
        
        public int Id { get; set; }
        public Client Client { get; set; }
        public string Sujet { get; set; }
        public string Description { get; set; }
        public DateTime DateReclamation { get; set; }
        public string Statut {
            get { return Statut;}
            set {
                switch (value) {
                    case EnCours :
                        Statut = EnCours;
                        break;
                    case EnAttente : 
                        Statut = EnAttente;
                        break;
                    case Resolue :
                        Statut = Resolue;
                        break;
                    case Rejetee :
                        Statut = Rejetee;
                        break;
                    default :
                        throw new ArgumentException("Satut doit être une des quatre constantes de la classe Reclamation");
                }
            }
        }

        public DateTime? DateTraitement { get; set; }
        public string? Reponse { get; set; }

        public Reclamation(int id, Client client, string sujet, string description)
        {
            Id = id;
            Client = client;
            Sujet = sujet;
            Description = description;
            DateReclamation = DateTime.Now;
            Statut = EnAttente;
        }

        public void PasserEnCours()
        {
            Statut = EnCours;
        }

        public void MarquerCommeResolue(string reponse)
        {
            Statut = Resolue;
            Reponse = reponse;
            DateTraitement = DateTime.Now;
        }

        public void MarquerCommeRejetee(string justification)
        {
            Statut = Rejetee;
            Reponse = justification;
            DateTraitement = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Réclamation #{Id} - {Sujet} - {Statut} - Client : {Client.Nom} {Client.Prenom}";
        }
    }

}