using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation
{
    struct FreightPackage
    {
        public Ressource ContainedRessource { get; private set; }
        public int Amount { get; private set; }

        public FreightPackage(Ressource ressource, int amount)
            : this()
        {
            ContainedRessource = ressource;
            Amount = amount;
        }
    }
}