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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Big_Happy.Loguin
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        private string codigoCaptcha = "";
        private void GenerarCaptcha()
        {
            const string caracteres = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var rnd = new Random();
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < 5; i++) sb.Append(caracteres[rnd.Next(caracteres.Length)]);
            codigoCaptcha = sb.ToString();
            Capchat.Text = codigoCaptcha;
            Capchat.Font = new Font("Consolas", 14, FontStyle.Bold | FontStyle.Italic);
            Capchat.ForeColor = Color.FromArgb(180, 23, 23);
            CopiarCapchat.Clear();
            CopiarCapchat.Focus();
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            Contraseña.UseSystemPasswordChar = true;
            GenerarCaptcha();
        }
        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            GenerarCaptcha();
        }
        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            if (!string.Equals(CopiarCapchat.Text.Trim(), codigoCaptcha, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("El CAPTCHA no coincide. Intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GenerarCaptcha();
                return;
            }
            if (Sesion.IniciarSesion(Usuario.Text, Contraseña.Text, out string msg))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Contraseña.Clear();
                GenerarCaptcha();
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {
            FrmUsuarioAdd FUL = new FrmUsuarioAdd();
            FUL.ShowDialog();
        }              
    }
}
