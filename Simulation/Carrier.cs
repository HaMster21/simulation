﻿using System.Drawing;

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

        PointF directionVector;

        private Pen carrierPen = new Pen( Color.Blue );

        private System.Timers.Timer timer;

        /// <summary>
        /// Creates a new Carrier
        /// </summary>
        /// <param name="speed">The speed of the Carrier, must be between 1 and 100</param>
        /// <param name="position">The start position of the Carrier</param>
        public Carrier( int speed, Town initialLocation )
        {
            this.Speed = speed;
            this.Position = initialLocation.Position;
            this.homeTown = initialLocation;
            CurrentTarget = this.Position;
            this.Running = false;
            this.IsHome = true;
            timer = new System.Timers.Timer( 100 ); // every 1/10th of a second
            timer.Elapsed += new System.Timers.ElapsedEventHandler( move );
        }

        public void setNewTarget( Producer target )
        {
            this.CurrentTarget = target.Position;
            this.targetProducer = target;
            this.Running = true;
            this.IsHome = false;
            directionVector = new PointF( this.CurrentTarget.X - this.Position.X, this.CurrentTarget.Y - this.Position.Y );
            timer.Start();
        }

        public void setNewTarget( Town target )
        {
            this.CurrentTarget = target.Position;
            this.targetProducer = null;
            this.Running = true;
            this.IsHome = false;
            directionVector = new PointF( this.CurrentTarget.X - this.Position.X, this.CurrentTarget.Y - this.Position.Y );
            timer.Start();
        }

        private void move( object sender, System.Timers.ElapsedEventArgs e )
        {

            moveAlongDirectionVector();

            if (
                ((int)this.Position.X <= (int)this.CurrentTarget.X + 1 && (int)this.Position.X >= (int)this.CurrentTarget.X - 1) &&
                ((int)this.Position.Y <= (int)this.CurrentTarget.Y + 1 && (int)this.Position.Y >= (int)this.CurrentTarget.Y - 1)
               )
            {
                //we reached the target
                timer.Stop();
                this.Running = false;

                if ( !(this.targetProducer == null) &&
                    ((int)this.Position.X <= (int)this.targetProducer.Position.X + 1 && (int)this.Position.X >= (int)this.targetProducer.Position.X - 1) &&
                    ((int)this.Position.Y <= (int)this.targetProducer.Position.Y + 1 && (int)this.Position.Y >= (int)this.targetProducer.Position.Y - 1)
                   )
                {
                    this.getRessourcesAndReturnHome();
                }
                else
                {
                    this.stashRessources();
                    this.IsHome = true;
                }
            }
        }

        private void moveAlongDirectionVector()
        {
            this.Position = new PointF( this.Position.X + (directionVector.X / (Speed * 10)), this.Position.Y + (directionVector.Y / (Speed * 10)) );
        }

        private void stashRessources()
        {
            this.homeTown.addRessourceToStash( this.Freight );
            this.CurrentTarget = this.Position;
            this.targetProducer = null;
            this.Running = false;
        }

        private void getRessourcesAndReturnHome()
        {
            this.Freight = this.targetProducer.getRessources();
            this.targetProducer = null;
            this.setNewTarget( this.homeTown );
            this.Running = true;
            this.timer.Start();
        }

        internal void Repaint( System.Windows.Forms.PaintEventArgs a )
        {
            a.Graphics.DrawEllipse( carrierPen, this.Position.X, this.Position.Y, 4f, 3f );
        }
    }
}
