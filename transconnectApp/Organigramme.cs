    namespace transconnect
    {
        public class Organigramme
        {
            public class Noeud
            {
                private Salarie salarie;
                private Noeud? succ;    
                private Noeud? frere;   

                public Noeud(Salarie salarie, Noeud? succ = null, Noeud? frere = null)
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

                public Noeud? Succ
                {
                    get { return succ; }
                    set { succ = value; }
                }

                public Noeud? Frere
                {
                    get { return frere; }
                    set { frere = value; }
                }

                public override string ToString()
                {
                    return salarie != null ? $"{salarie.Nom} {salarie.Prenom}" : "Noeud vide";
                }

                
            }

            private Noeud? racine;

            public Organigramme(Noeud? racine = null)
            {
                this.racine = racine;
            }

            public Noeud? Racine
            {
                get { return racine; }
                set { racine = value; }
            }
            /// <summary>
            /// Ajoute un salarié en tant que frère du noeud courant.
            /// </summary>
            /// <param name="start"></param>
            /// <param name="salarie"></param>
            /// <returns></returns>
            public bool Ajouter_Frere(Noeud? start, Salarie salarie)
            {
                if (start == null) return false;
                Noeud? courant = start;
                while (courant.Frere != null)
                {
                    courant = courant.Frere;
                }
                courant.Frere = new Noeud(salarie);
                return true;
            }
            /// <summary>
            /// Ajoute un salarié en tant que successeur du noeud courant.
            /// </summary>
            /// <param name="start"></param>
            /// <param name="salarie"></param>
            /// <returns></returns>
            public bool Ajouter_Succ(Noeud? start, Salarie salarie)
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
            /// <summary>
            /// Ajoute un salarié à l'organigramme.
            /// Si un manager est spécifié, le salarié sera ajouté comme successeur de ce manager.
            /// </summary>
            /// <param name="salarie"></param>
            /// <param name="manager"></param>
            public void AjouterSalarie(Salarie salarie, Salarie? manager = null)
            {
                if (racine == null)
                {
                    // premier salarié de l’entreprise
                    racine = new Noeud(salarie);
                    return;
                }

                if (manager!.Equals(racine.Salarie))
                {
                    // nouveau top-manager (ex. second directeur) : frère de la racine
                    Ajouter_Frere(racine.Succ, salarie);
                    return;
                }

                // cas classique : on cherche le manager et on ajoute le successeur
                Noeud? noeudManager = Rechercher(racine, manager);
                if (noeudManager != null)
                    Ajouter_Succ(noeudManager, salarie);
                else
                    Console.WriteLine($"Manager {manager.Nom} introuvable dans l'organigramme.");
            }

            /// <summary>
            /// Affiche l'organigramme à partir du noeud spécifié avec une indentation pour représenter la hiérarchie.
            /// </summary>
            /// <param name="start"></param>
            /// <param name="prefix"></param>
            public void Afficher(Noeud? start, string prefix = "")
            {
                if (start == null) 
                { 
                    return;
                }

                // Afficher le salarié courant
                Console.WriteLine($"{prefix}├─ {start} ({start.Salarie?.Poste}) - #{start.Salarie?.NumeroSS}");
                
                // Afficher ses successeurs (subordonnés directs) avec une indentation supplémentaire
                if (start.Succ != null)
                {
                    Afficher(start.Succ, prefix + "│  ");
                }
                
                // Afficher ses frères (même niveau hiérarchique)
                if (start.Frere != null)
                {
                    Afficher(start.Frere, prefix);
                }
            }
            /// <summary>
            /// Recherche un salarié dans l'organigramme à partir du noeud spécifié.
            /// </summary>
            /// <param name="start"></param>
            /// <param name="salarie"></param>
            /// <returns></returns>
            public Noeud? Rechercher(Noeud? start, Salarie salarie)
            {
                if (start is null) return null;
                if (start.Salarie.Equals(salarie)) return start;

                Noeud? trouve = Rechercher(start.Succ, salarie);
                if (trouve is not null) return trouve;

                return Rechercher(start.Frere, salarie);
            }

            /// <summary>
            /// Recherche un salarié dans l'organigramme à partir de la racine.
            /// </summary>
            /// <param name="salarie"></param>
            /// <returns></returns>
            public Noeud? RechercherSalarie(Salarie salarie)
            {
                if (racine is null) return null;
                return Rechercher(racine, salarie);
            }
            
            /// <summary>
            /// Supprime un salarié de l'organigramme.
            /// </summary>
            /// <param name="salarie"></param>
            public void SupprimerSalarie(Salarie salarie)
            {
                if (racine is null) return;

                if (racine.Salarie.Equals(salarie))
                {
                    racine = null;
                }
                else
                {
                    SupprimerNoeud(null, racine, salarie);
                }
            }
            /// <summary>
            /// Supprime un noeud de l'organigramme.
            /// Cette méthode est appelée récursivement pour parcourir l'organigramme.
            /// </summary>
            /// <param name="parent"></param>
            /// <param name="courant"></param>
            /// <param name="salarie"></param>
            private void SupprimerNoeud(Noeud? parent, Noeud courant, Salarie salarie)
            {
                if (courant is null) return;

                if (courant.Succ is not null)
                {
                    SupprimerNoeud(courant, courant.Succ, salarie);
                }

                if (courant.Frere is not null)
                {
                    SupprimerNoeud(courant, courant.Frere, salarie);
                }

                if (courant.Salarie.Equals(salarie))
                {
                    if (parent != null)
                    {
                        if (parent.Succ is not null && parent.Succ.Equals(courant))
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
            /// <summary>
            /// Modifie le manager d'un salarié dans l'organigramme.
            /// </summary>
            /// <param name="salarie"></param>
            /// <param name="nouveauManager"></param>
            public void ModifierManager(Salarie salarie, Salarie nouveauManager)
            {
                SupprimerSalarie(salarie);
                AjouterSalarie(salarie, nouveauManager);
            }
            /// <summary>
            /// Obtenir la liste des subordonnés d'un manager.
            /// </summary>
            /// <param name="manager"></param>
            /// <returns></returns>
            public List<Salarie> ObtenirSubordonnes(Salarie manager)
            {
                List<Salarie> subordonnes = new List<Salarie>();
                Noeud? managerNoeud = Rechercher(racine, manager);
                if (managerNoeud != null)
                {
                    Noeud? courant = managerNoeud.Succ;
                    while (courant != null)
                    {
                        subordonnes.Add(courant.Salarie!);
                        courant = courant.Frere;
                    }
                }
                return subordonnes;
            }
        }
    }
