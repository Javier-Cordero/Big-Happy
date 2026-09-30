using Big_Happy.Productos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Big_Happy.Pedidos
{
    public struct TDPedido
    {
        public int idPedido;
        public string cliente;
        public string usuario;
        public DateTime fecha;
        public string hora;
        public string estado;
        public double total;
        public string metodoPago;
    }
    public struct TDDetallePedido
    {
        public int idDetalle;
        public int idPedido;
        public string producto;
        public int cantidad;
        public string observacion;
        public double subtotal;
    }
    public static class DatosPedidos
    {
        public static TDPedido[] VPedido = new TDPedido[100];
        public static int visPedido = -1;
        public static string rutaPedidos = Path.Combine(Application.StartupPath, "Pedidos.txt");
        public static TDDetallePedido[] VDetalle = new TDDetallePedido[500];
        public static int visDetalle = -1;
        public static string rutaDetallePedidos = Path.Combine(Application.StartupPath, "DetallePedidos.txt");
        // -------------------- PEDIDOS --------------------
        public static int CrearPedido(string cliente, string usuario, DateTime fecha, string hora, string estado, string metodoPago = "")
        {
            int nuevoId = 1;
            for (int i = 0; i <= visPedido; i++) if (VPedido[i].idPedido >= nuevoId) nuevoId = VPedido[i].idPedido + 1;
            visPedido++;
            VPedido[visPedido].idPedido = nuevoId;
            VPedido[visPedido].cliente = cliente;
            VPedido[visPedido].usuario = usuario;
            VPedido[visPedido].fecha = fecha;
            VPedido[visPedido].hora = hora;
            VPedido[visPedido].estado = estado;
            VPedido[visPedido].total = 0;
            VPedido[visPedido].metodoPago = metodoPago;
            return nuevoId;
        }
        public static bool EditarPedido(int idPedido, string cliente, string usuario, DateTime fecha, string hora, string estado, string metodoPago = "")
        {
            for (int i = 0; i <= visPedido; i++)
            {
                if (VPedido[i].idPedido != idPedido) continue;
                VPedido[i].cliente = cliente;
                VPedido[i].usuario = usuario;
                VPedido[i].fecha = fecha;
                VPedido[i].hora = hora;
                VPedido[i].estado = estado;
                if (!string.IsNullOrEmpty(metodoPago)) VPedido[i].metodoPago = metodoPago;
                GuardarEnArchivo();
                return true;
            }
            return false;
        }
        public static void EliminarPedido(int idPedido)
        {
            int pos = -1;
            for (int i = 0; i <= visPedido; i++) if (VPedido[i].idPedido == idPedido) { pos = i; break; }
            if (pos < 0) return;
            for (int i = pos; i < visPedido; i++) VPedido[i] = VPedido[i + 1];
            visPedido--;
            if (visPedido + 1 < VPedido.Length) VPedido[visPedido + 1] = default(TDPedido);
            EliminarDetallesDePedido(idPedido);
            GuardarEnArchivo();
            GuardarDetalleEnArchivo();
        }
        public static bool CambiarEstado(int idPedido, string nuevoEstado)
        {
            for (int i = 0; i <= visPedido; i++)
            {
                if (VPedido[i].idPedido != idPedido) continue;
                VPedido[i].estado = nuevoEstado;
                GuardarEnArchivo();
                return true;
            }
            return false;
        }
        public static void ActualizarTotal(int idPedido, double total)
        {
            for (int i = 0; i <= visPedido; i++)
            {
                if (VPedido[i].idPedido == idPedido)
                {
                    VPedido[i].total = total;
                    return;
                }
            }
        }
        public static double ObtenerTotal(int idPedido)
        {
            for (int i = 0; i <= visPedido; i++) if (VPedido[i].idPedido == idPedido) return VPedido[i].total;
            return 0;
        }
        // -------------------- DETALLES --------------------
        public static void AgregarDetalle(int idPedido, string producto, int cantidad, string observacion, double precio)
        {
            visDetalle++;
            VDetalle[visDetalle].idDetalle = visDetalle + 1;
            VDetalle[visDetalle].idPedido = idPedido;
            VDetalle[visDetalle].producto = producto;
            VDetalle[visDetalle].cantidad = cantidad;
            VDetalle[visDetalle].observacion = observacion;
            VDetalle[visDetalle].subtotal = precio * cantidad;
            RecalcularTotal(idPedido);
        }
        public static void RecalcularTotal(int idPedido)
        {
            double total = 0;
            for (int i = 0; i <= visDetalle; i++) if (VDetalle[i].idPedido == idPedido) total += VDetalle[i].subtotal;
            for (int i = 0; i <= visPedido; i++)
            {
                if (VPedido[i].idPedido == idPedido)
                {
                    VPedido[i].total = total;
                    return;
                }
            }
        }
        public static void EliminarDetallesDePedido(int idPedido)
        {
            int k = 0;
            for (int i = 0; i <= visDetalle; i++)
            {
                if (VDetalle[i].idPedido == idPedido) continue;
                if (k != i) VDetalle[k] = VDetalle[i];
                k++;
            }
            for (int i = k; i <= visDetalle; i++) VDetalle[i] = default(TDDetallePedido);
            visDetalle = k - 1;
        }
        public static int ContarDetalles(int idPedido)
        {
            int c = 0;
            for (int i = 0; i <= visDetalle; i++) if (VDetalle[i].idPedido == idPedido) c++;
            return c;
        }
        public static List<TDDetallePedido> ObtenerDetalles(int idPedido)
        {
            var lista = new List<TDDetallePedido>();
            for (int i = 0; i <= visDetalle; i++) if (VDetalle[i].idPedido == idPedido) lista.Add(VDetalle[i]);
            return lista;
        }
        public static int ContarDetallesHuerfanos()
        {
            int huerfanos = 0;
            for (int i = 0; i <= visDetalle; i++)
            {
                bool existe = false;
                for (int j = 0; j <= visPedido; j++) if (VPedido[j].idPedido == VDetalle[i].idPedido) { existe = true; break; }
                if (!existe) huerfanos++;
            }
            return huerfanos;
        }
        public static void LimpiarDetallesHuerfanos()
        {
            int k = 0;
            for (int i = 0; i <= visDetalle; i++)
            {
                bool existe = false;
                for (int j = 0; j <= visPedido; j++) if (VPedido[j].idPedido == VDetalle[i].idPedido) { existe = true; break; }
                if (!existe) continue;
                if (k != i) VDetalle[k] = VDetalle[i];
                k++;
            }
            for (int i = k; i <= visDetalle; i++) VDetalle[i] = default(TDDetallePedido);
            visDetalle = k - 1;
        }
        // -------------------- GRID --------------------
        public static void MostrarEnGrid(DataGridView grid, string filtro)
        {
            grid.Rows.Clear();
            filtro = (filtro ?? "").Trim();
            for (int i = 0; i <= visPedido; i++)
            {
                if (filtro != "" && !Coincide(VPedido[i], filtro)) continue;
                var productos = new List<string>();
                int cantidadTotal = 0;
                double total = 0;
                for (int j = 0; j <= visDetalle; j++)
                {
                    if (VDetalle[j].idPedido != VPedido[i].idPedido) continue;
                    productos.Add($"{VDetalle[j].producto} x{VDetalle[j].cantidad}");
                    cantidadTotal += VDetalle[j].cantidad;
                    total += VDetalle[j].subtotal;
                }
                grid.Rows.Add(VPedido[i].idPedido, VPedido[i].cliente, VPedido[i].fecha.ToShortDateString(), VPedido[i].hora, string.Join(", ", productos), cantidadTotal, total.ToString("0.00"), VPedido[i].estado);
            }
        }
        private static bool Coincide(TDPedido p, string filtro)
        {
            string f = filtro.ToLower();

            return (p.cliente ?? "").ToLower().Contains(f)
                || (p.usuario ?? "").ToLower().Contains(f)
                || p.fecha.ToString("dd/MM/yyyy").Contains(f)
                || p.fecha.ToString("yyyy-MM-dd").Contains(f)
                || (p.hora ?? "").ToLower().Contains(f)
                || (p.estado ?? "").ToLower().Contains(f)
                || (p.metodoPago ?? "").ToLower().Contains(f);
        }
        // -------------------- ARCHIVOS --------------------
        public static void GuardarEnArchivo()
        {
            using (StreamWriter writer = new StreamWriter(rutaPedidos))
            {
                for (int i = 0; i <= visPedido; i++)
                    writer.WriteLine(VPedido[i].idPedido + "*" + VPedido[i].cliente + "*" + VPedido[i].fecha + "*" + VPedido[i].hora + "*" + VPedido[i].estado + "*" + VPedido[i].total + "*" + (VPedido[i].metodoPago ?? ""));
            }
        }
        public static void GuardarDetalleEnArchivo()
        {
            using (StreamWriter writer = new StreamWriter(rutaDetallePedidos))
            {
                for (int i = 0; i <= visDetalle; i++)
                    writer.WriteLine(VDetalle[i].idDetalle + "*" + VDetalle[i].idPedido + "*" + VDetalle[i].producto + "*" + VDetalle[i].cantidad + "*" + VDetalle[i].observacion + "*" + VDetalle[i].subtotal);
            }
        }
        public static void CargarDetalleDesdeArchivo()
        {
            visDetalle = -1;
            for (int i = 0; i < VDetalle.Length; i++) VDetalle[i] = default(TDDetallePedido);
            if (!File.Exists(rutaDetallePedidos)) return;
            string[] lineas = File.ReadAllLines(rutaDetallePedidos);
            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                string[] datos = linea.Split('*');
                if (datos.Length >= 6)
                {
                    visDetalle++;
                    VDetalle[visDetalle].idDetalle = int.Parse(datos[0].Trim());
                    VDetalle[visDetalle].idPedido = int.Parse(datos[1].Trim());
                    VDetalle[visDetalle].producto = datos[2].Trim();
                    VDetalle[visDetalle].cantidad = int.Parse(datos[3].Trim());
                    VDetalle[visDetalle].observacion = datos[4].Trim();
                    VDetalle[visDetalle].subtotal = double.Parse(datos[5].Trim());
                }
            }
        }
        public static void CargarDesdeArchivo()
        {
            visPedido = -1;
            for (int i = 0; i < VPedido.Length; i++) VPedido[i] = default(TDPedido);
            if (!File.Exists(rutaPedidos)) return;
            string[] lineas = File.ReadAllLines(rutaPedidos);
            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                string[] datos = linea.Split('*');
                if (datos.Length >= 6)
                {
                    visPedido++;
                    VPedido[visPedido].idPedido = int.Parse(datos[0].Trim());
                    VPedido[visPedido].cliente = datos[1].Trim();
                    VPedido[visPedido].fecha = DateTime.Parse(datos[2].Trim());
                    VPedido[visPedido].hora = datos[3].Trim();
                    VPedido[visPedido].estado = datos[4].Trim();
                    VPedido[visPedido].total = double.Parse(datos[5].Trim());
                    VPedido[visPedido].metodoPago = datos.Length > 6 ? datos[6].Trim() : "";
                }
            }
        }
    }
}