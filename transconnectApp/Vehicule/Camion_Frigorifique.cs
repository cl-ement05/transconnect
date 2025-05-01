namespace transconnect {
    public class CamionFrigorifique : PoidsLourds {
        private double capaciteIsotherme;
        private int nombreGroupesElectrogene;

        /// <summary>
        /// Constructeur naturel
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="couleur"></param>
        /// <param name="marque"></param>
        /// <param name="capaciteIsotherme"></param>
        /// <param name="nombreGroupesElectrogene"></param>
        /// <param name="statut"></param>
        public CamionFrigorifique(string immatriculation, string couleur, string marque, double capacite, double capaciteIsotherme, int nombreGroupesElectrogene, string statut = vehiculeDispo)
            : base(immatriculation, couleur, marque, capacite, statut)
        {
            this.capaciteIsotherme = capaciteIsotherme;
            this.nombreGroupesElectrogene = nombreGroupesElectrogene;
        }

        public double CapaciteIsotherme { get { return capaciteIsotherme; }set { capaciteIsotherme = value; }}

        public int NombreGroupesElectrogene {get { return nombreGroupesElectrogene; }set { nombreGroupesElectrogene = value; }}

        public static new CamionFrigorifique CreerNouveau() {
            Console.WriteLine("Veuillez saisir les informations du nouveau véhicule.");
            VehiculeDataHolder data = Vehicule.CreerNouveau();
            
            double capacite = Program.ParseInt("Capacité chargement : ");
            double capaciteIso = Program.ParseInt("Capacité isotherme : ");
            int groupes = Program.ParseInt("Nombre groupes électrogènes : ");

            return new CamionFrigorifique(
                data.immatriculation,
                data.couleur,
                data.marque,
                capacite,
                capaciteIso,
                groupes,
                data.statut
            );
        }

        public override string ToString() {
            return base.ToString() + " | Camion Frigorifique: Capacité isotherme: " + capaciteIsotherme + " m³" + " | Groupes électrogènes: " + nombreGroupesElectrogene;
        }
    }
}
