using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace qplwork
{
    class CurrentPath : Param
    {
        public override int DrawParam(int y)
        {
            return 0;
        }

        public override string GetNewValue()
        {
            int index = Program.form.openFileDialog1.FileName.LastIndexOf("\\");
            return Program.form.openFileDialog1.FileName.Remove(index, Program.form.openFileDialog1.FileName.Length - index);
        }
    }
}
