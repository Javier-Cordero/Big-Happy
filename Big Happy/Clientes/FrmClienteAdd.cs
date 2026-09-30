using Big_Happy.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Big_Happy.Clientes
{
    public partial class FrmClienteAdd : Form
    {
        public FrmClienteAdd()
        {
            InitializeComponent();
        }
        private bool esEdicion = false;
        private int indiceEdicion = -1;
        public FrmClienteAdd(int indice) : this()
        {
            esEdicion = true;
            indiceEdicion = indice;
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Complete los campos obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textBox2.Text, out int telefono))
            {
                MessageBox.Show("El teléfono debe ser numérico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (esEdicion)
            {
                DatosClientes.Editar(indiceEdicion, textBox1.Text, telefono, textBox3.Text, textBox4.Text);
                MessageBox.Show("Usuario actualizado correctamente.");
            }
            else
            {
                DatosClientes.Guardar(textBox1.Text, telefono, textBox3.Text, textBox4.Text);
                MessageBox.Show("Usuario guardado correctamente.");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void FrmClienteAdd_Load(object sender, EventArgs e)
        {
            if (esEdicion)
            {
                this.Text = "EDITAR USUARIO";
                CargarDatosUsuario(indiceEdicion);
            }
            else this.Text = "INGRESAR USUARIO";
        }
        private void CargarDatosUsuario(int indice)
        {
            var u = DatosClientes.VCliente[indice];
            textBox1.Text = u.nombre;
            textBox2.Text = u.telefono.ToString();
            textBox3.Text = u.email;
            textBox4.Text = u.direccion;
            iconButton1.Text = "Actualizar Usuario";
        }
    }
}
