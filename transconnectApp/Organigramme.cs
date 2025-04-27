namespace transconnect
{
    public class Organigramme
    {
        private class Noeud
        {
            public Salarie Salarie { get; set; }
            public Noeud? PremierFils { get; set; }
            public Noeud? FrereSuivant { get; set; }

            public Noeud(Salarie salarie)
            {
                this.Salarie = salarie;
                this.PremierFils = null;
                this.FrereSuivant = null;
            }
        }

        private Noeud? racine;

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
                    var nouveauNoeud = new Noeud(salarie);
                    if (noeudManager.PremierFils == null)
                    {
                        noeudManager.PremierFils = nouveauNoeud;
                    }
                    else
                    {
                        Noeud frere = noeudManager.PremierFils;
                        while (frere.FrereSuivant != null)
                        {
                            frere = frere.FrereSuivant;
                        }
                        frere.FrereSuivant = nouveauNoeud;
                    }
                }
                else
                {
                    Console.WriteLine($"Manager {manager.Nom} introuvable.");
                }
            }
        }

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
                    SupprimerNoeud(null, racine, salarie);
                }
            }
        }

        private void SupprimerNoeud(Noeud? parent, Noeud courant, Salarie salarie)
        {
            if (courant.PremierFils != null)
            {
                SupprimerNoeud(courant, courant.PremierFils, salarie);
            }
            if (courant.FrereSuivant != null)
            {
                SupprimerNoeud(parent, courant.FrereSuivant, salarie);
            }
            if (courant.Salarie == salarie)
            {
                if (parent != null)
                {
                    if (parent.PremierFils == courant)
                    {
                        parent.PremierFils = courant.FrereSuivant;
                    }
                    else
                    {
                        Noeud frere = parent.PremierFils;
                        while (frere != null && frere.FrereSuivant != courant)
                        {
                            frere = frere.FrereSuivant;
                        }
                        if (frere != null)
                        {
                            frere.FrereSuivant = courant.FrereSuivant;
                        }
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
            Noeud? noeudManager = TrouverNoeud(racine, manager);
            if (noeudManager != null && noeudManager.PremierFils != null)
            {
                Noeud? courant = noeudManager.PremierFils;
                while (courant != null)
                {
                    subordonnes.Add(courant.Salarie);
                    courant = courant.FrereSuivant;
                }
            }
            return subordonnes;
        }

        private Noeud? TrouverNoeud(Noeud? courant, Salarie salarie)
        {
            if (courant == null) return null;
            if (courant.Salarie == salarie) return courant;
            Noeud? trouve = TrouverNoeud(courant.PremierFils, salarie);
            if (trouve != null) return trouve;
            return TrouverNoeud(courant.FrereSuivant, salarie);
        }

        public void Afficher()
        {
            if (racine != null)
            {
                AfficherNoeud(racine, "");
            }
            else
            {
                Console.WriteLine("Organigramme vide.");
            }
        }

        private void AfficherNoeud(Noeud noeud, string indent)
        {
            Console.WriteLine(indent + noeud.Salarie.Nom + " - " + noeud.Salarie.Poste);
            if (noeud.PremierFils != null)
            {
                AfficherNoeud(noeud.PremierFils, indent + "    ");
            }
            if (noeud.FrereSuivant != null)
            {
                AfficherNoeud(noeud.FrereSuivant, indent);
            }
        }
    }
}
