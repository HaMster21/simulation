using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    class RessourceManager
    {
        private Dictionary<int, string> ressources;

        private static RessourceManager theOnlyInstance;

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
            if (ressources.Count == 0)
            {
                ressources.Add(1, name);
            }
            else
            {
                ressources.Add(ressources.Count + 1, name);
            }
        }

        public int getRessourceID(string name)
        {
            if (ressources.ContainsValue(name))
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
            if (ressources.ContainsKey(ID))
            {
                return ressources[ID];
            }
            else return null;
        }

        public int ressourceCount()
        {
            return ressources.Count;
        }
    }
}
