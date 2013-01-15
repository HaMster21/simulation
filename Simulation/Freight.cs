using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulation
{
    struct Freight
    {
        private Dictionary<int, string> inventory;

        public void addToFreight(FreightPackage package)
        {
            //TODO: Rewrite to store items only once
            //TODO: Maybe it is more comfortable to use a hashmap here with itemID as hash and the 
            //amount contained
            //inventory.Add(package);
        }

        //TODO: Store item IDs in FreightPackages only once and add amounts to a FreightPackage 
        //that is unique for a given ressource. This enables for removing a FreightPackage with the
        //specified amount of a specified ressource
        public FreightPackage removeFreightPackage()
        {
            return new FreightPackage(new Ressource(1, "Holz"), 15);
        }
    }
}
