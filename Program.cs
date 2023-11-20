using System;

namespace SimulationTrafic
{
    class Program
    {
        static void Main(string[] args)
        {
            // Créer des intersections
            Intersection rondPoint = new Intersection("Rond-Point", TypeIntersection.RondPoint);
            Intersection cederLePassageIntersection = new Intersection("Céder le Passage", TypeIntersection.CederLePassage);
            Intersection feuTricoloreIntersection = new Intersection("Feu Tricolore", TypeIntersection.FeuTricolore);

            // Créer des véhicules
            Voiture voiture1 = new Voiture();

            // Simuler les actions des véhicules à différentes intersections
            rondPoint.EntrerIntersection(voiture1);
            rondPoint.Attendre(voiture1);
            rondPoint.SortirIntersection(voiture1);

            feuTricoloreIntersection.EntrerIntersection(voiture1);
            feuTricoloreIntersection.SortirIntersection(voiture1);
        }
    }
}
