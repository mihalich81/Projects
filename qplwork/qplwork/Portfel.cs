using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace qplwork
{
    class Portfel
    {

        private static string text = "";

        private string PORTFOLIO_EX;
        private string DESCRIPTION;
        private string CLIENTS_LIST;
        private string FIRMS_LIST;

        private Param[] _params = new Param[100];

        private float[] heights = new float[100];

        private TextBox textBox1, textBox2, textBox3;

        public bool isSaved = true;

        private void BoxTextChanged(object sender, EventArgs e)
        {
            if (!Program.form.Text.Contains("НЕ СОХРАНЕНО"))
                Program.form.Text = Program.form.Text + " - *НЕ СОХРАНЕНО!";

        }

        public static int dx = 475;

        public static string tst = "";

        public Portfel(string fileName)
        {
            try
            {
                if (!text.Equals(""))
                    Program.form.panel.Dispose();
                Program.form.panel = new Panel();
                // Считываем файл
                FileStream fs = File.Open(fileName, FileMode.OpenOrCreate);
                Int32 len = (Int32)fs.Length;
                byte[] bytes = new byte[len];
                fs.Read(bytes, 0, len);
                fs.Close(); // Не забываем закрыть файл
                Encoding in_code = Encoding.GetEncoding(1251);
                text = in_code.GetString(bytes);

                // Считываем переменные в шапке
                PORTFOLIO_EX = ReadHeadValue(text, "PORTFOLIO_EX");
                DESCRIPTION = ReadHeadValue(text, "DESCRIPTION");
                CLIENTS_LIST = ReadHeadValue(text, "CLIENTS_LIST");
                FIRMS_LIST = ReadHeadValue(text, "FIRMS_LIST");

                // ебаный костыль для 'extern help
                string[] lines = text.Replace("extern help", "").Remove(0, text.Replace("extern help", "").IndexOf("\'extern")).Split('\n');
                int k = -1;
                string type;
                foreach (string line in lines)
                {
                    if (line.StartsWith("\'//"))
                        break;
                    else if (line.Contains("extern"))
                    {
                        k++;
                        type = line.Remove(0, 7).Trim();
                        if (type.Equals("name"))
                            _params[k] = new NameParam();
                        else if (type.Equals("string"))
                            _params[k] = new StringParam();
                        else if (type.Equals("bool"))
                            _params[k] = new BooleanParam();
                        else if (type.Equals("time"))
                            _params[k] = new TimeParam();
                        else if (type.Equals("period"))
                            _params[k] = new PeriodParam();
                        else if (type.Equals("double"))
                            _params[k] = new DoubleParam();
                        else if (type.Equals("int"))
                            _params[k] = new IntParam();
                        else if (type.Equals("path"))
                            _params[k] = new PathParam();
                        else if (type.Equals("current path"))
                            _params[k] = new CurrentPath();
                    }
                    else if (line.Contains("=") && !line.Trim().StartsWith("\'"))
                    {
                        _params[k].SetOldLine(line);
                        string[] vals = line.Split('=');
                        _params[k].SetName(vals[0].Trim());
                        vals = vals[1].Trim().Split('\'');
                        _params[k].SetValue(vals[0].Trim().Replace("\"", ""));
                        _params[k].SetShowedName(vals[1].Trim());
                    }
                    else if (line.Trim().StartsWith("\'"))
                    {
                        string[] vals = line.Split('\'');
                        _params[k].AddComments(vals[1] + "\n");
                    }
                }
                int y = DrawHeaderValues();
                int l = 3;
                for (int i = 0; i <= k; i++)
                {
                    if (_params[i] is NameParam)
                        continue;
                    heights[l] = _params[i].DrawParam(y);
                    y += (int)heights[l];
                    l++;
                }
                Program.form.DrowTable(k + 4, heights);
                Program.form.CreatePanel();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message +"\n"+ e.StackTrace);
                MessageBox.Show("Портфель не содержит параметров.");
                if (Program.form.panel != null)
                {
                    Program.form.panel.Dispose();
                    Program.form.panel = new Panel();
                }
            }
        }

        private static string ReadHeadValue(string text, string valueName)
        {
            string result;
            result = text.Remove(0, text.IndexOf(valueName) + valueName.Length + 1);
            int index = result.IndexOf(";");
            result = result.Remove(index, result.Length - index);
            return result;
        }

        private int DrawHeaderValues()
        {
            int X = 9;
            int Y = 17;
            heights[0] = Program.form.CreateLabel(X, Y, "Название портфеля", true, 11F);
            heights[0] += Program.form.CreateLabel(X, Y + 14, "Используется для идентификации портфеля в системе QUIK, а также при выводе сообщений.\n"
                + "При загрузке нескольких портфелей, этот параметр должен быть уникален для каждого портфеля. \n", false, 8F);
            textBox1 = Program.form.CreateTextBox(X + dx, Y, PORTFOLIO_EX, new EventHandler(BoxTextChanged));
            
            Y += (int)heights[0];
            heights[1] = Program.form.CreateLabel(X, Y, "Список кодов клиентов", true, 11F);
            heights[1] += Program.form.CreateLabel(X, Y + 14, "Значение «ALL_CLIENTS» соответствует выбору всех кодов клиентов.\n"
                + "Список задаётся через запятую. Значения по каждому счету \n"
                + "клиента формируют новую строку таблицы портфеля.", false, 8F);
            textBox2 = Program.form.CreateTextBox(X + dx, Y, CLIENTS_LIST, new EventHandler(BoxTextChanged));

            Y += (int)heights[1];
            heights[2] = Program.form.CreateLabel(X, Y, "Список идентификаторов фирм", true, 11F);
            heights[2] += Program.form.CreateLabel(X, Y + 14, "Значение «FIRM_ID» соответствует выбору всех идентификаторов фирм.\n"
                + "Список задаётся через запятую. ", false, 8F);
            textBox3 = Program.form.CreateTextBox(X + dx, Y, FIRMS_LIST, new EventHandler(BoxTextChanged));
            return Y + Convert.ToInt32(heights[2]);
        }

        public bool Save(string fileName)
        {
            if (PORTFOLIO_EX != textBox1.Text)
            {
             
                text = text.Replace("PORTFOLIO_EX " + PORTFOLIO_EX + ";", "PORTFOLIO_EX " + textBox1.Text + ";");
                PORTFOLIO_EX = textBox1.Text;
            }
            if (DESCRIPTION != textBox1.Text)
            {
                text = text.Replace("DESCRIPTION " + DESCRIPTION + ";", "DESCRIPTION " + textBox1.Text + ";");
                DESCRIPTION = textBox1.Text;
            }
            if (CLIENTS_LIST != textBox2.Text)
            {
                text = text.Replace("CLIENTS_LIST " + CLIENTS_LIST + ";", "CLIENTS_LIST " + textBox2.Text + ";");
                CLIENTS_LIST = textBox2.Text;
            }
            
            if (FIRMS_LIST != textBox3.Text)
            {
                text = text.Replace("FIRMS_LIST " + FIRMS_LIST + ";", "FIRMS_LIST " + textBox3.Text + ";");
                FIRMS_LIST = textBox3.Text;
            }

            for (int i = 0; i < 100; i++)
            {
                if (_params[i] == null)
                    break;
                else if (_params[i] is NameParam && _params[i].GetValue() != textBox1.Text) // костыль
                {
                    string oldLine = _params[i].GetOldLine();
                    string newLine = oldLine.Replace("\"" + _params[i].GetValue() + "\"", "\"" + textBox1.Text + "\"");
                    text = text.Replace(oldLine, newLine);
                    _params[i].SetValue(textBox1.Text);
                    _params[i].SetOldLine(newLine);
                }
                else if (_params[i].GetValue() != _params[i].GetNewValue())
                {
                    string oldLine = _params[i].GetOldLine();
                    string newLine = "";
                    
                    if (_params[i] is DoubleParam)
                    {
                        newLine = oldLine;
                        if (!string.IsNullOrEmpty(_params[i].GetValue().ToString()))
                            newLine = newLine.Replace(_params[i].GetValue(), "");

                        newLine = newLine.Replace("=", "= " + _params[i].GetNewValue());
                    }
                    else
                    {
                        if (!oldLine.Contains("Название портфеля"))
                            newLine = oldLine.Replace("\"" + _params[i].GetValue() + "\"", "\"" + _params[i].GetNewValue() + "\"");
                    }

                    if (!string.IsNullOrEmpty(newLine))
                    {
                        text = text.Replace(oldLine, newLine);
                        _params[i].SetValue(_params[i].GetNewValue());
                        _params[i].SetOldLine(newLine);
                    }
                }
            }
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fs = File.OpenWrite(fileName);
                byte[] bytes = Encoding.GetEncoding(1251).GetBytes(text);
                
                fs.Write(bytes, 0, bytes.Length);
                fs.Close(); // Не забываем закрыть файл
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private Param Find(string name)
        {
            for (int i = 0; i < 100; i++)
            {
                if (_params[i] == null)
                    return null;
                else if (_params[i].GetName() == name)
                    return _params[i];
            }
            return null;
        }

        public String GetName()
        {
            return PORTFOLIO_EX;
        }

    }
}
