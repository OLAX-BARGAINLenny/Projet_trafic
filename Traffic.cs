using System;

namespace SimulationTrafic
{
    public enum TypeIntersection
    {
        CederLePassage,
        RondPoint,
        FeuTricolore
    }

    public class Intersection
    {
        public TypeIntersection Type { get; set; }
        public string Nom { get; set; }

        public Intersection(string nom, TypeIntersection type)
        {
            Nom = nom;
            Type = type;
        }

        public void EntrerIntersection(Vehicule vehicule)
        {
            Console.WriteLine($"{vehicule.Type} approche de l'intersection {Nom}.");
            // Logique pour entrer dans l'intersection
        }

        public void Attendre(Vehicule vehicule)
        {
            Console.WriteLine($"{vehicule.Type} attend à l'intersection {Nom} pour un chemin dégagé.");
            // Logique pour attendre à l'intersection
        }

        public void SortirIntersection(Vehicule vehicule)
        {
            Console.WriteLine($"{vehicule.Type} a quitté l'intersection {Nom}.");
            // Logique pour sortir de l'intersection
        }
    }

    public class Vehicule
    {
        public string Type { get; set; }

        public Vehicule(string type)
        {
            Type = type;
        }

        public void Bouger()
        {
            Console.WriteLine($"{Type} se déplace.");
            // Logique pour le mouvement du véhicule
        }
    }

    public class Voiture : Vehicule
    {
        public Voiture() : base("Voiture")
        {
        }
    }

    public class Piéton : Vehicule
    {
        public Piéton() : base("Piéton")
        {
        }
    }
}
