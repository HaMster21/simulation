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
        public bool IsHome { get; private set; }
        
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
            this.IsHome = true;
            timer = new System.Timers.Timer(100); // every 1/10th of a second
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(move);
        }

        public void setNewTarget(Producer target)
        {
            if (target.Position != this.Position)
            {
                this.CurrentTarget = target.Position;
                this.targetProducer = target;
                this.Running = true;
                this.IsHome = false;
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
                this.IsHome = false;
                timer.Start();
            }
        }

        private void move(object sender, System.Timers.ElapsedEventArgs e)
        {
            //get direction vector
            PointF directionVector = new PointF(this.CurrentTarget.X - this.Position.X, this.CurrentTarget.Y - this.Position.Y);
            this.Position = new PointF(this.Position.X + (directionVector.X / Speed), this.Position.Y + (directionVector.Y / Speed));
            if (this.Position == this.CurrentTarget)
            {
                timer.Stop();
                this.Running = false;
                this.CurrentTarget = this.Position;
                if ((int)this.Position.X == (int)this.targetProducer.Position.X && (int)this.Position.Y == (int)targetProducer.Position.Y)
                {
                    this.getRessourcesAndReturnHome();
                }
                else if (this.Position == this.homeTown.Position)
                {
                    this.stashRessources();
                    this.IsHome = true;
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
            this.targetProducer = null;
            this.setNewTarget(this.homeTown);
            this.Running = true;
            this.timer.Start();
        }
    }
}
