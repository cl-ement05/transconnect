namespace transconnect
{
    public class Directeur : Salarie
    {     
        private List<Salarie> salariesSousGestion;

        public List<Salarie> SalariesSousGestion
        {
            get { return salariesSousGestion; }
            set { salariesSousGestion = value; }
        }

        public Directeur(string numeroSS, string nom, string prenom, DateTime dateNaissance,
                         string adressePostale, string email, string telephone,
                         DateTime dateEntree, decimal salaire)
            : base(numeroSS, nom, prenom, dateNaissance, adressePostale, email, telephone, dateEntree, Salarie.directeur, salaire)
        {
            this.salariesSousGestion = new List<Salarie>();
        }

        /// <summary>
        /// Ajoute un salarié sous la gestion du directeur.
        /// </summary>
        /// <param name="salarie"></param>
        public void AjouterSalarie(Salarie salarie)
        {
            salariesSousGestion.Add(salarie);
        }

        /// <summary>
        /// Supprime un salarié de la gestion du directeur.
        /// </summary>
        /// <param name="salarie"></param>
        public void SupprimerSalarie(Salarie salarie)
        {
            salariesSousGestion.Remove(salarie);
        }

        /// <summary>
        /// Affiche les salariés sous la gestion du directeur.
        /// </summary>
        public void AfficherSalariesSousGestion()
        {
            Console.WriteLine($"Salariés sous la gestion de {Nom} {Prenom} :");
            foreach (Salarie salarie in salariesSousGestion)
            {
                Console.WriteLine(salarie.ToString());
            }
        }

        public override string ToString()
        {
            return base.ToString() + $" | Nombre de salariés sous gestion : {salariesSousGestion.Count}";
        }
    }
}
