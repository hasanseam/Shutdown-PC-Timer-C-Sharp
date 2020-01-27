using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Shutdown_PC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Shutdown()
        {


            if (comboBox1.Text != "" && comboBox2.Text != "")
            {
                int Hours = Convert.ToInt32(comboBox1.Text);
                int Minit = Convert.ToInt32(comboBox2.Text);
                int Second = Hours * 3600 + Minit * 60;
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardError = true;
                startInfo.RedirectStandardOutput = true; //If I dont have this as true, cmd flashes open and closes
                Process p = Process.Start(startInfo);
                p.StandardInput.WriteLine("shutdown -s -t " + Second);
                p.StandardInput.Flush();
                p.StandardInput.Close();
                string error = "";
                using (StreamReader streamReader = p.StandardError)
                {
                    error += streamReader.ReadToEnd();
                }
                p.Close();
                if (error != "")
                {
                    MessageBox.Show("Hey You have already started a paln. If You want to change your plan cancel first then retry");
                }
                else
                {
                    MessageBox.Show("Successfull!! If you want to abort your plan click cancel button");
                }
                comboBox1.ResetText();
                comboBox2.ResetText();
            }
            else
            {
                string er = "";
                if (comboBox1.Text == "")
                {
                    er += "Set Hours";
                }
                if (comboBox2.Text == "")
                {
                    if (er == "")
                    {
                        er += "Set Miniutes";
                    }
                    else
                    {
                        er += " And Miniutes";
                    }
                }

                if (er != "")
                {
                    MessageBox.Show(er);
                }
            }
        }

        private void Abort()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true; //If I dont have this as true, cmd flashes open and closes
            Process p = Process.Start(startInfo);
            p.StandardInput.WriteLine("shutdown -a");
            p.StandardInput.Flush();
            p.StandardInput.Close();
            string error = "";
            using (StreamReader streamReader = p.StandardError)
            {
                error += streamReader.ReadToEnd();
            }
            p.Close();
            if (error != "")
            {
                MessageBox.Show("Sorry No shutdown was in progress.");
            }
            else
            {
                MessageBox.Show("Succesfully Aborted!!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shutdown();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Abort();
        }

    }
}
