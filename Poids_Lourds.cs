using System;

namespace transconnect {
    public class Poids_Lourds : Vehicule
    {
        private double volume;
        private string matiere_transportee;

        public Poids_Lourds(string immatriculation, string couleur,string marque,double volume, string matiere_transportee):base(immatriculation, couleur, marque)
        {
            this.volume = volume;
            this.matiere_transportee=matiere_transportee;
        }

        public double Volume{ get { return volume; } set { volume=value; }}
        public string Matiere_transportee{ get {return matiere_transportee;} set { matiere_transportee=value; }}

        public override string ToString()
        {
            return base.ToString() + " | Volume : " + volume + " | Matière transportée : " + matiere_transportee;
        }
    }
}
