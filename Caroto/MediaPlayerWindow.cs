﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caroto
{
    public partial class MediaPlayerWindow : Form
    {
        public MediaPlayerWindow()
        {
            InitializeComponent();
            WindowsMediaPlayer.uiMode = "none";
        }

        public void ReproduccionManual()
        {
            Show();
        }
    }

}
