using System;
using System.Threading;

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
        private Random random = new Random();
        private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1); // Sémaphore pour contrôler l'accès à l'intersection
        private SemaphoreSlim feuSemaphore = new SemaphoreSlim(1, 1); // Sémaphore pour contrôler l'accès au feu tricolore

        public TypeIntersection Type { get; private set; }
        public string Nom { get; private set; }
        public bool FeuVert { get; private set; }

        public Intersection(string nom, TypeIntersection type)
        {
            Nom = nom;
            Type = type;

            // Initialise le feu tricolore à vert par défaut
            if (Type == TypeIntersection.FeuTricolore)
                FeuVert = true;
        }

        public void TraiterEntree(Vehicule vehicule)
        {
            Console.WriteLine($"{vehicule.Type} approche de l'intersection {Nom}.");
            semaphore.Wait(); // Attendez le sémaphore pour garantir que seul un véhicule peut entrer à la fois
            // Logique pour entrer dans l'intersection
        }

        public void TraiterAttente(Vehicule vehicule)
        {
            Console.WriteLine($"{vehicule.Type} attend à l'intersection {Nom} pour un chemin dégagé.");

            // Vérifiez l'état du feu
            while (true)
            {
                feuSemaphore.Wait(); // Attendez le sémaphore pour garantir une modification atomique de l'état du feu
                if (FeuVert)
                {
                    feuSemaphore.Release();
                    break; // Si le feu est vert, sortir de la boucle d'attente
                }
                feuSemaphore.Release();
                Console.WriteLine($"Le feu est rouge à l'intersection {Nom}. {vehicule.Type} attend.");
                Thread.Sleep(1000); 
            }
        }

        public void TraiterSortie(Vehicule vehicule)
        {
            int sortieChoisie = ChoisirSortieAleatoire();
            Console.WriteLine($"{vehicule.Type} a quitté l'intersection {Nom} par la sortie {sortieChoisie + 1}.");
            semaphore.Release(); // Libérer le sémaphore pour permettre à d'autres véhicules d'entrer
            // Logique pour sortir de l'intersection en utilisant la sortie choisie
            // (ajoutez la logique appropriée en fonction de votre simulation).
        }

        public void PasserAuRouge()
        {
            if (Type == TypeIntersection.FeuTricolore)
            {
                feuSemaphore.Wait(); // Verrouille l'accès au feu tricolore pendant la modification
                FeuVert = false;
                Console.WriteLine($"🔴Feu tricolore à l'intersection {Nom} passe au rouge.🔴");
                feuSemaphore.Release();
            }
        }

        public void PasserAuVert()
        {
            if (Type == TypeIntersection.FeuTricolore)
            {
                feuSemaphore.Wait(); // Verrouille l'accès au feu tricolore pendant la modification
                FeuVert = true;
                Console.WriteLine($"🟢Feu tricolore à l'intersection {Nom} passe au vert.🟢");
                feuSemaphore.Release();
            }
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
                    return 3;
                case TypeIntersection.FeuTricolore:
                    return 4; 
                default:
                    throw new InvalidOperationException("Type d'intersection non pris en charge.");
            }
        }
    }
}
