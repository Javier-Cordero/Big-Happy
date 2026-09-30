using Big_Happy.Usuarios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Big_Happy.Clientes
{
    public struct TDCliente
    {
        public int codigo;
        public string nombre;
        public int telefono;
        public string email;
        public string direccion;
    }
    public static class DatosClientes
    {
        public static TDCliente[] VCliente = new TDCliente[500];
        public static int vis = -1;
        public static string rutaArchivo = Path.Combine(Application.StartupPath, "Clientes.txt");
        // Agregar
        public static bool Guardar(string nombre, int telefono, string email, string direccion)
        {
            vis++;
            VCliente[vis].codigo = vis + 1;
            VCliente[vis].nombre = nombre;
            VCliente[vis].telefono = telefono;
            VCliente[vis].email = email;
            VCliente[vis].direccion = direccion;
            GuardarEnArchivo();
            return true;
        }
        // Editar       
        public static bool Editar(int indice, string nombre, int telefono, string email, string direccion)
        {
            if (indice < 0 || indice > vis) return false;
            VCliente[indice].nombre = nombre;
            VCliente[indice].telefono = telefono;
            VCliente[indice].email = email;
            VCliente[indice].direccion = direccion;
            GuardarEnArchivo();
            return true;
        }
        // Eliminar
        public static void Eliminar(int indice)
        {
            if (indice < 0 || indice > vis) return;
            for (int i = indice; i < vis; i++)
                VCliente[i] = VCliente[i + 1];
            vis--;
            GuardarEnArchivo();
        }
        // Mostrar
        public static void Mostrar(DataGridView grid, string filtro = "")
        {
            grid.Rows.Clear();
            filtro = (filtro ?? "").Trim().ToLower();
            for (int i = 0; i <= vis; i++)
            {
                if (filtro != "" && !Coincide(VCliente[i], filtro)) continue;
                int rowIndex = grid.Rows.Add(VCliente[i].codigo, VCliente[i].nombre, VCliente[i].telefono, VCliente[i].email, VCliente[i].direccion);
                grid.Rows[rowIndex].Tag = i;
            }
        }
        private static bool Coincide(TDCliente u, string filtro)
        {
            return (u.nombre ?? "").ToLower().Contains(filtro) || (u.telefono.ToString() ?? "").ToLower().Contains(filtro) || (u.email ?? "").ToLower().Contains(filtro) || (u.direccion ?? "").ToLower().Contains(filtro);
        }
        // Guardar en archivo
        public static void GuardarEnArchivo()
        {
            using (StreamWriter writer = new StreamWriter(rutaArchivo))
            {
                for (int i = 0; i <= vis; i++)
                    writer.WriteLine(
                        VCliente[i].codigo + "*" +
                        VCliente[i].nombre + "*" +
                        VCliente[i].telefono + "*" +
                        VCliente[i].email + "*" +
                        VCliente[i].direccion);
            }
        }
        public static void CargarDesdeArchivo()
        {
            if (!File.Exists(rutaArchivo)) return;
            string[] lineas = File.ReadAllLines(rutaArchivo);
            vis = -1;
            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                string[] datos = linea.Split('*');
                if (datos.Length == 5)
                {
                    vis++;
                    VCliente[vis].codigo = int.Parse(datos[0].Trim());
                    VCliente[vis].nombre = datos[1].Trim();
                    VCliente[vis].telefono = int.Parse(datos[2].Trim());
                    VCliente[vis].email = datos[3].Trim();
                    VCliente[vis].direccion = datos[4].Trim();
                }
            }
        }
    }
}
