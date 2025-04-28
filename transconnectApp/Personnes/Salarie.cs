namespace transconnect
{
    public abstract class Salarie : Personne
    {
        public const string directeur = "Directeur";
        public const string chefEquipe = "Chef d'équipe";

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
        /// Modifie les informations d'un salarié
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="dateNaissance"></param>
        /// <param name="adressePostale"></param>
        /// <param name="email"></param>
        /// <param name="telephone"></param>
        /// <param name="salaire"></param>
        public void ModifierSalarieParNumeroSS(string nom, string prenom, DateTime dateNaissance,
                                               string adressePostale, string email, string telephone, decimal salaire)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.adressePostale = adressePostale;
            this.email = email;
            this.telephone = telephone;
            this.salaire = salaire;
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
        /// <summary>
        /// Ajoute un salarié à l'entreprise et à l'organigramme.
        /// </summary>
        /// <param name="dataState"></param>
        /// <param name="manager"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Embaucher(DataState dataState, Salarie? manager = null)
        {
            if (manager != null && !dataState.salaries.Contains(manager))
                throw new ArgumentException("Le manager doit faire partie de l'entreprise");

            if (!dataState.salaries.Contains(this))
            {
                dataState.salaries.Add(this);
                dataState.organigramme.AjouterSalarie(this, manager);
            }
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
                                dataState.directeur.AjouterSalarie(seulChauffeur);
                                dataState.organigramme.ModifierManager(seulChauffeur, dataState.directeur);
                            }
                        }
                        // Sinon aucun chauffeur sous sa responsabilité
                        dataState.salaries.Remove(chef);
                        dataState.organigramme.SupprimerSalarie(chef);
                    }
                    break;

                default:
                    dataState.salaries.Remove(this);
                    dataState.organigramme.SupprimerSalarie(this);
                    break;
            }
        }

        public override string ToString()
        {
            return base.ToString() + $" Poste : {poste}, Salaire : {salaire} €, Date d'entrée : {dateEntree.ToShortDateString()}";
        }
    }
}
