using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Simulation
{
    class Town
    {
        public List<Producer> producers;
        public PointF Position { get; private set; }

        public Town(PointF position, int producerCount)
        {
            this.producers.Clear();
            this.Position = position;
            Random random = new Random();
            for (int i = 0; i <= producerCount; i++)
            {
                int ressourceID = random.Next(RessourceManager.Instance().ressourceCount());
                Ressource ressource = new Ressource(ressourceID);
                PointF producerPosition = new PointF(random.Next(10)+this.Position.X,random.Next(10)+this.Position.Y);
                this.producers.Add(new Producer(ressource, 60000, random.Next(10), random.Next(150), producerPosition));
            }
        }
    }
}
