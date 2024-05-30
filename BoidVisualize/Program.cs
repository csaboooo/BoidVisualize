using BoidVisualize;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.WindowHeight = 40;
        Console.WindowWidth = 80;

        BoidAlgorithm boidAlgorithm = new BoidAlgorithm(numBoids:100 );
        boidAlgorithm.RunSimulation();
        Console.ReadKey();
    }
}