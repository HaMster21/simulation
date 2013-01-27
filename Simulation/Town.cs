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
        public List<Producer> producers;
        public List<Carrier> carriers;

        private Dictionary<Ressource, int> stash;

        public Town(PointF position, int producerCount)
        {
            this.producers = new List<Producer>();
            this.stash = new Dictionary<Ressource, int>();
            this.carriers = new List<Carrier>();
            this.producers.Clear();
            this.stash.Clear();
            this.carriers.Clear();

            this.Position = position;

            createProducers(producerCount);
            createCarriers(producerCount);

            foreach (Producer prod in producers)
            {
                prod.StartProduction();
            }
        }

        private void createCarriers(int producerCount)
        {
            int carrierCount = producerCount / 2;
            for (int j = 0; j <= carrierCount; j++)
            {
                Carrier newCarrier = new Carrier(10, this);
                this.carriers.Add(newCarrier);
            }
        }

        private void createProducers(int producerCount)
        {
            Random random = new Random();
            for (int i = 0; i <= producerCount; i++)
            {
                int ressourceID = random.Next(RessourceManager.Instance().ressourceCount());
                Ressource ressource = new Ressource(ressourceID);
                PointF producerPosition = new PointF(random.Next(-50, 50) + this.Position.X, random.Next(-50, 50) + this.Position.Y);
                Producer newProducer = new Producer(ressource, random.Next(6000,30000), random.Next(1, 10), random.Next(50, 150), producerPosition);
                newProducer.newProducts += new ProducerCallback(sendCarrier);
                this.producers.Add(newProducer);
            }
        }

        public void addRessourceToStash(FreightPackage freight)
        {
            this.stash.Add(freight.ContainedRessource, freight.Amount);
        }

        private void sendCarrier(Producer producer)
        {
            foreach (Carrier carrier in this.carriers)
            {
                if (carrier.IsHome)
                {
                    carrier.setNewTarget(producer);
                    break;
                }
            }
        }

    }
}
