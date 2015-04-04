using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace qplwork
{
    class TimeParam : Param
    {
        private MaskedTextBox maskedTextBox = new MaskedTextBox();

        public override int DrawParam(int y)
        {
            int height = Program.form.CreateLabel(9, y, _paramShowedName, true, 11F);
            height += Program.form.CreateLabel(9, y + height, _paramComments, false, 8F);

            maskedTextBox = Program.form.CreateMaskedTextBox(9 + Portfel.dx, y, _paramValue);
            return height;
        }



        public override string GetNewValue()
        {
            return maskedTextBox.Text.Replace(":","");
        }
    }
}
