using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Big_Happy
{
    public static class ModalHelper
    {
        public static DialogResult ShowCentered(Control parent, Form modal)
        {
            modal.StartPosition = FormStartPosition.Manual;
            modal.ShowInTaskbar = false;
            Point center = parent.PointToScreen(new Point(parent.Width / 2, parent.Height / 2));
            modal.Location = new Point(center.X - modal.Width / 2, center.Y - modal.Height / 2);
            return modal.ShowDialog(parent.FindForm());
        }
        public static void OpenChildForm(Panel panel,Form childForm)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel.Controls.Add(childForm);
            panel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
