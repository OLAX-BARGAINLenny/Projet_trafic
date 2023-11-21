using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SimulationTrafic
{
    class Program
    {
        static async Task Main(string[] args)
        {


            Intersection feuTricoloreIntersection = new Intersection("Feu Tricolore", TypeIntersection.FeuTricolore);
            Intersection ClasicIntersection = new Intersection("Clasic", TypeIntersection.CederLePassage);

            // Créer  véhicules
            Voiture voiture1 = new Voiture("Audi🚕");
            Voiture voiture2 = new Voiture("Bmw🚙");
            Voiture voiture3 = new Voiture("Porsche🏎️ ");


            Task task1 = Task.Run(() => SimulerVehicule(feuTricoloreIntersection, voiture1));
            Task task2 = Task.Run(() => SimulerVehicule(feuTricoloreIntersection, voiture2));
            Task task3 = Task.Run(() => SimulerVehicule(feuTricoloreIntersection, voiture3));

         

            await Task.WhenAll(task1, task2, task3);

            
            await Task.WhenAll(task1, task2, task3);
        }
        static async Task SimulerVehicule(Intersection intersection, Vehicule vehicule)
        {
            intersection.TraiterEntree(vehicule);
if (intersection.Type == TypeIntersection.FeuTricolore)
            {
        await Task.Delay(2000); 
                intersection.PasserAuRouge();
                await Task.Delay(3000);
                intersection.PasserAuVert();
                await Task.Delay(3000);

            intersection.TraiterSortie(vehicule);            }
            else
            {
                await Task.Delay(2000);
            }

            
        }
    }
}
