﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace qplwork
{
    class DoubleParam : Param
    {
        private TextBox textBox = new TextBox();

        public override int DrawParam(int y)
        {
            int height = Program.form.CreateLabel(9, y, _paramShowedName, true, 11F);
            height += Program.form.CreateLabel(9, y + height, _paramComments, false, 8F);

            textBox = Program.form.CreateTextBox(9 + Portfel.dx, y, _paramValue, BoxTextChanged, new KeyPressEventHandler(KeyPressed));
            return height;
        }

        public override string GetNewValue()
        {
            return textBox.Text;
        }

        public void KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == ' ' || e.KeyChar == ',');
        }

        private void BoxTextChanged(object sender, EventArgs e)
        {

            if (!Program.form.Text.Contains("НЕ СОХРАНЕНО"))
                Program.form.Text = Program.form.Text + " - *НЕ СОХРАНЕНО!";
        }
    }
}
