using System.Drawing.Text;

namespace transconnect
{
    public class Module_Vehicule
    {
        private List<Vehicule> flotte;

        public Module_Vehicule()
        {
            flotte = new List<Vehicule>();
            flotte.Add(new Voiture("AB-123-CD", "Rouge", "Toyota", 5));
            flotte.Add(new Camionette("EF-456-GH", "Blanc", "Renault", "Transport de matériaux"));
            flotte.Add(new CamionCiterne("IJ-789-KL", "Bleu", "Volvo", 50.0, "Produits chimiques"));
            flotte.Add(new CamionBenne("MN-012-OP", "Vert", "Scania", 2, true));
            flotte.Add(new CamionFrigorifique("QR-345-ST", "Blanc", "Mercedes", 30.0, 2));
        }

        public Vehicule SelectionnerVehicule()
        {
            Console.WriteLine("Liste des véhicules disponibles : ");
            for(int i=0;i<flotte.Count;i++)
            {
                Console.WriteLine(i+" : "+flotte[i]);
            }
            Console.WriteLine("Saisir le véhicule voulu");
            int index=Convert.ToInt32(Console.ReadLine());
            return flotte[index];
        }

        public void AjouterVehicule(Vehicule v)
        {
            if(v!= null)
            {
                flotte.Add(v);
            }
        }

        public void RetirerVehicule(Vehicule v)
        {
            foreach (Vehicule ve in flotte)
            {
                if(ve.Immatriculation==v.Immatriculation)
                {
                    flotte.Remove(v);
                }
                else
                {
                    Console.WriteLine("Aucun véhicule trouvé");
                }
            }
        }
    }
}