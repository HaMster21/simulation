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
        public bool Running { get; private set; }
        public PointF CurrentTarget { get; private set; }
        
        private Producer targetProducer { get; set; }
        private Town homeTown { get; set; }
        private FreightPackage Freight { get; set; }

        private System.Timers.Timer timer;

        /// <summary>
        /// Creates a new Carrier
        /// </summary>
        /// <param name="speed">The speed of the Carrier, must be between 1 and 100</param>
        /// <param name="position">The start position of the Carrier</param>
        public Carrier(int speed, Town initialLocation)
        {
            this.Speed = speed;
            this.Position = initialLocation.Position;
            CurrentTarget = this.Position;
            this.Running = false;
            timer = new System.Timers.Timer(600); // 1/100 second
            timer.Elapsed += new System.Timers.ElapsedEventHandler(move);
        }

        public void setNewTarget(Producer target)
        {
            if (target.Position != this.Position)
            {
                this.CurrentTarget = target.Position;
                this.targetProducer = target;
                this.Running = true;
                timer.Start();
            }
        }

        public void setNewTarget(Town target)
        {
            if (target.Position != this.Position)
            {
                this.CurrentTarget = target.Position;
                this.targetProducer = null;
                this.Running = true;
                timer.Start();
            }
        }


        private void move(object sender, System.Timers.ElapsedEventArgs e)
        {
            float x = this.Position.X;
            float y = this.Position.Y;
            this.Position = new PointF(x + Speed / 100, y + Speed / 100);
            if (this.Position == this.CurrentTarget)
            {
                timer.Stop();
                this.Running = false;
                this.CurrentTarget = this.Position;
                if (this.Position == this.targetProducer.Position)
                {
                    this.getRessourcesAndReturnHome();
                }
                else if (this.Position == this.homeTown.Position)
                {
                    this.stashRessources();
                }
            }
        }

        private void stashRessources()
        {
            this.homeTown.addRessourceToStash(this.Freight);
            this.CurrentTarget = this.Position;
            this.targetProducer = null;
            this.Running = false;
        }

        private void getRessourcesAndReturnHome()
        {
            this.Freight = this.targetProducer.getRessources();
            this.CurrentTarget = this.homeTown.Position;
            this.targetProducer = null;
            this.Running = true;
            this.timer.Start();
        }
    }
}
