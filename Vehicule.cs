using System;

namespace SimulationTrafic
{
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
