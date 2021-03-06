﻿using System;
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

        private int numberOfTowns;
        private int averageProducerCount;

        public Map(int townCount, int averageProducerToBuiltCount)
        {
            InitializeComponent();
            producerBrush = new SolidBrush(Color.Blue);
            townBrush = new SolidBrush(Color.Green);
            carrierBrush = new SolidBrush(Color.Red);
            wayPen = new Pen(Color.White);
            this.numberOfTowns = townCount;
            this.averageProducerCount = averageProducerToBuiltCount;
        }

        #region Event reactions
        private void createRessources()
        {
            RessourceManager.Instance().addRessource( "Holz" );
            RessourceManager.Instance().addRessource( "Stein" );
            RessourceManager.Instance().addRessource( "Getreide" );
            RessourceManager.Instance().addRessource( "Fisch" );
            RessourceManager.Instance().addRessource( "Bretter" );
            RessourceManager.Instance().addRessource( "Wolle" );
            RessourceManager.Instance().addRessource( "Diamant" );
            RessourceManager.Instance().addRessource( "Humus" );
            RessourceManager.Instance().addRessource( "Bonbons" );
            RessourceManager.Instance().addRessource( "Waffen" );
        }

        private void buildTowns()
        {
            Random random = new Random();
            for ( int i = 0; i <= numberOfTowns - 1; i++ )
            {
                PointF position = new PointF( random.Next( 5, this.panel1.Width - 15 ), random.Next( 5, this.panel1.Height - 15 ) );
                Town townToBuild = new Town( position, random.Next( averageProducerCount - averageProducerCount / 3, averageProducerCount + averageProducerCount / 3 ) );
                towns.Add( townToBuild );
            }
        }

        private void Map_Load( object sender, EventArgs e )
        {
            createRessources();
            towns = new List<Town>();
            buildTowns();
            this.animationTimer.Start();
        }

        private void panel1_Paint( object sender, PaintEventArgs e )
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach ( Town t in this.towns )
            {
                t.Repaint( e );
            }
        }
        #endregion

        private void redrawPanel(object sender, EventArgs e)
        {
            this.panel1.Refresh();
        }
    }
}
