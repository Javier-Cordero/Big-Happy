using Big_Happy.Loguin;
using Big_Happy.Pedidos;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace Big_Happy.Informacion
{
    public partial class FrmInformaciones : Form
    {
        public FrmInformaciones()
        {
            InitializeComponent();
        }
        private void CargarTodo()
        {
            DatosProductos.CargarDesdeArchivo();
            DatosPedidos.CargarDesdeArchivo();
            DatosPedidos.CargarDetalleDesdeArchivo();
            CargarKpis();
            CargarGraficoProductos();
        }
        private void CargarKpis()
        {
            // --- Tarjeta 1: Ingresos hoy ---
            double hoy = DatosDashboard.VentasDelDia();
            double varHoy = DatosDashboard.VariacionHoyVsAyer();
            label2.Text = "$ " + hoy.ToString("N2");
            label3.Text = FormatearVariacion(varHoy);
            label3.ForeColor = varHoy >= 0 ? Color.Green : Color.Red;
            // --- Tarjeta 2: Ingresos mes ---
            double mes = DatosDashboard.VentasDelMes();
            double varMes = DatosDashboard.VariacionMesVsAnterior();
            label5.Text = "$ " + mes.ToString("N2");
            label6.Text = FormatearVariacion(varMes);
            label6.ForeColor = varMes >= 0 ? Color.Green : Color.Red;
            // --- Tarjeta 3: Pedidos ---
            label8.Text = DatosDashboard.PedidosDelDia().ToString();
            label7.Text = "Mes: " + DatosDashboard.PedidosDelMes();
            // --- Tarjeta 4: Producto top ---
            var top = DatosDashboard.ProductoMasVendido();
            if (top != null)
            {
                label10.Text = top.Producto;
                label11.Text = top.Cantidad + " vendidos";
            }
            else
            {
                label10.Text = "---";
                label11.Text = "Sin datos";
            }

            // Colores de fondo de las tarjetas
            panel1.BackColor = Color.FromArgb(46, 204, 113);   // verde
            panel2.BackColor = Color.FromArgb(52, 152, 219);   // azul
            panel3.BackColor = Color.FromArgb(155, 89, 182);   // morado
            panel4.BackColor = Color.FromArgb(230, 126, 34);   // naranja
        }
        private string FormatearVariacion(double pct)
        {
            if (pct > 0) return "▲ +" + pct.ToString("N1") + "% vs período anterior";
            if (pct < 0) return "▼ " + pct.ToString("N1") + "% vs período anterior";
            return "= 0% vs período anterior";
        }
        private void CargarGraficoProductos()
        {
            // ---------- LIMPIAR ----------
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear();
            chart1.ChartAreas.Clear();
            chart1.Palette = ChartColorPalette.None;
            chart1.BackColor = Color.White;
            // ---------- ÁREA ----------
            var area = new ChartArea("main");
            area.BackColor = Color.White;
            area.AxisX.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisX.LineColor = Color.LightGray;
            area.AxisX.Title = "Unidades vendidas";
            area.AxisX.TitleFont = new Font("Segoe UI", 9, FontStyle.Bold);
            area.AxisX.Minimum = 0;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            // Eje Y → PRODUCTOS (horizontal abajo)
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            area.AxisY.LabelStyle.Interval = 1;
            area.AxisY.Interval = 1;
            area.AxisY.IsLabelAutoFit = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisY.LineColor = Color.LightGray;
            area.AxisY.Title = "Productos";
            area.AxisY.TitleFont = new Font("Segoe UI", 9, FontStyle.Bold);
            chart1.ChartAreas.Add(area);
            // ---------- TÍTULO ----------
            chart1.Titles.Add(new Title(
                "RENDIMIENTO DE PRODUCTOS",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.FromArgb(52, 73, 94)));
            // ---------- SERIE ----------
            var serie = new Series("Productos")
            {
                ChartType = SeriesChartType.Bar,
                IsValueShownAsLabel = true,
                LabelFormat = "N0",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                LabelForeColor = Color.FromArgb(52, 73, 94),
                CustomProperties = "PointWidth=0.6"
            };
            chart1.Series.Add(serie);
            // ---------- DATOS ----------
            var datos = DatosDashboard.ProductosDelCatalogoConVentas();
            if (datos.Count == 0)
            {
                chart1.Titles.Add(new Title(
                    "(No hay productos registrados)",
                    Docking.Bottom,
                    new Font("Segoe UI", 10, FontStyle.Italic),
                    Color.Gray));
                return;
            }
            // ---------- PINTAR CON SEMÁFORO ----------
            int max = datos.Max(x => x.Cantidad);
            foreach (var d in datos)
            {
                int idx = serie.Points.AddXY(d.Producto, d.Cantidad);
                Color color;
                if (d.Cantidad == 0) color = Color.FromArgb(189, 195, 199);
                else
                {
                    double ratio = max > 0 ? (double)d.Cantidad / max : 0;
                    if (ratio >= 0.7) color = Color.FromArgb(46, 204, 113);
                    else if (ratio >= 0.3) color = Color.FromArgb(241, 196, 15);
                    else color = Color.FromArgb(231, 76, 60);
                }
                serie.Points[idx].Color = color;
                serie.Points[idx].ToolTip = $"{d.Producto}\nVendidos: {d.Cantidad}\nTotal: $ {d.Total:N2}";
            }
        }
        private void FrmInformaciones_Load(object sender, EventArgs e)
        {
            if (!Sesion.ExigirPermiso(Sesion.PuedeVerInformacion)) { this.Close(); return; }
            CargarTodo();
        }
    }
}
