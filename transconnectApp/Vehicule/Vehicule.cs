using System;

namespace transconnect {
    public struct VehiculeDataHolder {
        public VehiculeDataHolder(string immatriculation, string couleur, string marque, string statut = Vehicule.vehiculeDispo)
        {
            this.immatriculation = immatriculation;
            this.couleur = couleur;
            this.marque = marque;
            this.statut = statut;
        }
        public string immatriculation;
        public string couleur;
        public string marque;
        public string statut;
    }
    
    public abstract class Vehicule
    {
        public const string vehiculeDispo = "disponible";
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
        public void SupprimerVehicule(DataState dataState)
        {
            if (dataState.commandes.FindAll(c => c.Vehicule.Equals(this)).Count > 0) {
                Console.WriteLine("Le véhicule est rattaché à une commande, suppression impossible");
            } else {
                if (!dataState.flotte.Remove(this)) {
                    Console.WriteLine("Véhicule absent de la flotte");
                } else {
                    Console.WriteLine("Véhicule supprimé");
                }
            }
        }

        public void ChangerStatut(DataState dataState) {
            Console.WriteLine("Statut actuel : " + statut);
            Console.Write("Veuillez saisir le nouveau statut (disponible/maintenance) : ");
            List<Commande> cmd = dataState.commandes.FindAll(c => c.Vehicule.Equals(this));
            string st = Console.ReadLine()!;
            bool valid = false;
            while (!valid) {
                try {
                    if (st == vehiculeMaintenance && cmd.Count > 0) {
                        Console.WriteLine("Erreur : le véhicule est rattaché à une commande, impossible de changer son statut");
                        return;
                    }
                    Statut = st;
                    valid = true;
                } catch (ArgumentException) {
                    Console.WriteLine("Saisie invalide, veuillez réessayer : ");
                    st = Console.ReadLine()!;
                }
            }
            Console.WriteLine("Statut modifié avec succès");
        }

        protected static VehiculeDataHolder CreerNouveau() {
            Console.Write("Numéro d'immat : ");
            string immat = Console.ReadLine()!;
            Console.Write("Couleur : ");
            string couleur = Console.ReadLine()!;
            Console.Write("Marque : ");
            string marque = Console.ReadLine()!;

            return new VehiculeDataHolder(immat, couleur, marque);
        }

        /// <summary>
        /// Sélectionner un véhicule disponible
        /// </summary>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public static Vehicule? SelectionnerVehicule(DataState dataState, DateTime date)
        {
            List<Vehicule> dispos = dataState.flotte.FindAll(v => v.EstDisponible(dataState, date));
            if (dispos.Count > 0) {
                Console.WriteLine("Liste des véhicules disponibles : ");
                for(int i = 0; i < dispos.Count; i++)
                {
                    Console.WriteLine(i + " : " + dispos[i]);
                }
                Console.WriteLine("Saisir le véhicule voulu");
                int index = Convert.ToInt32(Console.ReadLine());
                while (index < 0 || index >= dispos.Count) {
                    Console.WriteLine("Saisie invalide, veuillez réessayer : ");
                    index = Convert.ToInt32(Console.ReadLine());
                }
                return dispos[index];
            } else return null;
        }

        public static void AfficherVehicules(DataState dataState) {
            if (dataState.flotte.Count == 0) {
                Console.WriteLine("Aucun véhicule");
            } else {
                foreach(Vehicule c in dataState.flotte) {
                    Console.WriteLine(c);
                }
            }
        }

        public bool EstDisponible(DataState dataState, DateTime date) {
            if (statut != vehiculeDispo) return false;
            foreach (Commande c in dataState.commandes.FindAll(c => c.Vehicule.Equals(this)))
            {
                if (c.DateCommande.Date == date.Date)
                    return false;
            }
            return true;
        }

        public static Vehicule? RechercherVehicule(DataState dataState, string immat)
        {
            return dataState.flotte.Find(v => v.Immatriculation == immat);
        }

        public override bool Equals(object? obj)
        {
            try { 
                Vehicule p = (Vehicule)obj!;
                return p.immatriculation == immatriculation;
            } catch (Exception) {
                return false;
            }
        }

        public override string ToString()
        {
            return "Immatriculation : " + immatriculation + " | Couleur : " + couleur + " | Marque : " + marque + " | Statut : " + statut;
        }

    }
}
