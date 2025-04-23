
using transconnect;

namespace unitTest
{
    public class UnitTest1
    {
        string[] villes = { "Paris", "Lyon", "Marseille", "Bordeaux" };
        int[,] matrice = new int[4, 4] { {  0, 393, 660, 499 }, {393,   0, 277,   0 }, {660, 277,   0,   0 }, {499,   0,   0,   0 } };
        private DataState datastate;
        private Module_statistiques module;

        public UnitTest1()
        {
            datastate = new DataState(new Dictionary<string, List<(string data, int weight)>>());
            module = new Module_statistiques(datastate);
        }

        [Fact]
        public void TestAfficherLivraisonsParChauffeur()
        {
            Chauffeur chauffeur = new Chauffeur("1", "Michel", "Leblanc", DateTime.Now.AddYears(-30), "Bd Magenta", "leblanc@outlook.com", "0154278473", DateTime.Now.AddYears(-5), 2000);
            datastate.salaries.Add(chauffeur);

            Client client = new Client("1", "Marseulli", "Véronique", DateTime.Now.AddYears(-40), "Rue de la Gare", "vero.m@gmail.com", "0698528488");
            Voiture voiture = new Voiture("AB-123-CD", "Rouge", "Peugeot", 4);
            Commande commande = new Commande(1, client, "Paris", "Lyon", voiture, chauffeur, DateTime.Today);
            chauffeur.LivraisonsEffectuees.Add(commande);

            module.AfficherLivraisonsParChauffeur();

            Assert.Single(chauffeur.LivraisonsEffectuees);
        }

        [Fact]
        public void TestAfficherCommandesParPeriode()
        {
            Chauffeur chauffeur = new Chauffeur("1", "Michel", "Leblanc", DateTime.Now.AddYears(-30), "Bd Magenta", "leblanc@outlook.com", "0154278473", DateTime.Now.AddYears(-5), 2000);
            datastate.salaries.Add(chauffeur);

            Client client = new Client("2", "Marseulli", "Véronique", DateTime.Now.AddYears(-40), "Rue de la Gare", "vero.m@gmail.com", "0698528488");
            Voiture voiture = new Voiture("AB-123-CD", "Rouge", "Peugeot", 4);

            Commande commande1 = new Commande(1, client, "Paris", "Lyon", voiture, chauffeur, new DateTime(2025, 4, 10));
            Commande commande2 = new Commande(2, client, "Lille", "Nice", voiture, chauffeur, new DateTime(2024, 1, 1));

            datastate.commandes.Add(commande1);
            datastate.commandes.Add(commande2);

            DateTime debut = new DateTime(2025, 4, 1);
            DateTime fin = new DateTime(2025, 4, 30);
            module.AfficherCommandesParPeriode(debut, fin);

            int nbCommandesDansPeriode = datastate.commandes.FindAll(c => c.DateCommande.Date >= debut && c.DateCommande.Date <= fin).Count;
            Assert.Equal(1, nbCommandesDansPeriode);
        }
        
        [Fact]
        public void TestMoyennePrixCommandes()
        {
            Chauffeur chauffeur = new Chauffeur("1", "Michel", "Leblanc", DateTime.Now.AddYears(-30), "Bd Magenta", "leblanc@outlook.com", "0154278473", DateTime.Now.AddYears(-5), 2000);
            datastate.salaries.Add(chauffeur);

            Client client = new Client("2", "Marseulli", "Véronique", DateTime.Now.AddYears(-40),"Rue de la Gare", "vero.m@gmail.com", "0698528488");
            Voiture voiture = new Voiture("AB-123-CD", "Rouge", "Peugeot", 4);

            datastate.commandes.Add(new Commande(1, client, "Paris", "Lyon", voiture, chauffeur, DateTime.Today));
            datastate.commandes.Add(new Commande(2, client, "Paris", "Marseille", voiture, chauffeur, DateTime.Today)); 
            datastate.commandes.Add(new Commande(3, client, "Paris", "Bordeaux", voiture, chauffeur, DateTime.Today));
            datastate.commandes.Add(new Commande(4, client, "Lyon", "Marseille", voiture, chauffeur, DateTime.Today));

            double tarif = chauffeur.TarifHoraire;
            double total = (393 + 660 + 499 + 277) * tarif;
            double moyenne_theorique = total / 4;

            double somme = 0;
            foreach (Commande c in datastate.commandes)
            {
                somme += c.CalculerPrixCommande(datastate);
            }
            double moyenne = somme / datastate.commandes.Count;
            
            Assert.Equal(moyenne_theorique, moyenne, 3);
        }
    }
}