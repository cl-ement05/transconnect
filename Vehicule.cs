using System;

namespace transconnect {
    public abstract class Vehicule
    {
        protected string immatriculation;
        protected string couleur;
        protected string marque;

        public Vehicule(string immatriculation, string couleur,string marque)
        {
            this.immatriculation=immatriculation;
            this.couleur=couleur;
            this.marque=marque;
        }

        public string Immatriculation {get { return immatriculation; }}
        public string Couleur {get { return couleur;}}
        public string Marque {get { return marque;}}

        public override string ToString()
        {
            return "Immatriculation : " + immatriculation + " | Couleur : " + couleur + " | Marque : " + marque;
        }

    }
}
