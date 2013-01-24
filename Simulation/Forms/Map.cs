using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simulation.Forms
{
    public partial class Map : Form
    {
        private List<Town> towns;

        public Map()
        {
            InitializeComponent();
            buildTowns();
            createRessources();
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
            for (int i = 0; i <= 5; i++)
            {
                Random random = new Random();
                PointF position = new PointF(random.Next(5, this.panel1.Width - 5), random.Next(5, this.panel1.Height));
                Town townToBuild = new Town(position, random.Next(2, 10));
                towns.Add(townToBuild);
            }
        }

        private void Map_Load(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (Town t in this.towns)
            {
                e.Graphics.DrawRectangle(null, t.Position.X, t.Position.Y, 1, 1);
            }
        }
    }
}
