
interface IAnimal
{
    void Eat();
    void Sleep();
}

interface IMovable:IAnimal
{
    void Walk();
    void Run();
}

interface ITrainable:IMovable
{
    void Train();
}

class Dog : ITrainable 
{
    public void Eat()
    {
        Console.WriteLine("Dog is eating.");
    }

    public void Sleep()
    {
        Console.WriteLine("Dog is sleeping.");
    }

    public void Walk()
    {
        Console.WriteLine("Dog is walking.");
    }

    public void Run()
    {
        Console.WriteLine("Dog is running.");
    }

    public void Train()
    {
        Console.WriteLine("Dog is being trained.");
    }
}

class Cat : IMovable
{
    public void Eat()
    {
        Console.WriteLine("Cat is eating.");
    }

    public void Sleep()
    {
        Console.WriteLine("Cat is sleeping.");
    }

    public void Walk()
    {
        Console.WriteLine("Cat is walking.");
    }

    public void Run()
    {
        Console.WriteLine("Cat is running.");
    }
}

class Program
{
    static void Main()
    {
        List<IAnimal> animals = new List<IAnimal>(); 

        animals.Add(new Dog());
        animals.Add(new Cat());

        for (int i = 0; i < animals.Count; i++)
        {
            IAnimal animal = animals[i];
            animal.Eat();
            animal.Sleep();

            if (animal is IMovable movable)
            {
                movable.Walk();
                movable.Run();
            }

            if (animal is ITrainable trainable)
            {
                trainable.Train();
            }

            Console.WriteLine();
        }
    }
}
