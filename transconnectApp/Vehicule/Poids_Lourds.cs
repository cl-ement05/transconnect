namespace transconnect
{
    public abstract class PoidsLourds : Vehicule
    {
        private double capaciteChargement;

        /// <summary>
        /// Constructeur naturel
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="couleur"></param>
        /// <param name="marque"></param>
        /// <param name="capaciteChargement"></param>
        /// <param name="statut"></param>
        public PoidsLourds(string immatriculation, string couleur, string marque, double capaciteChargement, string statut = vehiculeDispo)
            : base(immatriculation, couleur, marque, statut)
        {
            this.capaciteChargement = capaciteChargement;
        }

        public double CapaciteChargement {get { return capaciteChargement; }set { capaciteChargement = value; }}

        public override string ToString()
        {
            return base.ToString() + " | Capacité de chargement : " + capaciteChargement;
        }
    }
}
