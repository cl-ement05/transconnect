namespace transconnect
{
    public class Module_Salarie
    {
        private List<Salarie> salaries;
        private Organigramme organigramme;

        public Module_Salarie()
        {
            salaries = new List<Salarie>();
            organigramme = new Organigramme();
        }

        public List<Salarie> Salaries
        {
            get { return salaries; }
            set { salaries = value; }
        }
        /// <summary>
        /// Ajoute un salarié à l'entreprise et l'ajoute à l'organigramme.
        /// </summary>
        /// <param name="salarie"></param>
        /// <param name="manager"></param>
        public void EmbaucherSalarie(Salarie salarie, Salarie? manager = null)
        {
            salaries.Add(salarie);
            organigramme.AjouterSalarie(salarie, manager);
        }
        /// <summary>
        /// Supprime un salarié de l'entreprise et de l'organigramme.
        /// </summary>
        /// <param name="salarie"></param>
        public void LicencierSalarie(Salarie salarie)
        {
            salaries.Remove(salarie);
            organigramme.SupprimerSalarie(salarie);
        }
        /// <summary>
        /// Recherche un salarié par son numéro de sécurité sociale.
        /// </summary>
        /// <param name="numeroSS"></param>
        /// <returns></returns>
        public Salarie? RechercherSalarieParNumeroSS(string numeroSS)
        {
            return salaries.Find(s => s.NumeroSS == numeroSS);
        }
        /// <summary>
        /// Modifie les informations d'un salarié par son numéro de sécurité sociale.
        /// </summary>
        /// <param name="numeroSS"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="dateNaissance"></param>
        /// <param name="adressePostale"></param>
        /// <param name="email"></param>
        /// <param name="telephone"></param>
        /// <param name="salaire"></param>

        public void ModifierSalarieParNumeroSS(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                                               string adressePostale, string email, string telephone, decimal salaire)
        {
            Salarie? salarie = RechercherSalarieParNumeroSS(numeroSS);
            if (salarie != null)
            {
                salarie.Nom = nom;
                salarie.Prenom = prenom;
                salarie.DateNaissance = dateNaissance;
                salarie.AdressePostale = adressePostale;
                salarie.Email = email;
                salarie.Telephone = telephone;
                salarie.Salaire = salaire;
            }
            else
            {
                Console.WriteLine($"Salarié avec le numéro SS {numeroSS} introuvable.");
            }
        }


        /// <summary>
        /// Affiche la liste des salariés de l'entreprise.
        /// </summary>

        public void AfficherSalaries()
        {
            Console.WriteLine("Liste des salariés :");
            foreach (Salarie salarie in salaries)
            {
                Console.WriteLine(salarie.ToString());
            }
        }

        /// <summary>
        /// Affiche les salariés par nom.
        /// /// </summary>

        public void AfficherOrganigramme()
        {
            Console.WriteLine("Organigramme de l'entreprise :");
            organigramme.Afficher();
        }
    }
    /// <summary>
    /// Classe représentant l'organigramme de l'entreprise.
    /// Il est constitué de noeuds représentant les salariés et leurs subordonnés.
    /// Chaque salarié peut avoir un manager, et l'organigramme est structuré de manière hiérarchique.
    /// La racine de l'organigramme est le salarié sans manager.
    /// Les subordonnés sont ajoutés en tant que noeuds enfants du manager dans l'organigramme.
    /// </summary>

    public class Organigramme
    {
        private class Noeud
        {
            private Salarie salarie;
            private List<Noeud> subordonnes;
            public Salarie Salarie
            {
                get { return salarie; }
                set { salarie = value; }
            }
            public List<Noeud> Subordonnes
            {
                get { return subordonnes; }
                set { subordonnes = value; }
            }
            

            public Noeud(Salarie salarie)
            {
                this.salarie = salarie;
                this.subordonnes = new List<Noeud>();
            }
        }

        private Noeud? racine;

        /// <summary>
        /// Ajoute un salarié à l'organigramme.
        /// Si le salarié a un manager, il est ajouté en tant que subordonné du manager.
        /// Si le salarié n'a pas de manager, il est ajouté à la racine de l'organigramme.
        /// </summary>
        /// <param name="salarie"></param>
        /// <param name="manager"></param>

        public void AjouterSalarie(Salarie salarie, Salarie? manager = null)
        {
            if (racine == null)
            {
                racine = new Noeud(salarie);
            }
            else if (manager != null)
            {
                Noeud? noeudManager = TrouverNoeud(racine, manager);
                if (noeudManager != null)
                {
                    noeudManager.Subordonnes.Add(new Noeud(salarie));
                }
                else
                {
                    Console.WriteLine($"Manager {manager.Nom} introuvable dans l'organigramme.");
                }
            }
        }

        /// <summary>
        /// Supprime un salarié de l'organigramme.
        /// Si le salarié est la racine, l'organigramme devient vide.
        /// </summary>
        /// <param name="salarie"></param>

        public void SupprimerSalarie(Salarie salarie)
        {
            if (racine != null)
            {
                if (racine.Salarie == salarie)
                {
                    racine = null;
                }
                else
                {
                    SupprimerNoeud(racine, salarie);
                }
            }
        }


        /// <summary>
        /// Affiche l'organigramme de l'entreprise.
        /// </summary>

        public void Afficher()
        {
            if (racine != null)
            {
                AfficherNoeud(racine, "", true);
            }
            else
            {
                Console.WriteLine("L'organigramme est vide.");
            }
        }


        /// <summary>
        /// Recherche un salarié dans l'organigramme.
        /// Si le salarié est trouvé, retourne le noeud correspondant.
        /// </summary>
        /// <param name="noeud"></param>
        /// <param name="salarie"></param>
        /// <returns></returns>

        private Noeud? TrouverNoeud(Noeud noeud, Salarie salarie)
        {
            if (noeud.Salarie == salarie)
            {
                return noeud;
            }

            foreach (Noeud subordonne in noeud.Subordonnes)
            {
                Noeud? resultat = TrouverNoeud(subordonne, salarie);
                if (resultat != null)
                {
                    return resultat;
                }
            }

            return null;
        }


        /// <summary>
        /// Supprime un salarié de l'organigramme.
        /// </summary>
        /// <param name="noeud"></param>
        /// <param name="salarie"></param>

        private void SupprimerNoeud(Noeud noeud, Salarie salarie)
        {
            foreach (Noeud subordonne in noeud.Subordonnes.ToList())
            {
                if (subordonne.Salarie == salarie)
                {
                    noeud.Subordonnes.Remove(subordonne);
                    return;
                }
                else
                {
                    SupprimerNoeud(subordonne, salarie);
                }
            }
        }

        /// <summary>
        /// Affiche un salarié et ses subordonnés dans l'organigramme.
        /// </summary>
        /// <param name="noeud"></param>
        /// <param name="prefix"></param>
        /// <param name="isLast"></param>

        private void AfficherNoeud(Noeud noeud, string prefix, bool isLast)
        {
            // Changer la couleur en fonction du poste
            if (noeud.Salarie.Poste == "Directeur")
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (noeud.Salarie.Poste == "Chef d'Équipe")
                Console.ForegroundColor = ConsoleColor.Cyan;
            else
                Console.ForegroundColor = ConsoleColor.White;

            // Afficher le salarié avec un préfixe visuel
            Console.WriteLine(prefix + (isLast ? "└── " : "├── ") + noeud.Salarie.ToString());

            // Réinitialiser la couleur
            Console.ResetColor();

            // Préparer le préfixe pour les subordonnés
            prefix += isLast ? "    " : "│   ";

            // Parcourir les subordonnés
            for (int i = 0; i < noeud.Subordonnes.Count; i++)
            {
                AfficherNoeud(noeud.Subordonnes[i], prefix, i == noeud.Subordonnes.Count - 1);
            }
        }
    }
}