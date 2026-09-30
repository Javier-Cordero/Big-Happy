using Big_Happy.Loguin;
using Big_Happy.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Big_Happy.Loguin
{
    public static class Sesion
    {
        public static int CodigoUsuario = -1;
        public static string Nombre = "";
        public static string Rol = "";
        public static bool EstaActivo = false;
        static Random rnd = new Random();
        public static bool IniciarSesion(string usuario, string contraseña, out string mensaje)
        {
            mensaje = "";
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contraseña))
            {
                mensaje = "Debe ingresar usuario y contraseña.";
                return false;
            }
            DatosUsuarios.CargarDesdeArchivo();
            int indice = DatosUsuarios.ValidarLogin(usuario.Trim(), contraseña);
            if (indice < 0)
            {
                mensaje = "Usuario o contraseña incorrectos.";
                return false;
            }
            CodigoUsuario = DatosUsuarios.VUsuario[indice].codigo;
            Nombre = DatosUsuarios.VUsuario[indice].nombre;
            Rol = DatosUsuarios.VUsuario[indice].rol;
            EstaActivo = true;
            return true;
        }
        public static void CerrarSesion()
        {
            CodigoUsuario = -1;
            Nombre = "";
            Rol = "";
            EstaActivo = false;
        }
        public static bool EsAdministrador => Rol == "Administrador";
        public static bool EsEncargado => Rol == "Encargado";
        public static bool EsCajero => Rol == "Cajero";
        public static bool PuedeGestionarUsuarios => EsAdministrador;
        public static bool PuedeGestionarProductos => EsAdministrador || EsEncargado;
        public static bool PuedeVerInformacion => EsAdministrador || EsEncargado;
        public static bool PuedeEmitirTickets => EstaActivo;
        public static bool PuedeGestionarClientes => EstaActivo;
        public static bool PedirLogin()
        {
            using (var login = new FrmLogin()) return login.ShowDialog() == DialogResult.OK;
        }
        public static bool ExigirPermiso(bool permitido, string mensaje = null)
        {
            if (permitido) return true;
            MessageBox.Show(mensaje ?? "No tienes permisos para realizar esta acción.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        public static string GenerarCaptcha()
        {
            string caracteres = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            string codigo = "";
            for (int i = 0; i < 4; i++)
            {
                int indice = rnd.Next(0, caracteres.Length);
                codigo = codigo + caracteres[indice];
            }
            return codigo;
        }
    }
}
