using System;

public class Voiture : Vehicule
{
    private int nb_passagers;

    public Voiture (string immatriculation, string couleur,string marque,int nb_passagers):base(immatriculation, couleur, marque)
    {
        this.nb_passagers = nb_passagers;
    }

    public Nb_passager {get {return nb_passagers;} set {nb_passagers = value;} }

    public override string ToString ()
    {
        return base.ToString () + " | Nombre de passagers : " + nb_passagers;
    }
}