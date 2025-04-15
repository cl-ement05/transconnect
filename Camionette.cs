using System;

namespace transconnect {
    public class Camionette : Vehicule
    {
        private string usage;

        public Camionette(string immatriculation, string couleur,string marque,string usage):base(immatriculation, couleur, marque)
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