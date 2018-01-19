namespace Caroto
{
    partial class InitialForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialForm));
            this.mainText = new System.Windows.Forms.Label();
            this.clienteIdLbl = new System.Windows.Forms.Label();
            this.identificadorDePantallaLbl = new System.Windows.Forms.Label();
            this.apiKey = new System.Windows.Forms.TextBox();
            this.identidad = new System.Windows.Forms.TextBox();
            this.Iniciar = new System.Windows.Forms.Button();
            this.Cancelar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainText
            // 
            this.mainText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainText.AutoSize = true;
            this.mainText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainText.Location = new System.Drawing.Point(12, 171);
            this.mainText.Name = "mainText";
            this.mainText.Size = new System.Drawing.Size(688, 60);
            this.mainText.TabIndex = 0;
            this.mainText.Text = resources.GetString("mainText.Text");
            this.mainText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // clienteIdLbl
            // 
            this.clienteIdLbl.AutoSize = true;
            this.clienteIdLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clienteIdLbl.Location = new System.Drawing.Point(85, 275);
            this.clienteIdLbl.Name = "clienteIdLbl";
            this.clienteIdLbl.Size = new System.Drawing.Size(120, 16);
            this.clienteIdLbl.TabIndex = 2;
            this.clienteIdLbl.Text = "Numero de cliente:";
            // 
            // identificadorDePantallaLbl
            // 
            this.identificadorDePantallaLbl.AutoSize = true;
            this.identificadorDePantallaLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.identificadorDePantallaLbl.Location = new System.Drawing.Point(51, 316);
            this.identificadorDePantallaLbl.Name = "identificadorDePantallaLbl";
            this.identificadorDePantallaLbl.Size = new System.Drawing.Size(154, 16);
            this.identificadorDePantallaLbl.TabIndex = 3;
            this.identificadorDePantallaLbl.Text = "Identificador de pantalla:";
            // 
            // apiKey
            // 
            this.apiKey.Location = new System.Drawing.Point(222, 271);
            this.apiKey.Name = "apiKey";
            this.apiKey.Size = new System.Drawing.Size(415, 20);
            this.apiKey.TabIndex = 4;
            // 
            // identidad
            // 
            this.identidad.Location = new System.Drawing.Point(222, 312);
            this.identidad.Name = "identidad";
            this.identidad.Size = new System.Drawing.Size(415, 20);
            this.identidad.TabIndex = 5;
            // 
            // Iniciar
            // 
            this.Iniciar.Location = new System.Drawing.Point(439, 384);
            this.Iniciar.Name = "Iniciar";
            this.Iniciar.Size = new System.Drawing.Size(100, 25);
            this.Iniciar.TabIndex = 6;
            this.Iniciar.Text = "Iniciar";
            this.Iniciar.UseVisualStyleBackColor = true;
            this.Iniciar.Click += new System.EventHandler(this.Iniciar_Click);
            // 
            // Cancelar
            // 
            this.Cancelar.Location = new System.Drawing.Point(545, 384);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(100, 25);
            this.Cancelar.TabIndex = 7;
            this.Cancelar.Text = "Cancelar";
            this.Cancelar.UseVisualStyleBackColor = true;
            this.Cancelar.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(120, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(494, 83);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // InitialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 421);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.Iniciar);
            this.Controls.Add(this.identidad);
            this.Controls.Add(this.apiKey);
            this.Controls.Add(this.identificadorDePantallaLbl);
            this.Controls.Add(this.clienteIdLbl);
            this.Controls.Add(this.mainText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(730, 460);
            this.MinimumSize = new System.Drawing.Size(730, 460);
            this.Name = "InitialForm";
            this.Text = "CommunicTv Video Player";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mainText;
        private System.Windows.Forms.Label clienteIdLbl;
        private System.Windows.Forms.Label identificadorDePantallaLbl;
        private System.Windows.Forms.TextBox apiKey;
        private System.Windows.Forms.TextBox identidad;
        private System.Windows.Forms.Button Iniciar;
        private System.Windows.Forms.Button Cancelar;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

