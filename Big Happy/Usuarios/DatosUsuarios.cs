using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Big_Happy.Usuarios
{
    public struct TDUsuario
    {
        public int codigo;
        public string nombre;
        public string CI;
        public string rol;
        public string turno;
        public string email;
        public int telefono;
        public string contraseña;
        public string rutaImagen;
    }
    public static class DatosUsuarios
    {
        public static TDUsuario[] VUsuario = new TDUsuario[50];
        public static int vis = -1;
        public static string carpetaImagenes = Path.Combine(Application.StartupPath, "Perfil");
        public static string rutaArchivo = Path.Combine(Application.StartupPath, "Usuario.txt");
        // Agregar
        public static bool Guardar(string nombre,string CI, string rol, string turno, string email, int telefono, string contraseña, string rutaFoto)
        {
            vis++;
            VUsuario[vis].codigo = vis + 1;
            VUsuario[vis].nombre = nombre;
            VUsuario[vis].CI = CI;
            VUsuario[vis].rol = rol;
            VUsuario[vis].turno = turno;
            VUsuario[vis].email = email;
            VUsuario[vis].telefono = telefono;
            VUsuario[vis].contraseña = contraseña;
            VUsuario[vis].rutaImagen = ProcesarFoto(nombre, rutaFoto);
            GuardarEnArchivo();
            return true;
        }
        // Editar               
        public static bool Editar(int indice, string nombre, string CI, string rol, string turno, string email, int telefono, string contraseña, string rutaFoto)
        {
            if (indice < 0 || indice > vis) return false;
            VUsuario[indice].nombre = nombre;
            VUsuario[indice].CI = CI;
            VUsuario[indice].rol = rol;
            VUsuario[indice].turno = turno;
            VUsuario[indice].email = email;
            VUsuario[indice].telefono = telefono;
            VUsuario[indice].contraseña = contraseña;
            string rutaFinal;
            if (!string.IsNullOrEmpty(rutaFoto) && File.Exists(rutaFoto)) rutaFinal = ProcesarFoto(nombre, rutaFoto);
            else rutaFinal = VUsuario[indice].rutaImagen;
            VUsuario[indice].rutaImagen = rutaFinal;
            GuardarEnArchivo();
            return true;
        }
        // Mostrar
        public static void Mostrar(DataGridView grid, string filtro = "")
        {
            grid.Rows.Clear();
            filtro = (filtro ?? "").Trim().ToLower();
            for (int i = 0; i <= vis; i++)
            {
                if (filtro != "" && !Coincide(VUsuario[i], filtro)) continue;
                Image img = null;
                if (!string.IsNullOrEmpty(VUsuario[i].rutaImagen) && File.Exists(VUsuario[i].rutaImagen))
                    using (var tmp = Image.FromFile(VUsuario[i].rutaImagen)) img = new Bitmap(tmp);
                int rowIndex = grid.Rows.Add(
                    VUsuario[i].codigo,
                    VUsuario[i].nombre,
                    VUsuario[i].CI,
                    VUsuario[i].rol,
                    VUsuario[i].turno,
                    VUsuario[i].email,
                    VUsuario[i].telefono,
                    VUsuario[i].contraseña,
                    img
                );
                grid.Rows[rowIndex].Tag = i;
            }
        }
        private static bool Coincide(TDUsuario u, string filtro)
        {
            return (u.nombre ?? "").ToLower().Contains(filtro)
                || (u.CI ?? "").ToLower().Contains(filtro)
                || (u.rol ?? "").ToLower().Contains(filtro)
                || (u.turno ?? "").ToLower().Contains(filtro)
                || (u.email ?? "").ToLower().Contains(filtro)
                || u.telefono.ToString().Contains(filtro);
        }
        // Guardar en archivo
        public static void GuardarEnArchivo()
        {
            using (StreamWriter writer = new StreamWriter(rutaArchivo))
            {
                for (int i = 0; i <= vis; i++)
                    writer.WriteLine(
                        VUsuario[i].codigo + "*" +
                        VUsuario[i].nombre + "*" +
                        VUsuario[i].CI + "*" +
                        VUsuario[i].rol + "*" +
                        VUsuario[i].turno + "*" +
                        VUsuario[i].email + "*" +
                        VUsuario[i].telefono + "*" +
                        VUsuario[i].contraseña + "*" +
                        VUsuario[i].rutaImagen);
            }
        }
        // Validar Usuario
        public static int ValidarLogin(string usuario, string contraseña)
        {
            for (int i = 0; i <= vis; i++)
            {
                bool coincideUsuario = VUsuario[i].nombre.Equals(usuario, StringComparison.OrdinalIgnoreCase) || VUsuario[i].email.Equals(usuario, StringComparison.OrdinalIgnoreCase);
                if (coincideUsuario && VUsuario[i].contraseña == contraseña) return i;
            }
            return -1;
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
                if (datos.Length == 9)
                {
                    vis++;
                    VUsuario[vis].codigo = int.Parse(datos[0].Trim());
                    VUsuario[vis].nombre = datos[1].Trim();
                    VUsuario[vis].CI = datos[2].Trim();
                    VUsuario[vis].rol = datos[3].Trim();
                    VUsuario[vis].turno = datos[4].Trim();
                    VUsuario[vis].email = datos[5].Trim();
                    VUsuario[vis].telefono = int.Parse(datos[6].Trim());
                    VUsuario[vis].contraseña = datos[7].Trim();
                    VUsuario[vis].rutaImagen = datos[8].Trim();
                }
            }
        }
        // Generar avatar con iniciales
        public static string GenerarAvatar(string nombre)
        {
            // Asegurar carpeta
            if (!Directory.Exists(carpetaImagenes))
                Directory.CreateDirectory(carpetaImagenes);
            // Extraer iniciales (máx. 2)
            string iniciales = "";
            string[] palabras = nombre.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < Math.Min(palabras.Length, 2); i++)
                iniciales += palabras[i][0].ToString().ToUpper();
            if (string.IsNullOrEmpty(iniciales)) iniciales = "?";
            // Generar nombre único
            string nombreArchivo = $"avatar_{Guid.NewGuid():N}.png";
            string ruta = Path.Combine(carpetaImagenes, nombreArchivo);
            // Dibujar
            int tamano = 150;
            using (Bitmap bmp = new Bitmap(tamano, tamano))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    g.Clear(Color.FromArgb(52, 152, 219)); // azul
                    using (Font fuente = new Font("Arial", 48, FontStyle.Bold))
                    using (SolidBrush pincel = new SolidBrush(Color.White))
                    {
                        SizeF tamTexto = g.MeasureString(iniciales, fuente);
                        float x = (tamano - tamTexto.Width) / 2;
                        float y = (tamano - tamTexto.Height) / 2;
                        g.DrawString(iniciales, fuente, pincel, x, y);
                    }
                }
                bmp.Save(ruta, System.Drawing.Imaging.ImageFormat.Png);
            }
            return ruta;
        }
        private static string ProcesarFoto(string nombre, string rutaFoto)
        {
            if (!Directory.Exists(carpetaImagenes))
                Directory.CreateDirectory(carpetaImagenes);
            // Si el usuario seleccionó una foto → copiarla
            if (!string.IsNullOrEmpty(rutaFoto) && File.Exists(rutaFoto))
            {
                string nombreArchivo = $"foto_{Guid.NewGuid():N}{Path.GetExtension(rutaFoto)}";
                string destino = Path.Combine(carpetaImagenes, nombreArchivo);
                File.Copy(rutaFoto, destino, true);
                return destino;
            }
            return GenerarAvatar(nombre);
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
