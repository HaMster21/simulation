using System;
using System.Collections.Generic;
using System.Drawing;

namespace Simulation
{
    public delegate void PaintCallBack( System.Windows.Forms.PaintEventArgs a );

    class Town
    {
        private event PaintCallBack repaint;

        public PointF Position { get; private set; }
        public List<Producer> producers;
        public List<Carrier> carriers;

        private List<Ressource> stash;
        private Brush townbrush = new SolidBrush( Color.Green );
        private Pen wayPen = new Pen( Color.White );

        public Town( PointF position, int producerCount )
        {
            this.producers = new List<Producer>();
            this.stash = new List<Ressource>();
            this.carriers = new List<Carrier>();
            this.producers.Clear();
            this.stash.Clear();
            this.carriers.Clear();

            this.Position = position;

            createProducers( producerCount );
            createCarriers( producerCount );

            foreach ( Producer prod in producers )
            {
                prod.StartProduction();
            }
        }

        private void createCarriers( int producerCount )
        {
            int carrierCount = producerCount / 2;
            for ( int j = 0; j <= carrierCount; j++ )
            {
                Random random = new Random();
                Carrier newCarrier = new Carrier( random.Next( 2, 10 ), this );
                this.repaint += newCarrier.Repaint;
                this.carriers.Add( newCarrier );
            }
        }

        private void createProducers( int producerCount )
        {
            Random random = new Random();
            for ( int i = 0; i <= producerCount; i++ )
            {
                int ressourceID = random.Next( RessourceManager.Instance().ressourceCount() );
                Ressource ressource = new Ressource( ressourceID );
                PointF producerPosition = new PointF( random.Next( -50, 50 ) + this.Position.X, random.Next( -50, 50 ) + this.Position.Y );
                
                Producer newProducer = new Producer( ressource, random.Next( 10000, 30000 ), random.Next( 1, 10 ), random.Next( 50, 150 ), producerPosition );
                newProducer.newProducts += this.sendCarrier;
                this.repaint += newProducer.Repaint;

                this.producers.Add( newProducer );
            }
        }

        public void addRessourceToStash( FreightPackage freight )
        {
            this.stash.Add( freight.ContainedRessource );
        }

        private void sendCarrier( Producer producer )
        {
            foreach ( Carrier carrier in this.carriers )
            {
                if ( carrier.IsHome )
                {
                    carrier.setNewTarget( producer );
                    break;
                }
            }
        }

        public void Repaint( System.Windows.Forms.PaintEventArgs a )
        {
            a.Graphics.FillRectangle( townbrush, this.Position.X, this.Position.Y, 8f, 8f );
            foreach ( Producer p in this.producers )
            {
                a.Graphics.DrawLine( new Pen( Color.White ), this.Position, p.Position );
            }
            this.repaint( a );
        }
    }
}
