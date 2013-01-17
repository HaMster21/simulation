using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    static class RessourceManager
    {
        private static Dictionary<int, string> ressources;

        public static void addRessource(string name)
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

        public static int getRessourceID(string name)
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

        public static string getRessourceName(int ID)
        {
            if (ressources.ContainsKey(ID))
            {
                return ressources[ID];
            }
            else return null;
        }
    }
}
