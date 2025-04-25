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

        public DateTime DateEntree
        {
            get { return dateEntree; }
            set { dateEntree = value; }
        }

        public string Poste
        {
            get { return poste; }
            set { poste = value; }
        }

        public decimal Salaire
        {
            get { return salaire; }
            set { salaire = value; }
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
        /// Recherche un salarié par son numéro de sécurité sociale.
        /// </summary>
        /// <param name="numeroSS"></param>
        /// <returns></returns>
        public static Salarie? RechercherSalarieParNumeroSS(DataState dataState, string numeroSS)
        {
            return dataState.salaries.Find(s => s.NumeroSS == numeroSS);
        }

        /// <summary>
        /// Affiche la liste des salariés de l'entreprise.
        /// </summary>

        public static void AfficherSalaries(DataState dataState)
        {
            Console.WriteLine("Liste des salariés :");
            foreach (Salarie salarie in dataState.salaries)
            {
                Console.WriteLine(salarie.ToString() +"\n");
            }
        }

        /// <summary>
        /// Ajoute ce salarié à l'entreprise et l'ajoute à l'organigramme.
        /// </summary>
        /// <param name="salarie"></param>
        /// <param name="manager"></param>
        public void Embaucher(DataState dataState, Salarie? manager = null)
        {
            if (manager != null && !dataState.salaries.Contains(manager)) {
                throw new ArgumentException("Le manager doit faire partie de l'entreprise");
            }
            if (!dataState.salaries.Contains(this)) {
                if (dataState.directeur != null) {
                    dataState.directeur.AjouterSalarie(this);
                }
                dataState.salaries.Add(this);
                dataState.organigramme.AjouterSalarie(this, manager);
            }
        }

        /// <summary>
        /// Supprime ce salarié de l'entreprise et de l'organigramme.
        /// </summary>
        /// <param name="salarie"></param>
        public void Licencier(DataState dataState)
        {
            //dataState.salaries.Remove(this);
            //dataState.organigramme.SupprimerSalarie(this);
            switch(this.Poste)
            {
                case directeur:
                    Console.WriteLine("Suppression impossible : le directeur ne peux pas être supprimer");
                    break;
                 
                case chefEquipe:
                    ChefEquipe chef=(ChefEquipe)this;
                    List<Chauffeur> team=chef.ChauffeursSousResponsabilite;

                    if(team.Count >=2)
                    {
                        Chauffeur plusAncien = team.OrderBy(c=>c.DateEntree).First();
                        ChefEquipe nouveauChef=new ChefEquipe(plusAncien.NumeroSS, plusAncien.Nom, plusAncien.Prenom, plusAncien.DateNaissance, plusAncien.AdressePostale,
                                                                plusAncien.Email, plusAncien.Telephone, plusAncien.DateEntree,plusAncien.Salaire);
                        
                        foreach(Chauffeur c in team.Where(c => c!=plusAncien))
                        {
                            nouveauChef.AssignerChauffeur(c);
                        }

                        dataState.salaries.Add(nouveauChef);
                        dataState.organigramme.AjouterSalarie(nouveauChef, dataState.directeur);
                        dataState.salaries.Remove(chef);
                        dataState.organigramme.SupprimerSalarie(chef);
                    }

                    else if (team.Count==1)
                    {
                        Chauffeur seulChauffeur = team[0];
                        
                        ChefEquipe? cible = dataState.salaries.OfType<ChefEquipe>().Where(e=> e!=chef).OrderBy(e=>e.ChauffeursSousResponsabilite.Count).FirstOrDefault();
                        if(cible !=null)
                        {
                            cible.AssignerChauffeur(seulChauffeur);
                            dataState.organigramme.AjouterSalarie(seulChauffeur,cible);
                        }

                        else
                        {
                            dataState.directeur!.AjouterSalarie(seulChauffeur);
                            dataState.organigramme.AjouterSalarie(seulChauffeur,dataState.directeur);

                        }

                        dataState.salaries.Remove(chef);
                        dataState.organigramme.SupprimerSalarie(chef);
                    }
                    else
                    {
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
            return base.ToString() + $"Poste : {poste}, Salaire : {salaire} euros, Date d'entrée : {dateEntree.ToShortDateString()}";
        }
    }
}
