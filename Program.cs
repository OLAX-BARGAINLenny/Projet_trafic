using SimulationTrafic;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Veuillez choisir une option :");
        Console.WriteLine("1 - Feu Tricolore");
        Console.WriteLine("2 - Passage Piéton");

        // Utilisez Console.ReadLine() pour lire toute la ligne jusqu'à ce que l'utilisateur appuie sur Entrée
        string optionString = Console.ReadLine();

        if (int.TryParse(optionString, out int option))
        {
            Intersection intersection;

            switch (option)
            {
                case 1:
                    intersection = new Intersection("Feu Tricolore", TypeIntersection.FeuTricolore, 4);
                    break;
                case 2:
                    intersection = new Intersection("Passage Piéton", TypeIntersection.CederLePassage, 1);
                    break;
                default:
                    Console.WriteLine("Option invalide. Utilisation du feu tricolore par défaut.");
                    intersection = new Intersection("Feu Tricolore", TypeIntersection.FeuTricolore, 4);
                    break;
            }

            // Créer des véhicules
            Voiture voiture1 = new Voiture("Audi🚕 ");
            Voiture voiture2 = new Voiture("Bmw🚙 ");
            Voiture voiture3 = new Voiture("Porsche🏎️ ");

            Task task1 = Task.Run(() => SimulerVehicule(intersection, voiture1, 0));
            Task task2 = Task.Run(() => SimulerVehicule(intersection, voiture2, 1));
            Task task3 = Task.Run(() => SimulerVehicule(intersection, voiture3, 2));

            await Task.WhenAll(task1, task2, task3);
        }
        else
        {
            Console.WriteLine("Option invalide. Utilisation du feu tricolore par défaut.");
            Intersection intersection = new Intersection("Feu Tricolore", TypeIntersection.FeuTricolore, 4);
            // ... (le reste du code reste inchangé)
        }
    }

    static async Task SimulerVehicule(Intersection intersection, Vehicule vehicule, int entree)
    {
        if (vehicule is Pieton)
        {
            Pieton pieton = vehicule as Pieton;
            intersection.TraiterEntree(pieton);
            await Task.Delay(2000); // Logique pour attendre
            intersection.TraiterPassagePieton(pieton, entree);
        }
        else
        {
            intersection.TraiterEntree(vehicule, entree);

            if (intersection.Type == TypeIntersection.FeuTricolore)
            {
                await Task.Delay(2000);
                intersection.PasserAuRouge();
                await Task.Delay(3000);
                intersection.PasserAuVert();
                await Task.Delay(3000);
            }
            else
            {
                await Task.Delay(2000);
            }

            intersection.TraiterSortie(vehicule, entree);
        }
    }
}
