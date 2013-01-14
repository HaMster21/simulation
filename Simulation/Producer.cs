using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    class Producer
    {
        public event System.Action newProducts;

        public Ressource ProducedRessource { get; private set; }
        public int Interval { get; private set; }
        public int Weight { get; private set; }
        public int Store { get; private set; }

        Producer(Ressource product, int interval, int weight)
        {
            this.ProducedRessource = product;
            this.Interval = interval;
            this.Weight = weight;
            this.Store = 0;
        }

        public void StartProduction()
        {
            System.Threading.Timer timer = new System.Threading.Timer(new System.Threading.TimerCallback(addAmount), Weight, 0, Interval);
        }

        private void addAmount(object state)
        {
            this.Store += (int)state;
        }
    }
}
