using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App1
{
    public partial class Form1 : Form
    {
        public static string RFNo;
        public Form1()
        {
            InitializeComponent();
        }


        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = AYJCardLib.AYJReadCardLib.CardChk();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = AYJCardLib.AYJReadCardLib.CardChk();
            RFNo = textBox1.Text;
        }
    }
}
