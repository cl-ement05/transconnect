using System;

namespace transconnect {
    public class Camionette : Vehicule
    {
        private string usage;

        public Camionette(string immatriculation, string couleur, string marque, string usage, string statut = vehiculeDispo)
            : base(immatriculation, couleur, marque, statut)
        {
            this.usage = usage;
        }

        public string Usage { get { return usage; } set { usage = value; } }

        public override string ToString()
        {
            return base.ToString()+ " | Usage : " + usage;
        }
    }
}
