using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace qplwork
{
    class PathParam : Param
    {
        private static FolderBrowserDialog path = new FolderBrowserDialog();
        private TextBox textBox;

        public override int DrawParam(int y)
        {
            int height = Program.form.CreateLabel(9, y, _paramShowedName, true, 11F);
            height += Program.form.CreateLabel(9, y + height, _paramComments, false, 8F);

            textBox = Program.form.CreateTextBox(9 + Portfel.dx, y, _paramValue, new EventHandler(BoxTextChanged), new KeyPressEventHandler(KeyPressed));
            Program.form.CreateButton(9 + Portfel.dx + 151, y, "...", new EventHandler(Click));
            return height;
        }

        public override string GetNewValue()
        {
            return textBox.Text;
        }

        private void Click(object sender, EventArgs e)
        {
            int index = Program.form.openFileDialog1.FileName.LastIndexOf("\\");
            path.SelectedPath = Program.form.openFileDialog1.FileName.Remove(index, Program.form.openFileDialog1.FileName.Length - index);
            if (path.ShowDialog() == DialogResult.OK)
                textBox.Text = path.SelectedPath;
        }

        public void KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == ' ');
        }

        private void BoxTextChanged(object sender, EventArgs e)
        {
            if (!Program.form.Text.Contains("НЕ СОХРАНЕНО"))
                Program.form.Text = Program.form.Text + " - *НЕ СОХРАНЕНО!";

        }
       
    }
}
