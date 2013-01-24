using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Simulation
{
    struct Ressource
    {
        public int ID { get; private set; }
        public string Name { get; private set; }

        public Ressource(int ID, string name) : this()
        {
            this.ID = ID;
            this.Name = name;
        }

        public Ressource(int ID)
            : this()
        {
            this.ID = ID;
            this.Name = RessourceManager.Instance().getRessourceName(ID);
        }
    }
}
