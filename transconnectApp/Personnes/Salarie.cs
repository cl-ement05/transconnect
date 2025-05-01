namespace transconnect
{  
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

        public override void ModifierInfos()
        {
            base.ModifierInfos();
            Console.Write($"Salaire ({this.salaire}) : ");
            string salaire = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(salaire) && int.TryParse(salaire, out int sal)) this.salaire = sal;

        }

        /// <summary>
        /// Supprime un salarié de l'entreprise et de l'organigramme.
        /// Si le salarié est un chef d'équipe, il doit y avoir au moins 2 chauffeurs sous sa responsabilité.
        /// Si le salarié est un directeur, il ne peut pas être supprimé sans procédure spéciale.
        /// Si le salarié est un chef d'équipe avec un seul chauffeur sous sa responsabilité, le chauffeur sera transféré à un autre chef d'équipe ou au directeur.
        /// </summary>
        /// <param name="dataState"></param>
        public void Licencier(DataState dataState)
        {
            switch (this.Poste)
            {
                case directeur:
                    Console.WriteLine("Suppression impossible : Le directeur ne peut pas être supprimé sans procédure spéciale.");
                    return;

                case chefEquipe:
                    if (this is ChefEquipe chef)
                    {
                        List<Chauffeur> team = chef.ChauffeursSousResponsabilite;

                        if (team.Count >= 2)
                        {
                            Chauffeur plusAncien = team.OrderBy(c => c.DateEntree).First();

                            ChefEquipe nouveauChef = new ChefEquipe(
                                plusAncien.NumeroSS, plusAncien.Nom, plusAncien.Prenom, plusAncien.DateNaissance,
                                plusAncien.AdressePostale, plusAncien.Email, plusAncien.Telephone,
                                plusAncien.DateEntree, plusAncien.Salaire
                            );

                            foreach (Chauffeur chauffeur in team.Where(c => c != plusAncien))
                            {
                                nouveauChef.AssignerChauffeur(chauffeur);
                            }

                            dataState.salaries.Add(nouveauChef);
                            dataState.organigramme.AjouterSalarie(nouveauChef, dataState.directeur);
                            dataState.salaries.Remove(plusAncien);
                        }
                        else if (team.Count == 1)
                        {
                            Chauffeur seulChauffeur = team[0];
                            ChefEquipe? cible = dataState.salaries
                                .OfType<ChefEquipe>()
                                .Where(e => e != chef)
                                .OrderBy(e => e.ChauffeursSousResponsabilite.Count)
                                .FirstOrDefault();

                            if (cible != null)
                            {
                                cible.AssignerChauffeur(seulChauffeur);
                                dataState.organigramme.ModifierManager(seulChauffeur, cible);
                            }
                            else if (dataState.directeur != null)
                            {
                                Console.WriteLine(@"Erreur : aucun autre chef d'équipe n'est disponible
                                pour récupérer les chauffeurs de ce chef. La suppression n'a donc pas lieu");
                                return;
                            }
                        }
                        // Sinon aucun chauffeur sous sa responsabilité
                        dataState.salaries.Remove(chef);
                        dataState.organigramme.SupprimerSalarie(chef);
                    }
                    break;

                default:
                    foreach(Salarie salarie in dataState.salaries) {
                        if (salarie is ChefEquipe) {
                            ChefEquipe manager = (ChefEquipe)salarie;
                            manager.SupprimerChauffeur((Chauffeur)this);
                            break;
                        }
                    }
                    break;
            }
            dataState.directeur!.SupprimerSalarie(this);
            dataState.organigramme.SupprimerSalarie(this);
            dataState.salaries.Remove(this);
        }

        /// <summary>
        /// Ajoute un salarié à l'entreprise et à l'organigramme.
        /// </summary>
        /// <param name="dataState"></param>
        /// <param name="manager"></param>
        public void Embaucher(DataState dataState, Salarie? manager = null)
        {
            if (manager != null && !dataState.salaries.Contains(manager)) {
                Console.WriteLine("Erreur : le manager doit faire partie de l'entreprise");
            }

            if (!dataState.salaries.Contains(this))
            {
                dataState.salaries.Add(this);
                if (this.GetType() == typeof(Chauffeur)) {
                    if (manager is not null && manager.GetType() == typeof(ChefEquipe)) {
                        ChefEquipe chef = (ChefEquipe) manager!;
                        Chauffeur chauffeur = (Chauffeur) this;
                        chef.AssignerChauffeur(chauffeur);
                    } else {
                        Console.WriteLine("Erreur : le manager saisi n'est pas valide");
                    }
                    dataState.directeur!.AjouterSalarie(this);
                } else if (this.GetType() == typeof(ChefEquipe)) {
                    dataState.directeur!.AjouterSalarie(this);
                }
                dataState.organigramme.AjouterSalarie(this, manager);
                Console.WriteLine("Salarié embauché avec succès");
            } else Console.WriteLine("Salarié déjà embauché");
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
