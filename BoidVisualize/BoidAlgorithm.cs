using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoidVisualize
{
    internal class BoidAlgorithm
    {
        private List<Boid> boids;
        public BoidAlgorithm(int numBoids)
        {
            Random random = new Random();
            boids = new List<Boid>();

            for (int i = 0; i < numBoids; i++)
            {
                Boid boid = new Boid
                {
                    X = random.Next(0, Console.WindowWidth),
                    Y = random.Next(0, Console.WindowHeight),
                    VelocityX = (float)(random.NextDouble() * 2 - 1),
                    VelocityY = (float)(random.NextDouble() * 2 - 1)
                };

                boids.Add(boid);
            }
        }
        private void UpdateBoids()
        {
            foreach (Boid boid in boids)
            {
                boid.X += boid.VelocityX;
                boid.Y += boid.VelocityY;

                ApplySeparation(boid);
                ApplyAlignment(boid);
                ApplyCohesion(boid);

                boid.X = (boid.X + Console.WindowWidth) % Console.WindowWidth;
                boid.Y = (boid.Y + Console.WindowHeight) % Console.WindowHeight;
            }
        }
        private void DrawBoids()
        {
            Console.Clear();

            foreach (Boid boid in boids)
            {
                Console.SetCursorPosition((int)boid.X, (int)boid.Y);
                Console.Write("X");
            }
        }

        public void RunSimulation()
        {
            while (true)
            {
                UpdateBoids();
                DrawBoids();
                Thread.Sleep(20); 
            }
        }

        private void ApplySeparation(Boid currentBoid)
        {
            float separationRadius = 5f;
            float separationStrength = 0.1f;

            foreach (Boid otherBoid in boids)
            {
                if (otherBoid != currentBoid)
                {
                    float distance = Distance(currentBoid, otherBoid);

                    if (distance < separationRadius)
                    {
                        float moveAwayX = currentBoid.X - otherBoid.X;
                        float moveAwayY = currentBoid.Y - otherBoid.Y;

                        currentBoid.VelocityX += moveAwayX * separationStrength;
                        currentBoid.VelocityY += moveAwayY * separationStrength;
                    }
                }
            }
        }

        private void ApplyAlignment(Boid currentBoid)
        {
            float alignmentRadius = 10f;
            float alignmentStrength = 0.02f;

            int count = 0;
            float averageVelocityX = 0;
            float averageVelocityY = 0;

            foreach (Boid otherBoid in boids)
            {
                if (otherBoid != currentBoid)
                {
                    float distance = Distance(currentBoid, otherBoid);

                    if (distance < alignmentRadius)
                    {
                        averageVelocityX += otherBoid.VelocityX;
                        averageVelocityY += otherBoid.VelocityY;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                averageVelocityX /= count;
                averageVelocityY /= count;

                currentBoid.VelocityX += (averageVelocityX - currentBoid.VelocityX) * alignmentStrength;
                currentBoid.VelocityY += (averageVelocityY - currentBoid.VelocityY) * alignmentStrength;
            }
        }

        private void ApplyCohesion(Boid currentBoid)
        {
            float cohesionRadius = 15f;
            float cohesionStrength = 0.02f;

            int count = 0;
            float averageX = 0;
            float averageY = 0;

            foreach (Boid otherBoid in boids)
            {
                if (otherBoid != currentBoid)
                {
                    float distance = Distance(currentBoid, otherBoid);

                    if (distance < cohesionRadius)
                    {
                        averageX += otherBoid.X;
                        averageY += otherBoid.Y;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                averageX /= count;
                averageY /= count;

                float moveTowardsX = averageX - currentBoid.X;
                float moveTowardsY = averageY - currentBoid.Y;

                currentBoid.VelocityX += moveTowardsX * cohesionStrength;
                currentBoid.VelocityY += moveTowardsY * cohesionStrength;
            }
        }

        private float Distance(Boid boid1, Boid boid2)
        {
            float dx = boid1.X - boid2.X;
            float dy = boid1.Y - boid2.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
    }

}

