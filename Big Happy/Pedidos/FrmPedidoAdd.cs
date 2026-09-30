using Big_Happy.Clientes;
using Big_Happy.Loguin;
using Big_Happy.Productos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Big_Happy.Pedidos
{
    public partial class FrmPedidoAdd : Form
    {
        public FrmPedidoAdd()
        {
            InitializeComponent();
        }
        public FrmPedidoAdd(int idPedido) : this()
        {
            idPedidoEditar = idPedido;
        }
        private int idPedidoEditar = -1;
        private int contadorPedidos = 0;
        private void CrearBloquePedido(int numero, string productoInicial = null,int cantidadInicial = 1, string observacionInicial = "")
        {
            GroupBox gb = new GroupBox
            {
                Text = $"Pedido #{numero}",
                Name = $"gbPedido{numero}",
                Size = new Size(flowLayoutPanel1.ClientSize.Width, 100),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.White
            };
            Label lblProducto = new Label
            {
                Text = "PRODUCTO",
                Location = new Point(10, 30),
                Size = new Size(80, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            gb.Controls.Add(lblProducto);
            ComboBox cmbProducto = new ComboBox
            {
                Name = $"cmbProducto{numero}",
                Location = new Point(100, 27),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            for (int i = 0; i <= DatosProductos.vis; i++)
                cmbProducto.Items.Add(DatosProductos.VProducto[i].nombre);
            if (!string.IsNullOrEmpty(productoInicial))
            {
                int idx = cmbProducto.Items.IndexOf(productoInicial);
                if (idx >= 0) cmbProducto.SelectedIndex = idx;
                else if (cmbProducto.Items.Count > 0) cmbProducto.SelectedIndex = 0;
            }
            else if (cmbProducto.Items.Count > 0) cmbProducto.SelectedIndex = 0;
            gb.Controls.Add(cmbProducto);
            Label lblCantidad = new Label
            {
                Text = "CANTIDAD",
                Location = new Point(210, 30),
                Size = new Size(70, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            gb.Controls.Add(lblCantidad);
            NumericUpDown nudCantidad = new NumericUpDown
            {
                Name = $"nudCantidad{numero}",
                Location = new Point(290, 27),
                Size = new Size(60, 25),
                Minimum = 1,
                Maximum = 100,
                Value = cantidadInicial < 1 ? 1 : (cantidadInicial > 100 ? 100 : cantidadInicial)
            };
            gb.Controls.Add(nudCantidad);
            Label lblObs = new Label
            {
                Text = "OBSERVACIÓN",
                Location = new Point(10, 60),
                Size = new Size(85, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            gb.Controls.Add(lblObs);
            TextBox txtObs = new TextBox
            {
                Name = $"txtObs{numero}",
                Location = new Point(100, 62),
                Size = new Size(335, 25),
                Text = observacionInicial ?? ""
            };
            gb.Controls.Add(txtObs);
            Button btnEliminar = new Button
            {
                Text = "🗑",
                Location = new Point(gb.Width - 45, 25),
                Size = new Size(30, 30),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Tag = gb,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnEliminar.Click += (s, ev) => { flowLayoutPanel1.Controls.Remove(gb); gb.Dispose(); };
            gb.Controls.Add(btnEliminar);
            flowLayoutPanel1.Controls.Add(gb);
            flowLayoutPanel1.ScrollControlIntoView(gb);
        }
        private int ObtenerIndiceProducto(string nombre)
        {
            for (int i = 0; i <= DatosProductos.vis; i++)
                if (DatosProductos.VProducto[i].nombre == nombre) return i;
            return -1;
        }

        private void CargarPedidoParaEditar(int idPedido)
        {
            // Cabecera
            for (int i = 0; i <= DatosPedidos.visPedido; i++)
            {
                if (DatosPedidos.VPedido[i].idPedido != idPedido) continue;
                var p = DatosPedidos.VPedido[i];
                comboBox1.Text = p.cliente;
                Fecha.Value = p.fecha;
                Hora.Text = p.hora;
                textBox1.Text = p.estado;
                break;
            }
            // Detalles → crear GroupBoxes
            flowLayoutPanel1.Controls.Clear();
            contadorPedidos = 0;
            var detalles = DatosPedidos.ObtenerDetalles(idPedido);
            foreach (var d in detalles)
            {
                contadorPedidos++;
                CrearBloquePedido(contadorPedidos, d.producto, d.cantidad, d.observacion);
            }
        }
        private void FrmPedidoAdd_Load(object sender, EventArgs e)
        {
            label1.Text = Sesion.Nombre;
            DatosProductos.CargarDesdeArchivo();
            DatosClientes.CargarDesdeArchivo();
            DatosPedidos.CargarDesdeArchivo();
            DatosPedidos.CargarDetalleDesdeArchivo();
            DatosPedidos.LimpiarDetallesHuerfanos();
            comboBox1.Items.Clear();
            for (int i = 0; i <= DatosClientes.vis; i++) comboBox1.Items.Add(DatosClientes.VCliente[i].nombre);
            if (idPedidoEditar > 0)
            {
                CargarPedidoParaEditar(idPedidoEditar);
                this.Text = "Editar Pedido";
            }
            else
            {
                if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
                this.Text = "Nuevo Pedido";
            }
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            contadorPedidos++;
            CrearBloquePedido(contadorPedidos);
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona un cliente.");
                return;
            }
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("Agrega al menos un producto.");
                return;
            }
            int idPedido;
            if (idPedidoEditar > 0)
            {
                // EDITAR: actualizar cabecera y reemplazar detalles
                idPedido = idPedidoEditar;
                DatosPedidos.EditarPedido(idPedido, comboBox1.Text, label1.Text, Fecha.Value, Hora.Text, textBox1.Text);
                // Borrar detalles viejos
                DatosPedidos.EliminarDetallesDePedido(idPedido);
            }
            else
            {
                // CREAR
                idPedido = DatosPedidos.CrearPedido(comboBox1.Text, label1.Text, Fecha.Value, Hora.Text, textBox1.Text);
            }
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                GroupBox gb = flowLayoutPanel1.Controls[i] as GroupBox;
                if (gb == null) continue;
                ComboBox cmb = null;
                NumericUpDown nud = null;
                TextBox txt = null;
                for (int j = 0; j < gb.Controls.Count; j++)
                {
                    if (gb.Controls[j] is ComboBox) cmb = (ComboBox)gb.Controls[j];
                    else if (gb.Controls[j] is NumericUpDown) nud = (NumericUpDown)gb.Controls[j];
                    else if (gb.Controls[j] is TextBox) txt = (TextBox)gb.Controls[j];
                }
                if (cmb == null || nud == null || cmb.SelectedIndex < 0) continue;
                string nombre = cmb.Text;
                int cant = (int)nud.Value;
                string obs = txt != null ? txt.Text : "";
                double precio = 0;
                for (int k = 0; k <= DatosProductos.vis; k++)
                {
                    if (DatosProductos.VProducto[k].nombre == nombre)
                    {
                        precio = DatosProductos.VProducto[k].precio;
                        break;
                    }
                }
                DatosPedidos.AgregarDetalle(idPedido, nombre, cant, obs, precio);
            }
            DatosPedidos.RecalcularTotal(idPedido);
            DatosPedidos.GuardarEnArchivo();
            DatosPedidos.GuardarDetalleEnArchivo();
            string accion = idPedidoEditar > 0 ? "actualizado" : "guardado";
            MessageBox.Show($"Pedido #{idPedido} {accion}. Total: $" + DatosPedidos.ObtenerTotal(idPedido).ToString("0.00"));
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}