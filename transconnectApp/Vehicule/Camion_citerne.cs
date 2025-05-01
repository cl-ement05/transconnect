namespace transconnect {
    public class CamionCiterne : PoidsLourds {
        private double volumeCuve;
        private string typeProduit;

        /// <summary>
        /// Constructeur naturel
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="couleur"></param>
        /// <param name="marque"></param>
        /// <param name="volumeCuve"></param>
        /// <param name="typeProduit"></param>
        /// <param name="statut"></param>
        public CamionCiterne(string immatriculation, string couleur, string marque, double capacite, double volumeCuve, string typeProduit, string statut = vehiculeDispo)
            : base(immatriculation, couleur, marque, capacite, statut)
        {
            this.volumeCuve = volumeCuve;
            this.typeProduit = typeProduit;
        }

        public double VolumeCuve {get { return volumeCuve; }set { volumeCuve = value; }}

        public string TypeProduit {get { return typeProduit; }set { typeProduit = value; }}

        public static new CamionCiterne CreerNouveau() {
            Console.WriteLine("Veuillez saisir les informations du nouveau véhicule.");
            VehiculeDataHolder data = Vehicule.CreerNouveau();
            
            double capacite = Program.ParseInt("Capacité chargement : ");
            int cuve = Program.ParseInt("Volume cuve : ");
            Console.Write("Type de produit : ");
            string produit = Console.ReadLine()!;
            return new CamionCiterne(
                data.immatriculation,
                data.couleur,
                data.marque,
                capacite,
                cuve,
                produit,
                data.statut
            );
        }

        public override string ToString() {
            return base.ToString() + " | Camion-Citerne: Volume de cuve: " + volumeCuve + " L" +" | Type de produit: " + typeProduit;
        }
    }
}
