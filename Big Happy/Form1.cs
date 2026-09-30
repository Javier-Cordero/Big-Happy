using Big_Happy.Clientes;
using Big_Happy.Informacion;
using Big_Happy.Loguin;
using Big_Happy.Pedidos;
using Big_Happy.Productos;
using Big_Happy.Usuarios;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Big_Happy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Cerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Maximizar_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                Maximizar.IconChar = IconChar.Compress;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                Maximizar.IconChar = IconChar.Expand;
            }
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            ModalHelper.OpenChildForm(panel3, new FrmUsuarios());
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            ModalHelper.OpenChildForm(panel3, new FrmCliente());
        }
        private void iconButton3_Click(object sender, EventArgs e)
        {
            ModalHelper.OpenChildForm(panel3, new FrmProductos());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ModalHelper.OpenChildForm(panel3,new FrmPedidos());
        }
        private void iconButton5_Click(object sender, EventArgs e)
        {
            ModalHelper.OpenChildForm(panel3, new FrmInformaciones());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = $"Bienvenido, {Sesion.Nombre} ({Sesion.Rol})";
            Usuarios.Visible = Sesion.PuedeGestionarUsuarios;
            Clientes.Visible = Sesion.PuedeGestionarProductos;
            Productos.Visible = Sesion.PuedeVerInformacion;
            Tickets.Visible = Sesion.PuedeGestionarClientes;
            Informaciones.Visible = Sesion.PuedeEmitirTickets;
        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Cerrar sesión?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            Sesion.CerrarSesion();
            this.Close();
            if (Sesion.PedirLogin()) Application.Run(new Form1());
        }
    }
}
