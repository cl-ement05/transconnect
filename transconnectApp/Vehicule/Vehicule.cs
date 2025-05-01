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

        /// <summary>
        /// Constructeur naturel
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="couleur"></param>
        /// <param name="marque"></param>
        /// <param name="statut"></param>
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

        /// <summary>
        /// Ajout d'un véhicule
        /// </summary>
        /// <param name="dataState"></param>
        public void AjouterVehicule(DataState dataState)
        {
            if (!dataState.flotte.Contains(this)) {
                dataState.flotte.Add(this);
                Console.WriteLine("Véhicule rajouté avec succès");
            } else {
                Console.WriteLine("Véhicule déjà présent dans la flotte");
            }
        }

        /// <summary>
        /// Retirer un véhicule
        /// </summary>
        /// <param name="dataState"></param>
        public void RetirerVehicule(DataState dataState)
        {
            if (!dataState.flotte.Remove(this)) {
                Console.WriteLine("Véhicule absent de la flotte");
            }
        }

        /// <summary>
        /// Sélectionner un véhicule disponible
        /// </summary>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public static Vehicule SelectionnerVehicule(DataState dataState)
        {
            Console.WriteLine("Liste des véhicules disponibles : ");
            int counter = 1;
            List<int> validIndexes = new List<int>();
            for(int i = 0; i < dataState.flotte.Count; i++)
            {
                if (dataState.flotte[i].Statut == vehiculeDispo) {
                    Console.WriteLine(counter+" : " + dataState.flotte[i]);
                    validIndexes.Add(counter);
                    counter++;
                }
            }
            Console.WriteLine("Saisir le véhicule voulu");
            int index = Convert.ToInt32(Console.ReadLine());
            while (!validIndexes.Contains(index)) {
                Console.WriteLine("Saisie invalide, veuillez réessayer : ");
                index = Convert.ToInt32(Console.ReadLine());
            }
            return dataState.flotte[index];
        }

        public override string ToString()
        {
            return "Immatriculation : " + immatriculation + " | Couleur : " + couleur + " | Marque : " + marque + " | Statut : " + statut;
        }

    }
}
