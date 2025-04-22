namespace transconnect {
    public class CamionBenne : Vehicule {
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
