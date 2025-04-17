namespace transconnect
{
    public class Module_Statut_Vehicule
    {
        private Dictionary<Vehicule, string> statutVehicules;

        public Module_Statut_Vehicule()
        {
            statutVehicules = new Dictionary<Vehicule, string>();
        }

        /// <summary>
        /// Ajoute un véhicule au module avec un statut initial.
        /// </summary>
        /// <param name="vehicule">Le véhicule à ajouter.</param>
        /// <param name="statut">Le statut initial du véhicule (ex. "Disponible").</param>
        public void AjouterVehicule(Vehicule vehicule, string statut = "Disponible")
        {
            if (!statutVehicules.ContainsKey(vehicule))
            {
                statutVehicules.Add(vehicule, statut);
            }
            else
            {
                Console.WriteLine($"Le véhicule {vehicule.Immatriculation} est déjà enregistré.");
            }
        }

        /// <summary>
        /// Met à jour le statut d'un véhicule.
        /// </summary>
        /// <param name="vehicule">Le véhicule à mettre à jour.</param>
        /// <param name="nouveauStatut">Le nouveau statut du véhicule.</param>
        public void MettreAJourStatut(Vehicule vehicule, string nouveauStatut)
        {
            if (statutVehicules.ContainsKey(vehicule))
            {
                statutVehicules[vehicule] = nouveauStatut;
            }
            else
            {
                Console.WriteLine($"Le véhicule {vehicule.Immatriculation} n'est pas enregistré.");
            }
        }

        /// <summary>
        /// Récupère le statut actuel d'un véhicule.
        /// </summary>
        /// <param name="vehicule">Le véhicule dont on veut connaître le statut.</param>
        /// <returns>Le statut actuel du véhicule.</returns>
        public string ObtenirStatut(Vehicule vehicule)
        {
            if (statutVehicules.ContainsKey(vehicule))
            {
                return statutVehicules[vehicule];
            }
            else
            {
                return $"Le véhicule {vehicule.Immatriculation} n'est pas enregistré.";
            }
        }

        /// <summary>
        /// Affiche tous les véhicules et leurs statuts.
        /// </summary>
        public void AfficherStatuts()
        {
            Console.WriteLine("Statuts des véhicules :");
            foreach (var entry in statutVehicules)
            {
                Console.WriteLine($"{entry.Key.Immatriculation} - {entry.Value}");
            }
        }
    }
}