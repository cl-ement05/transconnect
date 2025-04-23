
using transconnect;

namespace unitTest
{
    public class UnitTest1
    {
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
        public void TestSupprimerClient()
        {
            Client client = new Client("1", "Durand", "Emma", DateTime.Now.AddYears(-28), "Toulouse", "emma@exemple.com", "0606060606");
            
            datastate.clients.Add(client);
            Assert.Contains(client, datastate.clients);

            client.SupprimerClient(datastate);
            Assert.DoesNotContain(client, datastate.clients);
        }

        [Fact]
        public void TestChauffeurDisponibilite()
        {
            Chauffeur chauffeur = new Chauffeur("1", "Michel", "Leblanc", DateTime.Now.AddYears(-30), "Bd Magenta", "leblanc@outlook.com", "0154278473", DateTime.Now.AddYears(-5), 2000);
            datastate.salaries.Add(chauffeur);

            Client client = new Client("2", "Marseulli", "Véronique", DateTime.Now.AddYears(-40), "Rue de la Gare", "vero.m@gmail.com", "0698528488");
            Voiture voiture = new Voiture("AB-123-CD", "Rouge", "Peugeot", 4);

            Commande commande = new Commande(1, client, "Lyon", "Nice", voiture, chauffeur, DateTime.Today);
            chauffeur.LivraisonsEffectuees.Add(commande);

            bool dispoAujourdHui = chauffeur.EstDisponible(DateTime.Today);
            bool dispoDemain = chauffeur.EstDisponible(DateTime.Today.AddDays(1));

            Assert.False(dispoAujourdHui);
            Assert.True(dispoDemain);
        }

        [Fact]
        public void TestAfficherSalaries()
        {
            Salarie salarie1 = new ChefEquipe("1", "Lemoine", "Alice", DateTime.Now.AddYears(-40), "Paris", "alice@icloud.com", "0622564636", DateTime.Now.AddYears(-5), 2800);
            Salarie salarie2 = new Chauffeur("2", "Durand", "Lucas", DateTime.Now.AddYears(-35), "Lyon", "lucas@gmail.com", "0622462794", DateTime.Now.AddYears(-3), 2000);

            datastate.salaries.Add(salarie1);
            datastate.salaries.Add(salarie2);

            Salarie.AfficherSalaries(datastate);

            Assert.Equal(2, datastate.salaries.Count);
            Assert.Contains(salarie1, datastate.salaries);
            Assert.Contains(salarie2, datastate.salaries);
        }
    }
}