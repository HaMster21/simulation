using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Simulation
{
    class Carrier
    {
        public PointF Position { get; private set; }
        public int Speed { get; set; }

        public Carrier(int speed, PointF position)
        {
            this.Speed = speed;
            this.Position = position;
        }
    }
}
