using System;
using System.Windows.Forms;

namespace Editor
{
    public partial class Form2 : Form
    {
        public Form2(int length)
        {
            InitializeComponent();
            numericUpDown1.Value = length;
            Length = length;
        }

        public int Length { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            Length = (int) numericUpDown1.Value;
        }
    }
}