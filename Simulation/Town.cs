using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Simulation
{
    class Town
    {
        public PointF Position { get; private set; }

        private Dictionary<Ressource,int> stash;
        private List<Producer> producers;
        private List<Carrier> carriers;

        public Town(PointF position, int producerCount)
        {
            this.producers = new List<Producer>();
            this.stash = new Dictionary<Ressource, int>();
            this.carriers = new List<Carrier>();
            this.producers.Clear();
            this.stash.Clear();
            this.carriers.Clear();

            this.Position = position;
            Random random = new Random();
            for (int i = 0; i <= producerCount; i++)
            {
                int ressourceID = random.Next(RessourceManager.Instance().ressourceCount());
                Ressource ressource = new Ressource(ressourceID);
                PointF producerPosition = new PointF(random.Next(10)+this.Position.X,random.Next(10)+this.Position.Y);
                Producer newProducer = new Producer(ressource, 60000, random.Next(10), random.Next(150), producerPosition);
                newProducer.newProducts += new System.Action(sendCarrier);
                this.producers.Add(newProducer);
            }

            int carrierCount = producerCount / 2;
            for (int j = 0; j <= carrierCount; j++)
            {
                Carrier newCarrier = new Carrier(10, this.Position);
                this.carriers.Add(newCarrier);
            }
        }

        private void sendCarrier()
        {
            throw new Exception("The method or operation is not implemented.");
        }

    }
}
