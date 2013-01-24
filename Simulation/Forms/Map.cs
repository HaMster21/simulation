using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Simulation.Forms
{
    public partial class Map : Form
    {
        private List<Town> towns;
        private Brush townBrush;
        private Brush producerBrush;
        private Brush carrierBrush;
        private Pen wayPen;

        public Map()
        {
            InitializeComponent();
            producerBrush = new SolidBrush(Color.Blue);
            townBrush = new SolidBrush(Color.Green);
            carrierBrush = new SolidBrush(Color.Red);
            wayPen = new Pen(Color.White);
            Graphics grafics = this.panel1.CreateGraphics();
            grafics.FillRectangle(townBrush, new Rectangle(50, 50, 50, 50));
        }

        private void createRessources()
        {
            RessourceManager.Instance().addRessource("Holz");
            RessourceManager.Instance().addRessource("Stein");
            RessourceManager.Instance().addRessource("Getreide");
            RessourceManager.Instance().addRessource("Fisch");
            RessourceManager.Instance().addRessource("Bretter");
            RessourceManager.Instance().addRessource("Wolle");
            RessourceManager.Instance().addRessource("Diamant");
            RessourceManager.Instance().addRessource("Humus");
            RessourceManager.Instance().addRessource("Bonbons");
            RessourceManager.Instance().addRessource("Waffen");
        }

        private void buildTowns()
        {
            Random random = new Random();
            for (int i = 0; i <= 10; i++)
            {
                PointF position = new PointF(random.Next(5, this.panel1.Width - 15), random.Next(5, this.panel1.Height - 15));
                Town townToBuild = new Town(position, random.Next(2, 10));
                towns.Add(townToBuild);
            }
        }

        private void Map_Load(object sender, EventArgs e)
        {
            towns = new List<Town>();
            buildTowns();
            createRessources();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            paintStaticContent(e);
        }

        private void paintStaticContent(PaintEventArgs e)
        {
            foreach (Town t in this.towns)
            {
                e.Graphics.FillRectangle(townBrush, t.Position.X, t.Position.Y, 8, 8);
                foreach (Producer producer in t.producers)
                {
                    e.Graphics.FillRectangle(producerBrush, producer.Position.X, producer.Position.Y, 5, 5);
                    e.Graphics.DrawLine(wayPen, producer.Position, t.Position);
                }
                foreach (Carrier carrier in t.carriers)
                {
                    e.Graphics.FillEllipse(carrierBrush, carrier.Position.X, carrier.Position.Y, 3, 3);
                }
            }
        }
    }
}
