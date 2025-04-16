namespace transconnect

public classe Module_Commande
{
    private List<Commandes> commandes ;

    public Module_Commande(
    {
        this.commandes = new List<Commandes>;
    }

    public void Creer_commande(Client client, string villeDepart, string villeArrivee, DateTime dateCommande, Chauffeur chauffeur, decimal tarifHoraire)
    {
        //Graphe : calcul du trajet + calcul km parcourur

        // Calcul prix de la commande : km parcour + tarif horaire
    }
}