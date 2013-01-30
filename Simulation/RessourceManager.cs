using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Simulation
{
    class RessourceManager
    {
        private Dictionary<int, string> ressources;

        private static RessourceManager theOnlyInstance;
        private RessourceManager()
        {
            this.ressources = new Dictionary<int, string>();
            this.ressources.Clear();
        }

        public static RessourceManager Instance() {
            if (theOnlyInstance == null) {
                theOnlyInstance = new RessourceManager();
                return theOnlyInstance;
            }
            else {
                return theOnlyInstance;
            }
        }

        public void addRessource(string name)
        {
            if (this.ressources.Count == 0)
            {
                this.ressources.Add(1, name);
            }
            else
            {
                this.ressources.Add(ressources.Count + 1, name);
            }
        }

        public int getRessourceID(string name)
        {
            if (this.ressources.ContainsValue(name))
            {
                foreach (var pair in ressources)
                {
                    if (pair.Value == name)
                    {
                        return pair.Key;
                    }
                }
                return -1;
            }
            else return -1;
        }

        public string getRessourceName(int ID)
        {
            if (this.ressources.ContainsKey(ID))
            {
                return this.ressources[ID];
            }
            else return "";
        }

        public int ressourceCount()
        {
            return this.ressources.Count;
        }
    }
}
