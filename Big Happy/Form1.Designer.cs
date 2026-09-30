namespace Big_Happy
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.Informaciones = new FontAwesome.Sharp.IconButton();
            this.Tickets = new FontAwesome.Sharp.IconButton();
            this.Productos = new FontAwesome.Sharp.IconButton();
            this.Clientes = new FontAwesome.Sharp.IconButton();
            this.Usuarios = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Minimizar = new FontAwesome.Sharp.IconPictureBox();
            this.Maximizar = new FontAwesome.Sharp.IconPictureBox();
            this.Cerrar = new FontAwesome.Sharp.IconPictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Minimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Maximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panel1.Controls.Add(this.iconButton1);
            this.panel1.Controls.Add(this.Informaciones);
            this.panel1.Controls.Add(this.Tickets);
            this.panel1.Controls.Add(this.Productos);
            this.panel1.Controls.Add(this.Clientes);
            this.panel1.Controls.Add(this.Usuarios);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(152, 700);
            this.panel1.TabIndex = 3;
            // 
            // iconButton1
            // 
            this.iconButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.UserLock;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 30;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(1, 644);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(150, 56);
            this.iconButton1.TabIndex = 5;
            this.iconButton1.Text = "CERRAR SESION";
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = true;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click_1);
            // 
            // Informaciones
            // 
            this.Informaciones.FlatAppearance.BorderSize = 0;
            this.Informaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Informaciones.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Informaciones.ForeColor = System.Drawing.Color.White;
            this.Informaciones.IconChar = FontAwesome.Sharp.IconChar.Readme;
            this.Informaciones.IconColor = System.Drawing.Color.White;
            this.Informaciones.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Informaciones.IconSize = 30;
            this.Informaciones.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Informaciones.Location = new System.Drawing.Point(1, 300);
            this.Informaciones.Name = "Informaciones";
            this.Informaciones.Size = new System.Drawing.Size(150, 50);
            this.Informaciones.TabIndex = 4;
            this.Informaciones.Text = "INFORMACION";
            this.Informaciones.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Informaciones.UseVisualStyleBackColor = true;
            this.Informaciones.Click += new System.EventHandler(this.iconButton5_Click);
            // 
            // Tickets
            // 
            this.Tickets.FlatAppearance.BorderSize = 0;
            this.Tickets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Tickets.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tickets.ForeColor = System.Drawing.Color.White;
            this.Tickets.IconChar = FontAwesome.Sharp.IconChar.Ticket;
            this.Tickets.IconColor = System.Drawing.Color.White;
            this.Tickets.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Tickets.IconSize = 30;
            this.Tickets.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Tickets.Location = new System.Drawing.Point(1, 250);
            this.Tickets.Name = "Tickets";
            this.Tickets.Size = new System.Drawing.Size(150, 50);
            this.Tickets.TabIndex = 3;
            this.Tickets.Text = "TICKET";
            this.Tickets.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Tickets.UseVisualStyleBackColor = true;
            this.Tickets.Click += new System.EventHandler(this.iconButton4_Click);
            // 
            // Productos
            // 
            this.Productos.FlatAppearance.BorderSize = 0;
            this.Productos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Productos.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Productos.ForeColor = System.Drawing.Color.White;
            this.Productos.IconChar = FontAwesome.Sharp.IconChar.Burger;
            this.Productos.IconColor = System.Drawing.Color.White;
            this.Productos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Productos.IconSize = 30;
            this.Productos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Productos.Location = new System.Drawing.Point(1, 200);
            this.Productos.Name = "Productos";
            this.Productos.Size = new System.Drawing.Size(150, 50);
            this.Productos.TabIndex = 2;
            this.Productos.Text = "PRODUCTOS";
            this.Productos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Productos.UseVisualStyleBackColor = true;
            this.Productos.Click += new System.EventHandler(this.iconButton3_Click);
            // 
            // Clientes
            // 
            this.Clientes.FlatAppearance.BorderSize = 0;
            this.Clientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Clientes.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clientes.ForeColor = System.Drawing.Color.White;
            this.Clientes.IconChar = FontAwesome.Sharp.IconChar.PersonChalkboard;
            this.Clientes.IconColor = System.Drawing.Color.White;
            this.Clientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Clientes.IconSize = 30;
            this.Clientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Clientes.Location = new System.Drawing.Point(1, 150);
            this.Clientes.Name = "Clientes";
            this.Clientes.Size = new System.Drawing.Size(150, 50);
            this.Clientes.TabIndex = 1;
            this.Clientes.Text = "CLIENTES";
            this.Clientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Clientes.UseVisualStyleBackColor = true;
            this.Clientes.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // Usuarios
            // 
            this.Usuarios.FlatAppearance.BorderSize = 0;
            this.Usuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Usuarios.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Usuarios.ForeColor = System.Drawing.Color.White;
            this.Usuarios.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.Usuarios.IconColor = System.Drawing.Color.White;
            this.Usuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Usuarios.IconSize = 30;
            this.Usuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Usuarios.Location = new System.Drawing.Point(1, 100);
            this.Usuarios.Name = "Usuarios";
            this.Usuarios.Size = new System.Drawing.Size(150, 50);
            this.Usuarios.TabIndex = 0;
            this.Usuarios.Text = "USUARIOS";
            this.Usuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Usuarios.UseVisualStyleBackColor = true;
            this.Usuarios.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.Minimizar);
            this.panel2.Controls.Add(this.Maximizar);
            this.panel2.Controls.Add(this.Cerrar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(152, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(848, 30);
            this.panel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Perpetua", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 22);
            this.label1.TabIndex = 9;
            this.label1.Text = "Usuario";
            // 
            // Minimizar
            // 
            this.Minimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Minimizar.BackColor = System.Drawing.Color.Transparent;
            this.Minimizar.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.Minimizar.IconColor = System.Drawing.Color.White;
            this.Minimizar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Minimizar.IconSize = 24;
            this.Minimizar.Location = new System.Drawing.Point(776, 1);
            this.Minimizar.Name = "Minimizar";
            this.Minimizar.Size = new System.Drawing.Size(24, 24);
            this.Minimizar.TabIndex = 8;
            this.Minimizar.TabStop = false;
            this.Minimizar.Click += new System.EventHandler(this.Minimizar_Click);
            // 
            // Maximizar
            // 
            this.Maximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Maximizar.BackColor = System.Drawing.Color.Transparent;
            this.Maximizar.IconChar = FontAwesome.Sharp.IconChar.Expand;
            this.Maximizar.IconColor = System.Drawing.Color.White;
            this.Maximizar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Maximizar.IconSize = 24;
            this.Maximizar.Location = new System.Drawing.Point(800, 1);
            this.Maximizar.Name = "Maximizar";
            this.Maximizar.Size = new System.Drawing.Size(24, 24);
            this.Maximizar.TabIndex = 7;
            this.Maximizar.TabStop = false;
            this.Maximizar.Click += new System.EventHandler(this.Maximizar_Click);
            // 
            // Cerrar
            // 
            this.Cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cerrar.BackColor = System.Drawing.Color.Transparent;
            this.Cerrar.IconChar = FontAwesome.Sharp.IconChar.X;
            this.Cerrar.IconColor = System.Drawing.Color.White;
            this.Cerrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Cerrar.IconSize = 24;
            this.Cerrar.Location = new System.Drawing.Point(824, 1);
            this.Cerrar.Name = "Cerrar";
            this.Cerrar.Size = new System.Drawing.Size(24, 24);
            this.Cerrar.TabIndex = 6;
            this.Cerrar.TabStop = false;
            this.Cerrar.Click += new System.EventHandler(this.Cerrar_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(152, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(848, 670);
            this.panel3.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Minimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Maximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cerrar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton Usuarios;
        private FontAwesome.Sharp.IconPictureBox Minimizar;
        private FontAwesome.Sharp.IconPictureBox Maximizar;
        private FontAwesome.Sharp.IconPictureBox Cerrar;
        private FontAwesome.Sharp.IconButton Clientes;
        private FontAwesome.Sharp.IconButton Informaciones;
        private FontAwesome.Sharp.IconButton Tickets;
        private FontAwesome.Sharp.IconButton Productos;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton iconButton1;
    }
}

