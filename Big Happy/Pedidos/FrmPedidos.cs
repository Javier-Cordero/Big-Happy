using Big_Happy.Loguin;
using System;
using System.Windows.Forms;

namespace Big_Happy.Pedidos
{
    public partial class FrmPedidos : Form
    {
        public FrmPedidos()
        {
            InitializeComponent();
            dataGridView1.CellContentClick -= dataGridView1_CellContentClick; // quita si ya estaba
            dataGridView1.CellContentClick += dataGridView1_CellContentClick; // agrega una sola vez
        }
        private void FrmPedidos_Load(object sender, EventArgs e)
        {
            if (!Sesion.ExigirPermiso(Sesion.PuedeEmitirTickets)) { this.Close(); return; }
            DatosPedidos.CargarDesdeArchivo();
            DatosPedidos.CargarDetalleDesdeArchivo();
            DatosPedidos.LimpiarDetallesHuerfanos();
            DatosPedidos.GuardarDetalleEnArchivo();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("idPedido", "ID");
            dataGridView1.Columns.Add("cliente", "Cliente");
            dataGridView1.Columns.Add("fecha", "Fecha");
            dataGridView1.Columns.Add("hora", "Hora");
            dataGridView1.Columns.Add("productos", "Productos");
            dataGridView1.Columns.Add("cantidad", "Cant.");
            dataGridView1.Columns.Add("total", "Total");
            dataGridView1.Columns.Add("estado", "Estado");
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.Name = "btnEditar";
            btnEditar.HeaderText = "Editar";
            btnEditar.Text = "✏️";
            btnEditar.UseColumnTextForButtonValue = true;
            btnEditar.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Add(btnEditar);
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "🗑️";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Add(btnEliminar);
            DatosPedidos.MostrarEnGrid(dataGridView1, "");
        }
        private void RefrescarGrid()
        {
            DatosPedidos.CargarDesdeArchivo();
            DatosPedidos.CargarDetalleDesdeArchivo();
            DatosPedidos.MostrarEnGrid(dataGridView1, textBox1.Text);
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            FrmPedidoAdd frm = new FrmPedidoAdd();
            if (ModalHelper.ShowCentered(this, frm) == DialogResult.OK)
                RefrescarGrid();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DatosPedidos.MostrarEnGrid(dataGridView1, textBox1.Text);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string col = dataGridView1.Columns[e.ColumnIndex].Name;
            int idPedido = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idPedido"].Value);
            if (col == "btnEditar")
            {
                FrmPedidoAdd frm = new FrmPedidoAdd(idPedido);
                if (ModalHelper.ShowCentered(this, frm) == DialogResult.OK) RefrescarGrid();
            }
            else if (col == "btnEliminar")
            {
                DialogResult r = MessageBox.Show($"¿Eliminar el pedido #{idPedido}?\nTambién se eliminarán sus detalles.", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    DatosPedidos.EliminarPedido(idPedido);
                    RefrescarGrid();
                }
            }
        }
    }
}