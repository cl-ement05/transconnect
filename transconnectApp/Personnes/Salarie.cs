using System;

namespace transconnect
{  
    /// <summary>
    /// Classe abstraite représentant un salarié de l'entreprise.
    /// Elle hérite de la classe Personne et contient des informations spécifiques aux salariés.
    /// </summary>
    public abstract class Salarie : Personne
    {
        public const string directeur = "Directeur";
        public const string chefEquipe = "Chef d'équipe";
        public const string chauffeur = "Chauffeur";

        private DateTime dateEntree;
        private string poste;
        private decimal salaire;

        public Salarie(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                       string adressePostale, string email, string telephone,
                       DateTime dateEntree, string poste, decimal salaire)
            : base(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone)
        {
            this.dateEntree = dateEntree;
            this.poste = poste;
            this.salaire = salaire;
        }

        public DateTime DateEntree { get => dateEntree; set => dateEntree = value; }
        public string Poste { get => poste; set => poste = value; }
        public decimal Salaire { get => salaire; set => salaire = value; }
        
        /// <summary>
        /// Méthode pour modifier les informations d'un salarié.
        /// </summary>
        public override void ModifierInfos(DataState dataState)
        {
            // Sauvegarde des anciennes informations
            string ancienPoste = this.poste;

            // Appel à la méthode de base pour modifier les informations générales
            base.ModifierInfos(dataState);

            // Modification du salaire
            Console.Write($"Salaire ({salaire}) : ");
            string saisie = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(saisie) &&
                decimal.TryParse(saisie, out var nouveauSalaire))
            {
                salaire = nouveauSalaire;
            }

            // Modification du poste
            Console.Write($"Poste ({poste}) : ");
            saisie = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(saisie))
            {
                poste = saisie;
            }

            // Mise à jour de l'organigramme si le poste a changé
            if (ancienPoste != poste)
            {
                // Supprimer le salarié de l'organigramme
                dataState.organigramme.SupprimerSalarie(this);

                if (poste == Salarie.chefEquipe)
                {
                    // Ajouter le salarié comme frère des autres chefs d'équipe et successeur du directeur
                    var directeurNoeud = dataState.organigramme.RechercherSalarie(dataState.directeur);
                    if (directeurNoeud != null)
                    {
                        dataState.organigramme.Ajouter_Succ(directeurNoeud, this);
                        Console.WriteLine($"Le salarié {Nom} {Prenom} a été promu au poste de Chef d'équipe.");
                    }
                }
                else if (poste == Salarie.chauffeur)
                {
                    // Demander le numéro de SS du chef d'équipe
                    Console.Write("Entrez le numéro de sécurité sociale du chef d'équipe : ");
                    string numeroSSChef = Console.ReadLine()!;
                    var chefEquipe = dataState.salaries
                        .FirstOrDefault(s => s.NumeroSS == numeroSSChef && s.Poste == Salarie.chefEquipe);

                    if (chefEquipe != null)
                    {
                        // Ajouter le chauffeur en tant que successeur du chef d'équipe
                        var chefNoeud = dataState.organigramme.RechercherSalarie(chefEquipe);
                        if (chefNoeud != null)
                        {
                            dataState.organigramme.Ajouter_Succ(chefNoeud, this);
                            Console.WriteLine($"Le chauffeur {Nom} {Prenom} a été ajouté sous le chef d'équipe {chefEquipe.Nom} {chefEquipe.Prenom}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Chef d'équipe introuvable. Le poste n'a pas été mis à jour.");
                        poste = ancienPoste; // Restaurer l'ancien poste si le chef d'équipe est introuvable
                    }
                }
                else if (poste == Salarie.directeur)
                {
                    // Vérifier s'il existe déjà un directeur
                    if (dataState.directeur != null && !dataState.directeur.Equals(this))
                    {
                        Console.WriteLine("Erreur : il ne peut y avoir qu'un seul directeur dans l'entreprise.");
                        poste = ancienPoste; // Restaurer l'ancien poste
                    }
                    
                }
                else
                {
                    // Ajouter le salarié sous son manager habituel (par exemple, le directeur)
                    Salarie? manager = dataState.salaries.FirstOrDefault(s => s.Poste == Salarie.directeur);
                    dataState.organigramme.AjouterSalarie(this, manager);
                }
            }

            Console.WriteLine("Les informations ont été mises à jour.");
        }

        /// <summary>
        /// Méthode pour licencier un salarié.
        /// Elle vérifie si le salarié peut être licencié en fonction de son poste et de ses responsabilités.
        /// Si le salarié est un chauffeur, elle vérifie également s'il a des commandes actives.
        /// Si le salarié est un chef d'équipe, elle gère la réaffectation de ses chauffeurs sous responsabilité.
        /// Si le salarié est le directeur, elle affiche un message d'erreur.
        /// </summary>
        /// <param name="dataState"></param>
        public void Licencier(DataState dataState)
        {
            if (this is Chauffeur chActif &&
                dataState.commandes.Any(cmd => cmd.Chauffeur.Equals(chActif)))
            {
                Console.WriteLine("Impossible de licencier : ce chauffeur possède encore des commandes actives.");
                return;
            }

            if (Poste == directeur)
            {
                Console.WriteLine("Suppression impossible : le directeur nécessite une procédure spéciale.");
                return;
            }

            if (this is ChefEquipe chef)
            {
                var team = chef.ChauffeursSousResponsabilite;

                // ≥2 chauffeurs : promotion du plus ancien
                if (team.Count >= 2)
                {
                    var doyen = team.OrderBy(c => c.DateEntree).First();
                    var nouveauChef = new ChefEquipe(doyen.NumeroSS, doyen.Nom, doyen.Prenom, doyen.DateNaissance,
                                                     doyen.AdressePostale, doyen.Email, doyen.Telephone,
                                                     doyen.DateEntree, doyen.Salaire);

                    foreach (Chauffeur c in team.Where(c => !c.Equals(doyen)))
                        nouveauChef.AssignerChauffeur(c);

                    dataState.salaries.Remove(doyen);
                    dataState.salaries.Add(nouveauChef);
                    dataState.organigramme.AjouterSalarie(nouveauChef, dataState.directeur);
                    foreach (Chauffeur c in nouveauChef.ChauffeursSousResponsabilite) dataState.organigramme.AjouterSalarie(c, nouveauChef);
                    Console.WriteLine($"Chauffeur promu : {nouveauChef.Nom} {nouveauChef.Prenom}");
                }
                else if (team.Count == 1)
                {
                    var seul = team[0];
                    var cible = dataState.salaries
                                         .OfType<ChefEquipe>()
                                         .Where(c => !c.Equals(chef))
                                         .OrderBy(c => c.ChauffeursSousResponsabilite.Count)
                                         .FirstOrDefault();

                    if (cible is null)
                    {
                        Console.WriteLine("Aucun chef d'équipe disponible pour reprendre le chauffeur ; licenciement annulé.");
                        return;
                    }

                    chef.SupprimerChauffeur(seul);
                    cible.AssignerChauffeur(seul);
                    dataState.organigramme.ModifierManager(seul, cible);
                    Console.WriteLine($"Nouveau manager pour reprendre le chauffeur : {cible.Nom} {cible.Prenom}");
                }

                RetirerDesCollections(dataState, chef);
                return;
            }

            if (this is Chauffeur chauffeur)
            {
                var chefProprietaire = dataState.salaries
                                               .OfType<ChefEquipe>()
                                               .FirstOrDefault(c => c.ChauffeursSousResponsabilite.Contains(chauffeur));
                chefProprietaire?.SupprimerChauffeur(chauffeur);
            }

            RetirerDesCollections(dataState, this);
        }

        /// <summary>
        /// Retire le salarié des collections de l'entreprise et de l'organigramme.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="s"></param>
        private static void RetirerDesCollections(DataState ds, Salarie s)
        {
            ds.organigramme.SupprimerSalarie(s);
            ds.salaries.Remove(s);
            Console.WriteLine("Salarié licencié avec succès");
        }

        /// <summary>
        /// Ajoute un salarié à l'entreprise et à l'organigramme.
        /// </summary>
        /// <param name="dataState"></param>
        /// <param name="manager"></param>
        public void Embaucher(DataState dataState, Salarie? manager = null)
        {
            if (dataState.salaries.Any(s => s.NumeroSS == NumeroSS))
            {
                Console.WriteLine("Erreur : un salarié possède déjà ce numéro de SS");
                return;
            }

            if (manager != null && !dataState.salaries.Contains(manager))
            {
                Console.WriteLine("Erreur : le manager doit faire partie de l'entreprise");
                return;
            }

            if (manager is null && dataState.directeur is not null) {
                Console.WriteLine("Erreur : tout salarié (hors directeur) doit avoir un supérieur hiérarchique");
            }

            if (this is Chauffeur chauffeurInstance)
            {
                if (manager is not ChefEquipe chef)
                {
                    Console.WriteLine("Erreur : un chauffeur doit avoir un chef d'équipe attitré");
                    return;
                }

                dataState.salaries.Add(this);
                chef.AssignerChauffeur(chauffeurInstance);
                dataState.organigramme.AjouterSalarie(chauffeurInstance, chef);
            }
            else
            {
                dataState.salaries.Add(this);
                dataState.organigramme.AjouterSalarie(this, manager);
            }

            Console.WriteLine("Salarié embauché avec succès");
        }

        /// <summary>
        /// Recherche un salarié par son numéro de sécurité sociale
        /// </summary>
        /// <param name="dataState"></param>
        /// <param name="numeroSS"></param>
        /// <returns></returns>
        public static Salarie? RechercherSalarieParNumeroSS(DataState dataState, string numeroSS)
        {
            return dataState.salaries.Find(s => s.NumeroSS == numeroSS);
        }

        /// <summary>
        /// Affiche la liste des salariés
        /// </summary>
        /// <param name="dataState"></param>
        public static void AfficherSalaries(DataState dataState)
        {
            Console.WriteLine("Liste des salariés :");
            foreach (Salarie salarie in dataState.salaries)
            {
                Console.WriteLine(salarie.ToString() + "\n");
            }
        }

        public override string ToString()
        {
            return base.ToString() + $" Poste : {poste}, Salaire : {salaire} euros, Date d'entrée : {dateEntree.ToShortDateString()}";
        }
    }
}
