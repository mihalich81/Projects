using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace qplwork
{
    class BooleanParam : Param
    {
        ComboBox comboBox = new ComboBox();

        public override int DrawParam(int y)
        {
            int height = Program.form.CreateLabel(9, y, _paramShowedName, true, 11F);
            height += Program.form.CreateLabel(9, y + height, _paramComments, false, 8F);

            comboBox = Program.form.CreateComboBox(9 + Portfel.dx, y, new object[] { "Да", "Нет" }, _paramValue == "Да" ? 0 : 1, new EventHandler(BoxTextChanged));
            return height;
        }

        public override string GetNewValue()
        {
            return comboBox.Text;
        }

        private void BoxTextChanged(object sender, EventArgs e)
        {
            if (!Program.form.Text.Contains("НЕ СОХРАНЕНО"))
                Program.form.Text = Program.form.Text + " - *НЕ СОХРАНЕНО!";

        }
    }
}
