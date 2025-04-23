
using transconnect;


namespace unitTest;

public class UnitTest1
{
    private DataState datastate;
    private Module_statistiques module;

        public void Setup()
        {
            datastate = new DataState(new Dictionary<string, List<(string data, int weight)>>());
            module = new Module_statistiques(datastate);
        }
        public void TestAfficherLivraisonsParChauffeur()
        {
            Chauffeur chauffeur = new Chauffeur("1", "Michel", "Leblanc", DateTime.Now.AddYears(-30),"Bd Magenta", "leblanc@outlook.com", "0154278473", DateTime.Now.AddYears(-5), 2000);
            datastate.salaries.Add(chauffeur);

            Client client = new Client("1", "Marseulli", "Véronique", DateTime.Now.AddYears(-40),"Rue de la Gare", "vero.m@gmail.comm", "0698528488");
            Voiture voiture = new Voiture("AB-123-CD", "Rouge", "Peugeot", 4);
            Commande commande = new Commande(1, client, "Paris", "Lyon", voiture, chauffeur, DateTime.Today);
            chauffeur.LivraisonsEffectuees.Add(commande);

            module.AfficherLivraisonsParChauffeur();

            Assert.AreEqual(1, chauffeur.LivraisonsEffectuees.Count);
        }
}
