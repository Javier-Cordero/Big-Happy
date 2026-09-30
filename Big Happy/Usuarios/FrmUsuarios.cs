using Big_Happy.Loguin;
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

namespace Big_Happy.Usuarios
{
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }
        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            if (!Sesion.ExigirPermiso(Sesion.PuedeGestionarUsuarios, "Solo el Administrador puede gestionar usuarios."))
            {
                this.Close();
                return;
            }
            ConfigurarGrid();
            DatosUsuarios.CargarDesdeArchivo();
            DatosUsuarios.Mostrar(dataGridView1);
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmUsuarioAdd()) ModalHelper.ShowCentered(this, frm);
            DatosUsuarios.CargarDesdeArchivo();
            DatosUsuarios.Mostrar(dataGridView1);
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            DatosUsuarios.Mostrar(dataGridView1, textBox1.Text);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int indice = (int)dataGridView1.Rows[e.RowIndex].Tag;
            string col = dataGridView1.Columns[e.ColumnIndex].Name;
            if (col == "colEditar")
            {
                if (!Sesion.ExigirPermiso(Sesion.PuedeGestionarUsuarios)) return;
                using (var frm = new FrmUsuarioAdd(indice))
                    if (frm.ShowDialog(this) == DialogResult.OK) DatosUsuarios.Mostrar(dataGridView1);
            }
        }
        private void ConfigurarGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowTemplate.Height = 60;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            // 8 columnas de texto
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCodigo", HeaderText = "Código", Width = 50 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", HeaderText = "Nombre" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCI", HeaderText = "CI" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRol", HeaderText = "Rol" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTurno", HeaderText = "Turno" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEmail", HeaderText = "Email" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTelefono", HeaderText = "Teléfono" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colContraseña", HeaderText = "Contraseña" });
            dataGridView1.Columns.Add(new DataGridViewImageColumn
            {
                Name = "colPerfil",
                HeaderText = "Perfil",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = null }
            });
            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colEditar",
                HeaderText = "",
                Text = "✏️ Editar",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            });
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DatosUsuarios.Mostrar(dataGridView1, textBox1.Text);
        }
    }
}
