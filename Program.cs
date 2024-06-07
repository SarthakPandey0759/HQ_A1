using System;

namespace VirtualPetGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Pet myPet = new Pet();

            Console.WriteLine("Welcome to Virtual Pet Game!");
            Console.WriteLine("Enter your pet's name:");
            string petName = Console.ReadLine();
            Console.WriteLine("Choose your pet's type (1 - Cat, 2 - Dog, 3 - Rabbit):");
            PetType petType = (PetType)Enum.Parse(typeof(PetType), Console.ReadLine());

            myPet.Create(petName, petType);
            Console.WriteLine($" Hey ! Welcome, {myPet.Name} the {myPet.Type}!");

            while (myPet.IsAlive)
            {
                DisplayStats(myPet);

                Console.WriteLine("Choose an action:");
                Console.WriteLine("1 - Feed");
                Console.WriteLine("2 - Play");
                Console.WriteLine("3 - Rest");
                Console.WriteLine("4 - Exit");

                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        myPet.Feed();
                        break;
                    case 2:
                        myPet.Play();
                        break;
                    case 3:
                        myPet.Rest();
                        break;
                    case 4:
                        Console.WriteLine("Thank You, its end !");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

                myPet.Update();
            }

            Console.WriteLine("WARNING : Your pet has passed away. Game over.");
        }

        static void DisplayStats(Pet pet)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Name: {pet.Name}\tType: {pet.Type}");
            Console.WriteLine($"Hunger: {pet.Hunger}/10\tHappiness: {pet.Happiness}/10\tHealth: {pet.Health}/10");
            if (pet.IsHungry)
                Console.WriteLine("WARNING : Your pet is hungry! Feed it.");
            if (pet.IsUnhappy)
                Console.WriteLine("WARNING : Your pet is unhappy! Play with it.");
            Console.WriteLine("-------------------------------");
        }
    }

    enum PetType
    {
        Cat,
        Dog,
        Rabbit
    }

    class Pet
    {
        public string Name { get; private set; }
        public PetType Type { get; private set; }
        public int Hunger { get; private set; }
        public int Happiness { get; private set; }
        public int Health { get; private set; }
        public bool IsAlive { get; private set; } = true;

        public bool IsHungry => Hunger > 7;
        public bool IsUnhappy => Happiness < 3;

        public void Create(string name, PetType type)
        {
            Name = name;
            Type = type;
            Hunger = 5;
            Happiness = 5;
            Health = 5;
        }

        public void Feed()
        {
            Console.WriteLine($"You feed {Name}.");
            Hunger = Math.Max(0, Hunger - 3);
            Happiness = Math.Min(10, Happiness + 1);
        }

        public void Play()
        {
            Console.WriteLine($"You play with {Name}.");
            Happiness = Math.Min(10, Happiness + 3);
            Hunger = Math.Min(10, Hunger + 1);
        }

        public void Rest()
        {
            Console.WriteLine($"You let {Name} rest.");
            Health = Math.Min(10, Health + 2);
            Happiness = Math.Max(0, Happiness - 1);
        }

        public void Update()
        {
            Hunger++;
            Happiness--;
            if (Hunger >= 10 || Happiness <= 0 || Health <= 0)
            {
                IsAlive = false;
            }
        }
    }
}