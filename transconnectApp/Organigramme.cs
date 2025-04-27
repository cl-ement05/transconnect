namespace transconnect
{
    public class Organigramme
    {
        public class Noeud
        {
            private Salarie salarie;
            private Noeud succ;    
            private Noeud frere;   

            public Noeud(Salarie salarie = null, Noeud succ = null, Noeud frere = null)
            {
                this.salarie = salarie;
                this.succ = succ;
                this.frere = frere;
            }

            public Salarie Salarie
            {
                get { return salarie; }
                set { salarie = value; }
            }

            public Noeud Succ
            {
                get { return succ; }
                set { succ = value; }
            }

            public Noeud Frere
            {
                get { return frere; }
                set { frere = value; }
            }

            public override string ToString()
            {
                return $"{salarie?.Nom} - {salarie?.Poste}";
            }
        }

        private Noeud racine;

        public Organigramme(Noeud racine = null)
        {
            this.racine = racine;
        }

        public Noeud Racine
        {
            get { return racine; }
            set { racine = value; }
        }

        public bool Ajouter_Frere(Noeud start, Salarie salarie)
        {
            if (start == null) return false;
            Noeud courant = start;
            while (courant.Frere != null)
            {
                courant = courant.Frere;
            }
            courant.Frere = new Noeud(salarie);
            return true;
        }

        public bool Ajouter_Succ(Noeud start, Salarie salarie)
        {
            if (start == null) return false;
            if (start.Succ == null)
            {
                start.Succ = new Noeud(salarie);
                return true;
            }
            else
            {
                return Ajouter_Frere(start.Succ, salarie);
            }
        }

        public void AjouterSalarie(Salarie salarie, Salarie? manager = null)
        {
            if (racine == null)
            {
                racine = new Noeud(salarie);
            }
            else if (manager != null)
            {
                Noeud? noeudManager = Rechercher(racine, manager);
                if (noeudManager != null)
                {
                    Ajouter_Succ(noeudManager, salarie);
                }
                else
                {
                    Console.WriteLine($"Manager {manager.Nom} introuvable dans l'organigramme.");
                }
            }
        }

        public void Afficher(Noeud start = null, string prefix = "")
        {
            if (start == null) start = racine;
            if (start != null)
            {
                Console.WriteLine(prefix + start.ToString());
                Afficher(start.Succ, prefix + "    ");
                Afficher(start.Frere, prefix);
            }
        }

        public Noeud? Rechercher(Noeud start, Salarie salarie)
        {
            if (start == null) return null;
            if (start.Salarie == salarie) return start;

            Noeud? trouve = Rechercher(start.Succ, salarie);
            if (trouve != null) return trouve;

            return Rechercher(start.Frere, salarie);
        }

        public void SupprimerSalarie(Salarie salarie)
        {
            if (racine == null) return;

            if (racine.Salarie == salarie)
            {
                racine = null;
            }
            else
            {
                SupprimerNoeud(null, racine, salarie);
            }
        }

        private void SupprimerNoeud(Noeud? parent, Noeud courant, Salarie salarie)
        {
            if (courant == null) return;

            if (courant.Succ != null)
            {
                SupprimerNoeud(courant, courant.Succ, salarie);
            }

            if (courant.Frere != null)
            {
                SupprimerNoeud(parent, courant.Frere, salarie);
            }

            if (courant.Salarie == salarie)
            {
                if (parent != null)
                {
                    if (parent.Succ == courant)
                    {
                        parent.Succ = courant.Frere;
                    }
                    else
                    {
                        parent.Frere = courant.Frere;
                    }
                }
            }
        }

        public void ModifierManager(Salarie salarie, Salarie nouveauManager)
        {
            SupprimerSalarie(salarie);
            AjouterSalarie(salarie, nouveauManager);
        }

        public List<Salarie> ObtenirSubordonnes(Salarie manager)
        {
            List<Salarie> subordonnes = new List<Salarie>();
            Noeud? managerNoeud = Rechercher(racine, manager);
            if (managerNoeud != null)
            {
                Noeud? courant = managerNoeud.Succ;
                while (courant != null)
                {
                    subordonnes.Add(courant.Salarie);
                    courant = courant.Frere;
                }
            }
            return subordonnes;
        }
    }
}
