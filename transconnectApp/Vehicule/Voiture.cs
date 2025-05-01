using System;

namespace transconnect {
    public class Voiture : Vehicule
    {
        private int nb_passagers;


        /// <summary>
        /// Constructeur naturel
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="couleur"></param>
        /// <param name="marque"></param>
        /// <param name="nb_passagers"></param>
        /// <param name="statut"></param>
        public Voiture (string immatriculation, string couleur, string marque, int nb_passagers, string statut = vehiculeDispo) 
            : base(immatriculation, couleur, marque, statut)
        {
            this.nb_passagers = nb_passagers;
        }

        public int Nb_passager { get {return nb_passagers;} set {nb_passagers = value;} }

        public static new Voiture CreerNouveau() {
            Console.WriteLine("Veuillez saisir les informations du nouveau véhicule.");
            VehiculeDataHolder data = Vehicule.CreerNouveau();
            
            int nb_passagers = Program.ParseInt("Nombre passagers : ");

            return new Voiture(
                data.immatriculation,
                data.couleur,
                data.marque,
                nb_passagers,
                data.statut
            );
        }

        public override string ToString ()
        {
            return base.ToString () + " | Nombre de passagers : " + nb_passagers;
        }
    }
}
