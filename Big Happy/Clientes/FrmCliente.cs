using Big_Happy.Loguin;
using Big_Happy.Productos;
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
    public partial class FrmCliente : Form
    {
        public FrmCliente()
        {
            InitializeComponent();
        }
        private void FrmCliente_Load(object sender, EventArgs e)
        {
            if (!Sesion.ExigirPermiso(Sesion.PuedeGestionarClientes)) { this.Close(); return; }
            ConfigurarGrid();
            DatosClientes.CargarDesdeArchivo();
            DatosClientes.Mostrar(dataGridView1);
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmClienteAdd()) ModalHelper.ShowCentered(this, frm);
            DatosClientes.CargarDesdeArchivo();
            DatosClientes.Mostrar(dataGridView1);
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            DatosClientes.Mostrar(dataGridView1, textBox1.Text);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int indice = (int)dataGridView1.Rows[e.RowIndex].Tag;
            string col = dataGridView1.Columns[e.ColumnIndex].Name;
            if (col == "colEditar")
            {
                if (!Sesion.ExigirPermiso(Sesion.PuedeGestionarUsuarios)) return;
                using (var frm = new FrmClienteAdd(indice))
                    if (frm.ShowDialog(this) == DialogResult.OK) DatosClientes.Mostrar(dataGridView1);
            }
            else if (col == "colEliminar")
            {
                if (!Sesion.ExigirPermiso(Sesion.EsAdministrador, "Solo el Administrador puede eliminar usuarios.")) return;
                var resp = MessageBox.Show("¿Eliminar este usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resp == DialogResult.Yes)
                {
                    DatosClientes.Eliminar(indice);
                    DatosClientes.Mostrar(dataGridView1);
                }
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
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            // 5 columnas de texto
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCodigo", HeaderText = "ID", Width = 50 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", HeaderText = "Nombre" });           
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTelefono", HeaderText = "Teléfono" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEmail", HeaderText = "Email" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDireccion", HeaderText = "Direccion" });
            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colEditar",
                HeaderText = "",
                Text = "✏️ Editar",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            });
            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colEliminar",
                HeaderText = "",
                Text = "🗑️ Eliminar",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            });
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DatosClientes.Mostrar(dataGridView1, textBox1.Text);
        }
    }
}
