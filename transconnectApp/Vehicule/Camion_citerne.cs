namespace transconnect {
    public class CamionCiterne : Vehicule {
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
        public CamionCiterne(string immatriculation, string couleur, string marque, double volumeCuve, string typeProduit, string statut = vehiculeDispo)
            : base(immatriculation, couleur, marque, statut)
        {
            this.volumeCuve = volumeCuve;
            this.typeProduit = typeProduit;
        }

        public double VolumeCuve {get { return volumeCuve; }set { volumeCuve = value; }}

        public string TypeProduit {get { return typeProduit; }set { typeProduit = value; }}

        public override string ToString() {
            return base.ToString() + " | Camion-Citerne: Volume de cuve: " + volumeCuve + " L" +" | Type de produit: " + typeProduit;
        }
    }
}
