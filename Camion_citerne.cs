namespace transconnect {
    public abstract class CamionCiterne : Vehicule {
        private double volumeCuve;
        private string typeProduit;

        public CamionCiterne(string immatriculation, string couleur, string marque, double volumeCuve, string typeProduit)
            : base(immatriculation, couleur, marque)
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
