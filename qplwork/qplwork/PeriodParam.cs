using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace qplwork
{
    class PeriodParam : Param
    {
       private static string[] values = {"1 минута = M1", 
                "2 минуты = M2", 
                "3 минуты = M3", 
                "4 минуты = M4", 
                "5 минут =  M5",
                "6 минут = M6", 
                "10 минут = M10", 
                "15 минут = M15", 
                "20 минут = M20", 
                "30 минут = M30", 
                "60 минут = H1", 
                "2 часа = H2", 
                "4 часа = H4", 
                "Дневной = D1", 
                "Недельный = W1", 
                "Месячный = MN1"};

        private ComboBox comboBox = new ComboBox();

        public override int DrawParam(int y)
        {
            int height = Program.form.CreateLabel(9, y, _paramShowedName, true, 11F);
            height += Program.form.CreateLabel(9, y + height, _paramComments, false, 8F);

            int i;
            for (i = 0; i < values.Length; i++)
                if (values[i].Split('=')[1].Trim() == _paramValue)
                    break;

            comboBox = Program.form.CreateComboBox(9 + Portfel.dx, y, values, i, new EventHandler(BoxTextChanged));
            return height;
        }

        public override string GetNewValue()
        {
            return comboBox.Text.Split('=')[1].Trim();
        }

        private void BoxTextChanged(object sender, EventArgs e)
        {
            if (!Program.form.Text.Contains("НЕ СОХРАНЕНО"))
                Program.form.Text = Program.form.Text + " - *НЕ СОХРАНЕНО!";

        }
    }
}
