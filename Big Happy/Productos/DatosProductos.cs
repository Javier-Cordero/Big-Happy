using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Big_Happy.Productos
{
    public struct TDProducto
    {
        public int codigo;
        public string nombre;
        public double precio;
        public string categoria;
        public int stock;
        public string rutaImagen;
    }
    public static class DatosProductos
    {
        public static TDProducto[] VProducto = new TDProducto[50];
        public static int vis = -1;
        public static string carpetaImagenes = Path.Combine(Application.StartupPath, "ImagenesProductos");
        public static string rutaArchivo = Path.Combine(Application.StartupPath, "Productos.txt");
        // Agregar
        public static bool Guardar(string nombre, double precio, string categoria, int stock, string ruta)
        {
            vis++;
            VProducto[vis].codigo = vis + 1;
            VProducto[vis].nombre = nombre;
            VProducto[vis].precio = precio;
            VProducto[vis].categoria = categoria;
            VProducto[vis].stock = stock;
            VProducto[vis].rutaImagen = ruta;
            GuardarEnArchivo();
            return true;
        }
        // Editar       
        public static bool Editar(int indice, string nombre, double precio, string categoria, int stock, string ruta)
        {
            if (indice < 0 || indice > vis) return false;

            VProducto[indice].nombre = nombre;
            VProducto[indice].precio = precio;
            VProducto[indice].categoria = categoria;
            VProducto[indice].stock = stock;
            VProducto[indice].rutaImagen = ruta;
            GuardarEnArchivo();
            return true;
        }
        // Eliminar
        public static void Eliminar(int indice)
        {
            if (indice < 0 || indice > vis) return;
            for (int i = indice; i < vis; i++)
                VProducto[i] = VProducto[i + 1];
            vis--;
            GuardarEnArchivo();
        }
        // Guardar en archivo
        public static void GuardarEnArchivo()
        {
            using (StreamWriter writer = new StreamWriter(rutaArchivo))
            {
                for (int i = 0; i <= vis; i++)
                    writer.WriteLine(
                        VProducto[i].codigo + "*" +
                        VProducto[i].nombre + "*" +
                        VProducto[i].precio + "*" +
                        VProducto[i].categoria + "*" +
                        VProducto[i].stock + "*" +
                        VProducto[i].rutaImagen);
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
                if (datos.Length == 6)
                {
                    vis++;
                    VProducto[vis].codigo = int.Parse(datos[0].Trim());
                    VProducto[vis].nombre = datos[1].Trim();
                    VProducto[vis].precio = double.Parse(datos[2].Trim());
                    VProducto[vis].categoria = datos[3].Trim();
                    VProducto[vis].stock = int.Parse(datos[4].Trim());
                    VProducto[vis].rutaImagen = datos[5].Trim();
                }
            }
        }
        public static void CrearCarpetaImagenes()
        {
            if (!Directory.Exists(carpetaImagenes))
                Directory.CreateDirectory(carpetaImagenes);
        }
        public static string CopiarImagen(string rutaOrigen, int numeroProducto)
        {
            string extension = Path.GetExtension(rutaOrigen);
            string nombreArchivo = "prod_" + numeroProducto + extension;
            string rutaDestino = Path.Combine(carpetaImagenes, nombreArchivo);
            File.Copy(rutaOrigen, rutaDestino, true);
            return nombreArchivo;
        }
    }
}
