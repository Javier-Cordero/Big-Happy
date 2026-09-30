using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Big_Happy.Productos
{
    public partial class FrmProductoAdd : Form
    {
        public FrmProductoAdd()
        {
            InitializeComponent();
            indiceEdicion = -1;
        }
        private int indiceEdicion = -1;
        private string nombreImagenTemporal = "";
        public FrmProductoAdd(int indice)
        {
            InitializeComponent();
            indiceEdicion = indice;
            Codigo.Text = DatosProductos.VProducto[indice].codigo.ToString();
            Nombre.Text = DatosProductos.VProducto[indice].nombre;
            Precio.Text = DatosProductos.VProducto[indice].precio.ToString();
            Categoria.Text = DatosProductos.VProducto[indice].categoria;
            Stock.Value = DatosProductos.VProducto[indice].stock;
            nombreImagenTemporal = DatosProductos.VProducto[indice].rutaImagen;
            string ruta = Path.Combine(DatosProductos.carpetaImagenes, nombreImagenTemporal);
            if (File.Exists(ruta))
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
                    pictureBox1.Image = Image.FromStream(fs);
            }
        }
        private void Guardar()
        {
            if (string.IsNullOrWhiteSpace(Nombre.Text) ||
                string.IsNullOrWhiteSpace(Precio.Text))
            {
                MessageBox.Show("Complete nombre y precio.");
                return;
            }
            double precio;
            if (!double.TryParse(Precio.Text, out precio))
            {
                MessageBox.Show("Precio inválido.");
                return;
            }
            if (indiceEdicion == -1)
            {
                bool ok = DatosProductos.Guardar(Nombre.Text, precio, Categoria.Text, (int)Stock.Value, nombreImagenTemporal);
                if (!ok) return;
            }
            else
                DatosProductos.Editar(indiceEdicion, Nombre.Text, precio, Categoria.Text, (int)Stock.Value, nombreImagenTemporal);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void LimpiarFormulario()
        {
            Codigo.Clear();
            Nombre.Clear();
            Precio.Clear();
            Categoria.SelectedIndex = -1;
            Stock.Value = 0;
            pictureBox1.Image = null;
            Codigo.Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog1.Title = "Seleccionar imagen del producto";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                nombreImagenTemporal = DatosProductos.CopiarImagen(openFileDialog1.FileName, DatosProductos.vis + 1);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void FrmProductoAdd_Load(object sender, EventArgs e)
        {
            DatosProductos.CrearCarpetaImagenes();
        }
    }
}
