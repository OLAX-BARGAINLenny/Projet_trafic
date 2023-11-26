using System;

namespace SimulationTrafic
{
    public class Vehicule
    {
        public string Type { get; private set; }
        public string Name { get; private set; }

        public Vehicule(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public void Bouger()
        {
            Console.WriteLine($"{Type} {Name} est sortie.");
            // Logique pour le mouvement du véhicule
        }
    }

    public class Voiture : Vehicule
    {
        internal static string voiture1;

        public Voiture(string name) : base("Voiture", name)
        {
            // Vous pouvez ajouter une logique supplémentaire si nécessaire
        }
    }

    public class Piéton : Vehicule
    {
        public Piéton(string name) : base("Piéton", name)
        {
            // Vous pouvez ajouter une logique supplémentaire si nécessaire
        }
    }
    public class Pieton : Vehicule
    {
        public Pieton(string name) : base("Piéton", name ?? throw new ArgumentNullException(nameof(name)))
        {
            // Vous pouvez ajouter une logique supplémentaire si nécessaire
        }
    }
}


