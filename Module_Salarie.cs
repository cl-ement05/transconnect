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

        public void EmbaucherSalarie(Salarie salarie, Salarie? manager = null)
        {
            salaries.Add(salarie);
            organigramme.AjouterSalarie(salarie, manager);
        }

        public void LicencierSalarie(Salarie salarie)
        {
            salaries.Remove(salarie);
            organigramme.SupprimerSalarie(salarie);
        }

        public Salarie? RechercherSalarieParNumeroSS(string numeroSS)
        {
            return salaries.Find(s => s.NumeroSS == numeroSS);
        }

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

        public void AfficherSalaries()
        {
            Console.WriteLine("Liste des salariés :");
            foreach (Salarie salarie in salaries)
            {
                Console.WriteLine(salarie.ToString());
            }
        }

        public void AfficherOrganigramme()
        {
            Console.WriteLine("Organigramme de l'entreprise :");
            organigramme.Afficher();
        }
    }

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

        public void SupprimerSalarie(Salarie salarie)
        {
            if (racine != null)
            {
                if (racine.Salarie == salarie)
                {
                    racine = null; // Supprime la racine
                }
                else
                {
                    SupprimerNoeud(racine, salarie);
                }
            }
        }

        public void Afficher()
        {
            if (racine != null)
            {
                AfficherNoeud(racine, 0);
            }
            else
            {
                Console.WriteLine("L'organigramme est vide.");
            }
        }

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

        private void AfficherNoeud(Noeud noeud, int niveau)
        {
            Console.WriteLine(new string('-', niveau * 2) + noeud.Salarie.Nom + " (" + noeud.Salarie.Poste + ")");
            foreach (Noeud subordonne in noeud.Subordonnes)
            {
                AfficherNoeud(subordonne, niveau + 1);
            }
        }
    }
}