namespace Caroto
{
    partial class StatusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Desconectar = new System.Windows.Forms.Button();
            this.actualizacionLbl = new System.Windows.Forms.Label();
            this.ultimaActualizacion = new System.Windows.Forms.TextBox();
            this.Actualizar = new System.Windows.Forms.Button();
            this.videosAlacenadosLbl = new System.Windows.Forms.Label();
            this.videosAlmacenados = new System.Windows.Forms.TextBox();
            this.Ver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Play = new System.Windows.Forms.Button();
            this.totalRerpoduccionLbl = new System.Windows.Forms.Label();
            this.tiempoTotal = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Desconectar
            // 
            this.Desconectar.Location = new System.Drawing.Point(129, 390);
            this.Desconectar.Name = "Desconectar";
            this.Desconectar.Size = new System.Drawing.Size(200, 25);
            this.Desconectar.TabIndex = 0;
            this.Desconectar.Text = "Desconectar";
            this.Desconectar.UseVisualStyleBackColor = true;
            this.Desconectar.Click += new System.EventHandler(this.Desconectar_Click);
            // 
            // actualizacionLbl
            // 
            this.actualizacionLbl.AutoSize = true;
            this.actualizacionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actualizacionLbl.Location = new System.Drawing.Point(13, 94);
            this.actualizacionLbl.Name = "actualizacionLbl";
            this.actualizacionLbl.Size = new System.Drawing.Size(149, 18);
            this.actualizacionLbl.TabIndex = 1;
            this.actualizacionLbl.Text = "Última actualizacion -";
            // 
            // ultimaActualizacion
            // 
            this.ultimaActualizacion.Location = new System.Drawing.Point(168, 95);
            this.ultimaActualizacion.Name = "ultimaActualizacion";
            this.ultimaActualizacion.ReadOnly = true;
            this.ultimaActualizacion.Size = new System.Drawing.Size(152, 20);
            this.ultimaActualizacion.TabIndex = 2;
            // 
            // Actualizar
            // 
            this.Actualizar.Location = new System.Drawing.Point(343, 92);
            this.Actualizar.Name = "Actualizar";
            this.Actualizar.Size = new System.Drawing.Size(100, 25);
            this.Actualizar.TabIndex = 3;
            this.Actualizar.Text = "Actualizar";
            this.Actualizar.UseVisualStyleBackColor = true;
            // 
            // videosAlacenadosLbl
            // 
            this.videosAlacenadosLbl.AutoSize = true;
            this.videosAlacenadosLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.videosAlacenadosLbl.Location = new System.Drawing.Point(13, 149);
            this.videosAlacenadosLbl.Name = "videosAlacenadosLbl";
            this.videosAlacenadosLbl.Size = new System.Drawing.Size(215, 18);
            this.videosAlacenadosLbl.TabIndex = 4;
            this.videosAlacenadosLbl.Text = "Videos almacenados en disco -";
            // 
            // videosAlmacenados
            // 
            this.videosAlmacenados.Location = new System.Drawing.Point(235, 150);
            this.videosAlmacenados.Name = "videosAlmacenados";
            this.videosAlmacenados.ReadOnly = true;
            this.videosAlmacenados.Size = new System.Drawing.Size(85, 20);
            this.videosAlmacenados.TabIndex = 5;
            // 
            // Ver
            // 
            this.Ver.Location = new System.Drawing.Point(373, 147);
            this.Ver.Name = "Ver";
            this.Ver.Size = new System.Drawing.Size(40, 25);
            this.Ver.TabIndex = 6;
            this.Ver.Text = "...";
            this.Ver.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Proxima reproduccion -";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(182, 211);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(138, 20);
            this.textBox1.TabIndex = 8;
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(343, 208);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(100, 25);
            this.Play.TabIndex = 9;
            this.Play.Text = "Reproducir";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // totalRerpoduccionLbl
            // 
            this.totalRerpoduccionLbl.AutoSize = true;
            this.totalRerpoduccionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRerpoduccionLbl.Location = new System.Drawing.Point(13, 287);
            this.totalRerpoduccionLbl.Name = "totalRerpoduccionLbl";
            this.totalRerpoduccionLbl.Size = new System.Drawing.Size(257, 18);
            this.totalRerpoduccionLbl.TabIndex = 10;
            this.totalRerpoduccionLbl.Text = "Tiempo total de videos reproducidos -";
            // 
            // tiempoTotal
            // 
            this.tiempoTotal.Location = new System.Drawing.Point(276, 287);
            this.tiempoTotal.Name = "tiempoTotal";
            this.tiempoTotal.ReadOnly = true;
            this.tiempoTotal.Size = new System.Drawing.Size(137, 20);
            this.tiempoTotal.TabIndex = 11;
            // 
            // StatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 461);
            this.Controls.Add(this.tiempoTotal);
            this.Controls.Add(this.totalRerpoduccionLbl);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ver);
            this.Controls.Add(this.videosAlmacenados);
            this.Controls.Add(this.videosAlacenadosLbl);
            this.Controls.Add(this.Actualizar);
            this.Controls.Add(this.ultimaActualizacion);
            this.Controls.Add(this.actualizacionLbl);
            this.Controls.Add(this.Desconectar);
            this.MaximumSize = new System.Drawing.Size(475, 500);
            this.MinimumSize = new System.Drawing.Size(475, 500);
            this.Name = "StatusForm";
            this.Text = "Control de estado";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Desconectar;
        private System.Windows.Forms.Label actualizacionLbl;
        private System.Windows.Forms.TextBox ultimaActualizacion;
        private System.Windows.Forms.Button Actualizar;
        private System.Windows.Forms.Label videosAlacenadosLbl;
        private System.Windows.Forms.TextBox videosAlmacenados;
        private System.Windows.Forms.Button Ver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Label totalRerpoduccionLbl;
        private System.Windows.Forms.TextBox tiempoTotal;
    }
}