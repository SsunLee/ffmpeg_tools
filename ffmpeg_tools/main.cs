using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace ffmpeg_tools
{
    
    public partial class main : Form
    {
        private ffmpeg_functions ff;

        public static main f;

        public main()
        {
            InitializeComponent();
            f = this;
            MaximizeBox = false;
            initEvent();

            ff = new ffmpeg_functions();


        }

        private void initEvent()
        {
            this.btnConvert.Click += new EventHandler(this.executeConvert);

        }

        private void executeConvert(object sender, EventArgs e)
        {


            if (txtURL.Text == "")
            {
                MessageBox.Show("URL을 넣어주세요");
            }
            else
            {

                Thread t = new Thread(() => GoConvert(txtURL.Text.ToString()));
                t.Start();


    
            }

        }

        /// <summary>
        /// 컨버트
        /// </summary>
        /// <param name="txt"> URL</param>
        private void GoConvert(string txt)
        {
            string time = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
            time += ".mp4";
            string ffmepgPath = ff.GetResourceFileName();

            string msg;

            msg = $"{ffmepgPath} -i \"{txt}\" -codec copy {time}";


            System.Diagnostics.Debug.Print(msg);

            string result = ff.RunExecute(msg, time);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            ff.KillAllFFMPEG();
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            ff.KillAllFFMPEG();
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            String strAppPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            System.Diagnostics.Debug.Print(strAppPath);
            System.Diagnostics.Process.Start(strAppPath);
        }
    }
}
