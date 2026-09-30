using Big_Happy.Pedidos;
using Big_Happy.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_Happy.Informacion
{
    public class VentaDia
    {
        public DateTime Fecha;
        public double Total;
    }
    public class ProductoVendido
    {
        public string Producto;
        public int Cantidad;
        public double Total;
    }
    public static class DatosDashboard
    {
        public static double VentasDelDia() => VentasDelDia(DateTime.Today);
        public static double VentasDelDia(DateTime dia)
        {
            double t = 0;
            for (int i = 0; i <= DatosPedidos.visPedido; i++)
                if (DatosPedidos.VPedido[i].fecha.Date == dia.Date) t += DatosPedidos.VPedido[i].total;
            return t;
        }
        public static double VentasDelMes()
        {
            DateTime hoy = DateTime.Today;
            double t = 0;
            for (int i = 0; i <= DatosPedidos.visPedido; i++)
            {
                var f = DatosPedidos.VPedido[i].fecha;
                if (f.Year == hoy.Year && f.Month == hoy.Month) t += DatosPedidos.VPedido[i].total;
            }
            return t;
        }
        public static double VentasMesAnterior()
        {
            DateTime refMes = DateTime.Today.AddMonths(-1);
            double t = 0;
            for (int i = 0; i <= DatosPedidos.visPedido; i++)
            {
                var f = DatosPedidos.VPedido[i].fecha;
                if (f.Year == refMes.Year && f.Month == refMes.Month) t += DatosPedidos.VPedido[i].total;
            }
            return t;
        }
        public static double VentasAyer() => VentasDelDia(DateTime.Today.AddDays(-1));

        public static int PedidosDelDia()
        {
            DateTime hoy = DateTime.Today;
            int c = 0;
            for (int i = 0; i <= DatosPedidos.visPedido; i++) if (DatosPedidos.VPedido[i].fecha.Date == hoy) c++;
            return c;
        }
        public static int PedidosDelMes()
        {
            DateTime hoy = DateTime.Today;
            int c = 0;
            for (int i = 0; i <= DatosPedidos.visPedido; i++)
            {
                var f = DatosPedidos.VPedido[i].fecha;
                if (f.Year == hoy.Year && f.Month == hoy.Month) c++;
            }
            return c;
        }
        public static ProductoVendido ProductoMasVendido()
        {
            var dict = new Dictionary<string, int>();
            for (int i = 0; i <= DatosPedidos.visDetalle; i++)
            {
                var d = DatosPedidos.VDetalle[i];
                if (!dict.ContainsKey(d.producto)) dict[d.producto] = 0;
                dict[d.producto] += d.cantidad;
            }
            if (dict.Count == 0) return null;
            var top = dict.OrderByDescending(x => x.Value).First();
            return new ProductoVendido { Producto = top.Key, Cantidad = top.Value };
        }
        public static List<ProductoVendido> TodosLosProductos()
        {
            var dict = new Dictionary<string, ProductoVendido>();
            for (int i = 0; i <= DatosPedidos.visDetalle; i++)
            {
                var d = DatosPedidos.VDetalle[i];
                if (!dict.ContainsKey(d.producto)) dict[d.producto] = new ProductoVendido { Producto = d.producto };
                dict[d.producto].Cantidad += d.cantidad;
                dict[d.producto].Total += d.subtotal;
            }
            return dict.Values.OrderByDescending(x => x.Cantidad).ToList();
        }
        public static List<VentaDia> Ultimos7Dias()
        {
            var lista = new List<VentaDia>();
            for (int i = 6; i >= 0; i--)
            {
                var dia = DateTime.Today.AddDays(-i);
                lista.Add(new VentaDia { Fecha = dia, Total = VentasDelDia(dia) });
            }
            return lista;
        }
        public static double VariacionHoyVsAyer()
        {
            double ayer = VentasAyer();
            if (ayer <= 0) return VentasDelDia() > 0 ? 100 : 0;
            return ((VentasDelDia() - ayer) / ayer) * 100.0;
        }
        public static double VariacionMesVsAnterior()
        {
            double ant = VentasMesAnterior();
            if (ant <= 0) return VentasDelMes() > 0 ? 100 : 0;
            return ((VentasDelMes() - ant) / ant) * 100.0;
        }
        public static List<ProductoVendido> ProductosDelCatalogoConVentas()
        {
            var lista = new List<ProductoVendido>();
            for (int i = 0; i <= DatosProductos.vis; i++)
            {
                string nombre = DatosProductos.VProducto[i].nombre;
                if (string.IsNullOrWhiteSpace(nombre)) continue;

                var pv = new ProductoVendido
                {
                    Producto = nombre,
                    Cantidad = 0,
                    Total = 0
                };
                for (int j = 0; j <= DatosPedidos.visDetalle; j++)
                {
                    var d = DatosPedidos.VDetalle[j];
                    if (string.Equals(d.producto, nombre, StringComparison.OrdinalIgnoreCase))
                    {
                        pv.Cantidad += d.cantidad;
                        pv.Total += d.subtotal;
                    }
                }
                lista.Add(pv);
            }
            return lista.OrderByDescending(x => x.Cantidad).ToList();
        }
    }
}