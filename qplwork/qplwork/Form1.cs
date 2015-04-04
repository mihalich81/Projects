using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace qplwork
{
    public partial class Form1 : Form
    {
        private Portfel pt;

        public Form1()
        {
            InitializeComponent();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                pt = new Portfel(openFileDialog1.FileName);
        }

        public void OpenFile(string fileName)
        {
            Program.form.openFileDialog1.FileName = fileName;
            pt = new Portfel(fileName);
           
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            if (pt == null || openFileDialog1 == null)
                MessageBox.Show("Ошибка, откройте файл.");
            else if (pt.Save(openFileDialog1.FileName)) { this.Text = "Настройка портфеля"; }
                //pt = pt; //вместо  MessageBox.Show("Файл успешно сохранен!");
            else
                MessageBox.Show("Ошибка при сохранении файла");
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            string name = "";
            try
            {
                if (pt != null)
                    name = pt.GetName();
            }
            catch (System.Exception ex)
            {
            	
            }
            if (name != "")
                saveFileDialog1.FileName = name + ".qpl";
            if (pt == null)
                MessageBox.Show("Ошибка, откройте файл.");
            else if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pt.Save(saveFileDialog1.FileName))
                {
                    this.Text = "Настройка портфеля";
                    MessageBox.Show("Файл успешно сохранен!");
                }
                else
                    MessageBox.Show("Ошибка при сохранении файла");
            }
        }

        public TableLayoutPanel DrowTable(int rows, float[] heights)
        {
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.Location = new Point(7, 13);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = rows;
            int size = 0;
            for (int i = 0; i < rows; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, heights[i] > 0 ? heights[i] - 2 : heights[i]));
                size += Convert.ToInt32(heights[i]);
            }
            tableLayoutPanel1.Size = new Size(735, size);
            tableLayoutPanel1.TabIndex = 0;
            panel.Controls.Add(tableLayoutPanel1);
            return tableLayoutPanel1;
        }

        public int CreateLabel(int x, int y, string text, bool bold, float size)
        {
            Label label = new Label();
            label.Location = new System.Drawing.Point(x, y);
            label.MaximumSize = new Size(470, 0);
            label.TabIndex = 0;
            label.Font = new Font("Times New Roman", size, bold ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            label.Text = text;
            label.Size = new Size(label.PreferredWidth, label.PreferredHeight);
       
            panel.Controls.Add(label);
            return label.PreferredHeight + 5;
        }

        public TextBox CreateTextBox(int x, int y, string text, EventHandler handler)
        {
            TextBox textBox1 = new TextBox();
            textBox1.Location = new System.Drawing.Point(x, y);
            textBox1.Size = new System.Drawing.Size(220, 20);
            textBox1.TabIndex = 0;
            textBox1.Text = text;
            if (handler != null)
                textBox1.TextChanged += handler;
            panel.Controls.Add(textBox1);
            return textBox1;
        }

        public ComboBox CreateComboBox(int x, int y, object[] values, int selected, EventHandler handler)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(values);
            comboBox.Location = new System.Drawing.Point(x, y);
            comboBox.Size = new System.Drawing.Size(250, 20);
            comboBox.TabIndex = 0;
            comboBox.SelectedIndex = selected;
            if (handler != null)
                comboBox.TextChanged += handler;
            panel.Controls.Add(comboBox);
            return comboBox;
        }

        public void CreateButton(int x, int y, string text, EventHandler handler)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new System.Drawing.Point(x, y);
            button.Size = new System.Drawing.Size(30, 20);
            button.TabIndex = 0;
            button.UseVisualStyleBackColor = true;
            if (handler != null)
                button.Click += handler;
            panel.Controls.Add(button);
        }

        public MaskedTextBox CreateMaskedTextBox(int x, int y, string value)
        {
            MaskedTextBox maskedTextBox = new MaskedTextBox();
            maskedTextBox.Location = new System.Drawing.Point(x, y);
            maskedTextBox.Mask = "00:00:00";
            maskedTextBox.PromptChar = ' ';
            maskedTextBox.Size = new System.Drawing.Size(220, 20);
            maskedTextBox.TabIndex = 0;
            maskedTextBox.Text = value;
            panel.Controls.Add(maskedTextBox);
            return maskedTextBox;
        }

        public void CreatePanel()
        {
            panel.AutoScroll = true;
            panel.Location = new Point(6, 9);
            panel.Size = new Size(770, 357);
            panel.TabIndex = 0;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.ResumeLayout(false);
            panel.PerformLayout();
            Controls.Add(panel);
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            (new MsgBoxHelp()).ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menuItem4_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            menuItem5_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            menuItem3_Click(sender, e);
        }

     

     
    }
}
