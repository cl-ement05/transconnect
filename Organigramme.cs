namespace transconnect{
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
            if (racine is null)
            {
                racine = new Noeud(salarie);
            }
            else if (manager is not null)
            {
                Noeud? noeudManager = TrouverNoeud(racine, manager);
                if (noeudManager is not null)
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
            if (racine is not null)
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
            if (racine is not null)
            {
                AfficherNoeud(racine, "", true);
            }
            else
            {
                Console.WriteLine("L'organigramme est vide.");
            }
        }


        /// <summary>
        /// Recherche un salarié dans l'organigramme à partir d'un certain noeud
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
                if (resultat is not null)
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
            if (noeud.Salarie.Poste == Salarie.directeur)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (noeud.Salarie.Poste == Salarie.chefEquipe)
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
