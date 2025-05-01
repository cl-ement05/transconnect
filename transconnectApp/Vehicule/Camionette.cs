using System;

namespace transconnect {
    public class Camionette : Vehicule
    {
        private string usage;

        /// <summary>
        /// Constructeur naturel
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="couleur"></param>
        /// <param name="marque"></param>
        /// <param name="usage"></param>
        /// <param name="statut"></param>
        public Camionette(string immatriculation, string couleur, string marque, string usage, string statut = vehiculeDispo)
            : base(immatriculation, couleur, marque, statut)
        {
            this.usage = usage;
        }

        public string Usage { get { return usage; } set { usage = value; } }

        public static new Camionette CreerNouveau() {
            Console.WriteLine("Veuillez saisir les informations du nouveau véhicule.");
            VehiculeDataHolder data = Vehicule.CreerNouveau();
            
            Console.Write("Usage : ");
            string usage = Console.ReadLine()!;

            return new Camionette(
                data.immatriculation,
                data.couleur,
                data.marque,
                usage,
                data.statut
            );
        }

        public override string ToString()
        {
            return base.ToString()+ " | Usage : " + usage;
        }
    }
}
