namespace transconnect {
    public class CamionBenne : PoidsLourds {
        private int nombreBennes;
        private bool grueAuxiliaire;

        /// <summary>
        /// Constructeur naturel
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="couleur"></param>
        /// <param name="marque"></param>
        /// <param name="nombreBennes"></param>
        /// <param name="grueAuxiliaire"></param>
        /// <param name="statut"></param>
        public CamionBenne(string immatriculation, string couleur, string marque, double capacite, int nombreBennes, bool grueAuxiliaire, string statut = vehiculeDispo)
            : base(immatriculation, couleur, marque, capacite, statut)
        {
            this.nombreBennes = nombreBennes;
            this.grueAuxiliaire = grueAuxiliaire;
        }

        public int NombreBennes {get { return nombreBennes; } set { nombreBennes = value; }}

        public bool GrueAuxiliaire {get { return grueAuxiliaire; }set { grueAuxiliaire = value; }}

        public static new CamionBenne CreerNouveau() {
            Console.WriteLine("Veuillez saisir les informations du nouveau véhicule.");
            VehiculeDataHolder data = Vehicule.CreerNouveau();
            
            double capacite = Program.ParseInt("Capacité chargement : ");
            int bennes = Program.ParseInt("Nombre bennes : ");
            Console.Write("Grue auxiliaire (oui/NON) : ");
            string grues = Console.ReadLine()!;
            bool grue = false;
            if (grues == "oui") {
                grue = true;
            }

            return new CamionBenne(
                data.immatriculation,
                data.couleur,
                data.marque,
                capacite,
                bennes,
                grue,
                data.statut
            );
        }

        public override string ToString() {
            return base.ToString() + " | Camion Benne: Nombre de bennes: " + nombreBennes + " | Grue auxiliaire: " + (grueAuxiliaire ? "Oui" : "Non");
        }
    }
}
