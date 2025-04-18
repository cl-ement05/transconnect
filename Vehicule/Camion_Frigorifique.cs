namespace transconnect {
    public class CamionFrigorifique : Vehicule {
        private double capaciteIsotherme;
        private int nombreGroupesElectrogene;

        public CamionFrigorifique(string immatriculation, string couleur, string marque, double capaciteIsotherme, int nombreGroupesElectrogene)
            : base(immatriculation, couleur, marque)
        {
            this.capaciteIsotherme = capaciteIsotherme;
            this.nombreGroupesElectrogene = nombreGroupesElectrogene;
        }

        public double CapaciteIsotherme { get { return capaciteIsotherme; }set { capaciteIsotherme = value; }}

        public int NombreGroupesElectrogene {get { return nombreGroupesElectrogene; }set { nombreGroupesElectrogene = value; }}

        public override string ToString() {
            return base.ToString() + " | Camion Frigorifique: Capacité isotherme: " + capaciteIsotherme + " m³" + " | Groupes électrogènes: " + nombreGroupesElectrogene;
        }
    }
}
