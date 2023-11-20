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
        private Random random = new Random(); // Une seule instance de Random

        public TypeIntersection Type { get; set; }
        public string Nom { get; set; }

        public Intersection(string nom, TypeIntersection type)
        {
            Nom = nom;
            Type = type;
        }

        public void EntrerIntersection(Vehicule vehicule)
        {
            Console.WriteLine($"{vehicule.Type} arrive à l'intersection {Nom}.");
            // Logique pour entrer dans l'intersection
        }

        public void Attendre(Vehicule vehicule)
        {
            Console.WriteLine($"{vehicule.Type} attend à l'intersection {Nom} pour un chemin dégagé.");
            // Logique pour attendre à l'intersection
        }

        public void SortirIntersection(Vehicule vehicule)
        {

            int sortieChoisie = ChoisirSortieAleatoire();
            Console.WriteLine($"{vehicule.Type} a quitté l'intersection {Nom} par la sortie {sortieChoisie + 1}.");

        }

        private int ChoisirSortieAleatoire()
        {
            return random.Next(0, NombreTotalSorties());
        }

        private int NombreTotalSorties()
        {
            switch (Type)
            {
                case TypeIntersection.RondPoint:
                    return 4;
                case TypeIntersection.CederLePassage:
                    return 1; 
                case TypeIntersection.FeuTricolore:
                    return 3; 
                default:
                    throw new InvalidOperationException("Type d'intersection non pris en charge.");
            }
        }
    }
}
