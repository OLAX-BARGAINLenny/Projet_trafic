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
            Piéton piéton1 = new Piéton();

            // Simuler les actions des véhicules à différentes intersections
            rondPoint.EntrerIntersection(voiture1);
            rondPoint.Attendre(voiture1);
            rondPoint.SortirIntersection(voiture1);

            cederLePassageIntersection.EntrerIntersection(voiture1);
            cederLePassageIntersection.Attendre(voiture1);
            cederLePassageIntersection.SortirIntersection(voiture1);

            feuTricoloreIntersection.EntrerIntersection(piéton1);
            feuTricoloreIntersection.Attendre(piéton1);
            feuTricoloreIntersection.SortirIntersection(piéton1);
        }
    }
}
