using System;

namespace transconnect {
    public abstract class Vehicule
    {
        public const string vehiculeDispo = "disponible";
        public const string vehiculeOccupe = "occupe";
        public const string vehiculeMaintenance = "maintenance";
        
        protected string immatriculation;
        protected string couleur;
        protected string marque;
        protected string statut;

        public Vehicule(string immatriculation, string couleur, string marque, string statut = vehiculeDispo)
        {
            this.immatriculation = immatriculation;
            this.couleur = couleur;
            this.marque = marque;
            this.statut = statut;
        }

        public string Immatriculation {get { return immatriculation; }}
        public string Couleur {get { return couleur;}}
        public string Marque {get { return marque;}}
        public string Statut {
            get { return statut;}
            set {
                switch (value) {
                    case vehiculeDispo :
                        statut = vehiculeDispo;
                        break;
                    case vehiculeMaintenance : 
                        statut = vehiculeMaintenance;
                        break;
                    case vehiculeOccupe :
                        statut = vehiculeOccupe;
                        break;
                    default :
                        throw new ArgumentException("Satut doit être une des trois constantes de la classe Vehicule");
                }
            }
        }

        public static Vehicule SelectionnerVehicule(DataState dataState)
        {
            Console.WriteLine("Liste des véhicules disponibles : ");
            for(int i=0; i < dataState.flotte.Count; i++)
            {
                Console.WriteLine(i+" : " + dataState.flotte[i]);
            }
            Console.WriteLine("Saisir le véhicule voulu");
            int index = Convert.ToInt32(Console.ReadLine());
            while (index < 0 || index >= dataState.flotte.Count()) {
                Console.WriteLine("Saisie invalide, veuillez réessayer : ");
                index = Convert.ToInt32(Console.ReadLine());
            }
            return dataState.flotte[index];
        }

        public void AjouterVehicule(DataState dataState)
        {
            if (!dataState.flotte.Contains(this)) {
                dataState.flotte.Add(this);
            }
        }

        public void RetirerVehicule(DataState dataState)
        {
            if (!dataState.flotte.Remove(this)) {
                Console.WriteLine("Véhicule absent de la flotte");
            }
        }

        public override string ToString()
        {
            return "Immatriculation : " + immatriculation + " | Couleur : " + couleur + " | Marque : " + marque + " | Statut : " + statut;
        }

    }
}
