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

        private List<Pieton> pietonsEnAttente = new List<Pieton>();

        public TypeIntersection Type { get; private set; }
        public string Nom { get; private set; }
        public bool FeuVert { get; private set; }

        private SemaphoreSlim[] semaphores; // Tableau de sémaphores pour contrôler l'accès à chaque entrée
        private int nombreEntrees;



        public Intersection(string nom, TypeIntersection type, int nombreEntrees)
    {
        Nom = nom;
        Type = type;
        this.nombreEntrees = nombreEntrees;
        semaphores = new SemaphoreSlim[nombreEntrees];

        for (int i = 0; i < nombreEntrees; i++)
        {
            semaphores[i] = new SemaphoreSlim(1, 1);
        }

        // Initialise le feu tricolore à vert par défaut
        if (Type == TypeIntersection.FeuTricolore)
            FeuVert = true;
    }

        public void TraiterEntree(Pieton pieton)
    {
        if (pieton == null)
        {
            throw new ArgumentNullException(nameof(pieton));
        }

        Console.WriteLine($"{pieton.Name} approche de l'intersection {Nom}.");
        semaphores[0].Wait(); // Attendez le sémaphore pour garantir qu'un piéton entre à la fois
        pietonsEnAttente.Add(pieton);
    }


       public void TraiterPassagePieton(Pieton pieton)
    {
        feuSemaphore.Wait();
        if (!FeuVert)
        {
            Console.WriteLine($"{pieton.Name} traverse le passage piéton à l'intersection {Nom}.");
        }
        feuSemaphore.Release();
    }

        public void TraiterEntree(Vehicule vehicule, int entree)
    {
        Console.WriteLine($"{vehicule.Name} approche de l'intersection {Nom} par l'entrée {entree + 1}.");
        semaphores[entree].Wait(); // Attendez le sémaphore pour garantir que seul un véhicule peut entrer à la fois
        // Logique pour entrer dans l'intersection
    }

        public void TraiterAttente(Vehicule vehicule)
        {
            Console.WriteLine($"{vehicule.Name} attend à l'intersection {Nom} pour un chemin dégagé.");

            // Vérifiez l'état du feu
            while (true)
            {
                feuSemaphore.Wait();
                if (FeuVert)
                {
                    feuSemaphore.Release();
                    break; // Si le feu est vert, sortir de la boucle d'attente
                }
                feuSemaphore.Release();
                Thread.Sleep(1000); 
            }
        }

        public void TraiterSortie(Vehicule vehicule, int sortie)
    {
        Console.WriteLine($"{vehicule.Name} a quitté l'intersection {Nom} par la sortie {sortie + 1}.");
        semaphores[sortie].Release(); // Libérez le sémaphore correspondant à la sortie
    }

        public void PasserAuRouge()
        {
            if (Type == TypeIntersection.FeuTricolore)
            {
                feuSemaphore.Wait(); // Verrouille l'accès au feu tricolore pendant la modification
                FeuVert = false;
                Console.WriteLine($"🔴 Feu tricolore à l'intersection {Nom} passe au rouge.🔴");
                Console.WriteLine("╔═══╗");
                Console.WriteLine("║🔴 ║");
                Console.WriteLine("║⚫ ║");
                Console.WriteLine("║⚫ ║");
                Console.WriteLine("╚╤╤═╝");
                feuSemaphore.Release();
            }
        }

        public void PasserAuVert()
        {
                feuSemaphore.Wait(); // Verrouille l'accès au feu tricolore pendant la modification
                FeuVert = true;
                Console.WriteLine($"🟢 Feu tricolore à l'intersection {Nom} passe au vert. 🟢");
                Console.WriteLine("╔═══╗");
                Console.WriteLine("║⚫ ║");
                Console.WriteLine("║⚫ ║");
                Console.WriteLine("║🟢 ║");
                Console.WriteLine("╚╤╤═╝");
                feuSemaphore.Release();
        }

        public class Pieton : Vehicule
        {
            public Pieton(string name) : base("Piéton", name ?? throw new ArgumentNullException(nameof(name)))
            {
                // Vous pouvez ajouter une logique supplémentaire si nécessaire
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

        internal void TraiterEntree(SimulationTrafic.Pieton pieton)
        {
            throw new NotImplementedException();
        }

        internal void TraiterPassagePieton(SimulationTrafic.Pieton pieton, int entree)
        {
            throw new NotImplementedException();
        }
    }
}
