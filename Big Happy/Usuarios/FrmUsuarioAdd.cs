using Big_Happy.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Big_Happy.Usuarios
{
    public partial class FrmUsuarioAdd : Form
    {
        public FrmUsuarioAdd()
        {
            InitializeComponent();
        }
        private bool esEdicion = false;
        private int indiceEdicion = -1;
        private string nombreImagenTemporal = "";
        private string rutaImagenActual = "";
        public FrmUsuarioAdd(int indice) : this()
        {
            esEdicion = true;
            indiceEdicion = indice;
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Seleccionar foto de perfil";
                dlg.Filter = "Imágenes (*.jpg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var tmp = Image.FromFile(dlg.FileName)) pictureBox1.Image = new Bitmap(tmp);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    nombreImagenTemporal = dlg.FileName;
                }
            }
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Complete los campos obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textBox4.Text, out int telefono))
            {
                MessageBox.Show("El teléfono debe ser numérico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (esEdicion)
            {
                string rutaFoto = string.IsNullOrEmpty(nombreImagenTemporal) ? rutaImagenActual : nombreImagenTemporal;
                DatosUsuarios.Editar(indiceEdicion, textBox1.Text, textBox2.Text, comboBox1.Text, comboBox2.Text, textBox3.Text, telefono, textBox5.Text, rutaFoto);
                MessageBox.Show("Usuario actualizado correctamente.");
            }
            else
            {
                DatosUsuarios.Guardar(textBox1.Text, textBox2.Text, comboBox1.Text, comboBox2.Text, textBox3.Text, telefono, textBox5.Text, nombreImagenTemporal);
                MessageBox.Show("Usuario guardado correctamente.");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FrmUsuarioAdd_Load(object sender, EventArgs e)
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
            var u = DatosUsuarios.VUsuario[indice];
            textBox1.Text = u.nombre;
            textBox2.Text = u.CI;
            comboBox1.Text = u.rol;
            comboBox2.Text = u.turno;
            textBox3.Text = u.email;
            textBox4.Text = u.telefono.ToString();
            textBox5.Text = u.contraseña;
            rutaImagenActual = u.rutaImagen;
            if (!string.IsNullOrEmpty(u.rutaImagen) && File.Exists(u.rutaImagen))
            {
                using (var tmp = Image.FromFile(u.rutaImagen)) pictureBox1.Image = new Bitmap(tmp);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            iconButton1.Text = "Actualizar Usuario";
        }
    }
}
