using Big_Happy.Loguin;
using Big_Happy.Productos;
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

namespace Big_Happy
{
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }
        private Panel Tarjetas(int ini)
        {
            Panel tarjeta = new Panel();
            tarjeta.Width = 200;
            tarjeta.Height = 230;
            tarjeta.BackColor = Color.White;
            tarjeta.BorderStyle = BorderStyle.FixedSingle;
            tarjeta.Margin = new Padding(10);
            tarjeta.Tag = ini;
            // Imagen
            PictureBox pic = new PictureBox();
            pic.Width = 180;
            pic.Height = 120;
            pic.Location = new Point(10, 10);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.BorderStyle = BorderStyle.FixedSingle;
            string rutaImagen = Path.Combine(DatosProductos.carpetaImagenes, DatosProductos.VProducto[ini].rutaImagen);
            if (File.Exists(rutaImagen))
            {
                using (FileStream fs = new FileStream(rutaImagen, FileMode.Open, FileAccess.Read))
                    pic.Image = Image.FromStream(fs);
            }
            // Nombre
            Label lblNombre = new Label();
            lblNombre.Text = DatosProductos.VProducto[ini].nombre;
            lblNombre.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblNombre.Location = new Point(10, 130);
            lblNombre.Width = 180;
            lblNombre.AutoEllipsis = true;
            // Precio
            Label lblPrecio = new Label();
            lblPrecio.Text = "$" + DatosProductos.VProducto[ini].precio.ToString("0.00");
            lblPrecio.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblPrecio.ForeColor = Color.Green;
            lblPrecio.Location = new Point(10, 160);
            lblPrecio.AutoSize = true;
            // Botón Editar
            Button btnEditar = new Button();
            btnEditar.Text = "✏️";
            btnEditar.Width = 80;
            btnEditar.Height = 30;
            btnEditar.Location = new Point(10, 190);
            btnEditar.BackColor = Color.LightSkyBlue;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.Click += (s, e) =>
            {
                FrmProductoAdd frm = new FrmProductoAdd(ini);
                if (frm.ShowDialog() == DialogResult.OK)
                    MostrarProductos();
            };
            // Botón Eliminar
            Button btnEliminar = new Button();
            btnEliminar.Text = "🗑️";
            btnEliminar.Width = 80;
            btnEliminar.Height = 30;
            btnEliminar.Location = new Point(100, 190);
            btnEliminar.BackColor = Color.LightCoral;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.Click += (s, e) =>
            {
                DialogResult r = MessageBox.Show("¿Eliminar '" + DatosProductos.VProducto[ini].nombre + "'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    DatosProductos.Eliminar(ini);   // ya guarda y actualiza internamente
                    MostrarProductos();
                }
            };

            tarjeta.Controls.Add(pic);
            tarjeta.Controls.Add(lblNombre);
            tarjeta.Controls.Add(lblPrecio);
            tarjeta.Controls.Add(btnEditar);
            tarjeta.Controls.Add(btnEliminar);
            return tarjeta;
        }
        private void MostrarProductos()
        {
            flowLayoutPanel1.Controls.Clear();
            var grupos = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i <= DatosProductos.vis; i++)
            {
                string cat = DatosProductos.VProducto[i].categoria;
                if (string.IsNullOrWhiteSpace(cat)) cat = "Sin categoría";
                else cat = cat.Trim();
                if (!grupos.ContainsKey(cat)) grupos[cat] = new List<int>();
                grupos[cat].Add(i);
            }
            foreach (var categoria in grupos.Keys.OrderBy(c => c, StringComparer.OrdinalIgnoreCase))
            {
                flowLayoutPanel1.Controls.Add(CrearEncabezadoCategoria(categoria));
                foreach (int indice in grupos[categoria])
                {
                    Panel tarjeta = Tarjetas(indice);
                    flowLayoutPanel1.Controls.Add(tarjeta);
                }
            }
            if (DatosProductos.vis < 0)
            {
                Label lblVacio = new Label();
                lblVacio.Text = "No hay productos registrados.";
                lblVacio.Font = new Font("Segoe UI", 11, FontStyle.Italic);
                lblVacio.ForeColor = Color.Gray;
                lblVacio.AutoSize = true;
                lblVacio.Margin = new Padding(15);
                flowLayoutPanel1.Controls.Add(lblVacio);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FrmProductoAdd FPA = new FrmProductoAdd();
            if (ModalHelper.ShowCentered(this, FPA) == DialogResult.OK)
                MostrarProductos();
        }
        private void FrmProductos_Load(object sender, EventArgs e)
        {
            if (!Sesion.ExigirPermiso(Sesion.PuedeGestionarProductos)) { this.Close(); return; }
            DatosProductos.CrearCarpetaImagenes();
            DatosProductos.CargarDesdeArchivo();
            MostrarProductos();
        }
        private Label CrearEncabezadoCategoria(string categoria)
        {
            Label lbl = new Label();
            lbl.Text = "📂 " + categoria;
            lbl.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(40, 40, 40);
            lbl.BackColor = Color.FromArgb(230, 240, 255);
            lbl.Height = 35;
            lbl.Width = flowLayoutPanel1.ClientSize.Width - 25; // ocupa todo el ancho
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Padding = new Padding(10, 0, 0, 0);
            lbl.Margin = new Padding(10, 15, 10, 5);
            return lbl;
        }
    }
}
