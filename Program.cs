using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulationTrafic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Créer une intersection 
            Intersection feuTricoloreIntersection = new Intersection("Feu Tricolore", TypeIntersection.FeuTricolore);

            // Créer  véhicules
            Voiture voiture1 = new Voiture();
            Voiture voiture2 = new Voiture();
            Voiture voiture3 = new Voiture();

            Task task1 = Task.Run(() => SimulerVehicule(feuTricoloreIntersection, voiture1));
            Task task2 = Task.Run(() => SimulerVehicule(feuTricoloreIntersection, voiture2));
            Task task3 = Task.Run(() => SimulerVehicule(feuTricoloreIntersection, voiture3));

            await Task.WhenAll(task1, task2, task3);
        }

        static async Task SimulerVehicule(Intersection intersection, Vehicule vehicule)
        {
            intersection.TraiterEntree(vehicule);

            await Task.Delay(2000); 

            intersection.PasserAuRouge(); // feu rouge
            await Task.Delay(2000); 
            intersection.PasserAuVert();// feu vert
            await Task.Delay(2000); //

            intersection.TraiterSortie(vehicule);
        }
    }
}
