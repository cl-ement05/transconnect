namespace transconnect
{    public class Reclamation
    {
        public const string EnAttente = "EnAttente";
        public const string Resolue = "Résolue";
        public const string Rejetee = "Rejetée";
        
        public int Id { get; set; }
        public Client Client { get; set; }
        public string Sujet { get; set; }
        public string Description { get; set; }
        public DateTime DateReclamation { get; set; }
        private static int lastNumber = 1;
        public string Statut {
            get { return Statut;}
            set {
                switch (value) {
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

        public Reclamation(Client client, string sujet, string description)
        {
            Id = lastNumber;
            Client = client;
            Sujet = sujet;
            Description = description;
            DateReclamation = DateTime.Now;
            Statut = EnAttente;
            lastNumber++;
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

        public void AjouterReclamation(DataState dataState) {
            if (!dataState.reclamations.Contains(this)) {
                dataState.reclamations.Add(this);
                Console.WriteLine("Réclamation enregistrée avec succès");
            } else {
                Console.WriteLine("Réclamation déjà enregistrée");
            }
        }

        public static void AfficherReclamations(DataState dataState) {
            if (dataState.reclamations.Count == 0) {
                Console.WriteLine("Aucune réclamation");
            } else {
                foreach(Reclamation reclamation in dataState.reclamations) {
                    Console.WriteLine(reclamation);
                }
            }
        }

        public static Reclamation? RechercherReclamation(DataState dataState, int id) {
            return dataState.reclamations.Find(c => c.Id == id);
        }

        public static Reclamation? CreerReclamation(DataState dataState) {
            Console.Write("Saisissez le numéro de SS du client qui passe commande : ");
            string numeroSS = Console.ReadLine()!;
            
            Client? clientExistant = Client.RechercherClientNSS(dataState, numeroSS);
            if (clientExistant is null)
            {
                Console.WriteLine("Client non trouvé. Vous devez d'abord créer le client avant de pouvoir créer une réclamation");
                return null;
            }

            Console.Write("Saisissez le sujet de la réclamation : ");
            string sujet = Console.ReadLine()!;
            Console.Write("Saisissez la description : ");
            string desc = Console.ReadLine()!;

            return new Reclamation(clientExistant, sujet, desc);
        }

        public override string ToString()
        {
            return $"Réclamation #{Id} - {Sujet} - {Statut} - Client : {Client.Nom} {Client.Prenom}\n{Description}";
        }
    }
}
