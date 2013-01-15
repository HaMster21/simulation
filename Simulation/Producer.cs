using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Simulation
{
    class Producer
    {
        public event System.Action newProducts;

        public Ressource ProducedRessource { get; private set; }
        public int Interval { get; private set; }
        public int Weight { get; private set; }
        public int Store { get; private set; }
        public int maxStore { get; private set; }
        public PointF Position { get; private set; }

        Producer(Ressource product, int interval, int weight, int storeLimit, PointF position)
        {
            this.ProducedRessource = product;
            this.Interval = interval;
            this.Weight = weight;
            this.Store = 0;
            this.maxStore = storeLimit;
            this.Position = position;
        }

        public void StartProduction()
        {
            System.Threading.Timer timer = new System.Threading.Timer(new System.Threading.TimerCallback(addAmount), Weight, 0, Interval);
        }

        private void addAmount(object state)
        {
            int amountToAdd = (int)state;
            if ((Store == maxStore) || (Store + amountToAdd > maxStore))
            {
                // if adding more than permitted or store is full, fill the store to max instead
                addAmount(maxStore - Store);
            }
            else
            {
                Store += amountToAdd;
            }
        }
    }
}
