﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Test
{
    public partial class FormMain : Form
    {
        FormImageWindow imageWindow;
        ToolBox toolBox;
        ProcessBar processBar;

        //private DeserializeDockContent m_deserializeDockContent;

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            toolStripButton5.Image = toolStripMenuItem4.Image;
            toolStripButton5.Text = toolStripMenuItem4.Text;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            toolStripButton5.Image = toolStripMenuItem5.Image;
            toolStripButton5.Text = toolStripMenuItem5.Text;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            toolStripButton5.Image = toolStripMenuItem6.Image;
            toolStripButton5.Text = toolStripMenuItem6.Text;
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            dockPanel1.Theme = new VS2015BlueTheme();


            imageWindow = new FormImageWindow();
            imageWindow.Show(dockPanel1);

            processBar = new ProcessBar();
            processBar.Show(dockPanel1, DockState.DockRight);

            toolBox = new ToolBox();
            toolBox.Show(processBar.Pane, DockAlignment.Left, 0.5);


            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (File.Exists(configFile))
            {
                //dockPanel1.LoadFromXml(configFile, m_deserializeDockContent);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            dockPanel1.SaveAsXml(configFile);
        }
    }
}