namespace transconnect {
    public class CamionBenne : Vehicule {
        private int nombreBennes;
        private bool grueAuxiliaire;

        public CamionBenne(string immatriculation, string couleur, string marque, int nombreBennes, bool grueAuxiliaire, string statut = vehiculeDispo)
            : base(immatriculation, couleur, marque, statut)
        {
            this.nombreBennes = nombreBennes;
            this.grueAuxiliaire = grueAuxiliaire;
        }

        public int NombreBennes {get { return nombreBennes; } set { nombreBennes = value; }}

        public bool GrueAuxiliaire {get { return grueAuxiliaire; }set { grueAuxiliaire = value; }}

        public override string ToString() {
            return base.ToString() + " | Camion Benne: Nombre de bennes: " + nombreBennes + " | Grue auxiliaire: " + (grueAuxiliaire ? "Oui" : "Non");
        }
    }
}
