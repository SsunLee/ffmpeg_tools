using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace ffmpeg_tools
{
    class ffmpeg_functions
    {

        public void main_sub()
        {

        }

        public string RunExecute(string strKey, string file)
        {
            ProcessStartInfo pri = new ProcessStartInfo();
            Process pro = new Process();

            pri.FileName = @"cmd.exe";
            pri.CreateNoWindow = true;
            pri.UseShellExecute = false;

            pri.RedirectStandardInput = true;                //표준 출력을 리다이렉트
            pri.RedirectStandardOutput = true;
            pri.RedirectStandardError = true;

            pro.StartInfo = pri;
            pro.Start();   //어플리케이션 실

            pro.StandardInput.Write(strKey + Environment.NewLine);
            pro.StandardInput.Close();

            System.IO.StreamReader sr = pro.StandardOutput;

            if (main.f.txtLog.InvokeRequired)
            {
                main.f.txtLog.Invoke(new MethodInvoker(delegate () { main.f.txtLog.AppendText($"{Environment.NewLine}{file} : 시작"); }));
            }
            else
            {
                main.f.txtLog.AppendText($"{Environment.NewLine}{file : 시작}");
            }

            bool blError = false;
            string resultValue = String.Empty;
            while ((resultValue = pro.StandardError.ReadLine()) != null)
            {
                Debug.Print(resultValue);
                if (resultValue.Contains("403"))
                {
                    blError = true;
                    break;
                }


            }

            pro.WaitForExit();


            pro.Close();
            if (main.f.txtLog.InvokeRequired)
            {
                if (blError)
                {
                    main.f.txtLog.Invoke(new MethodInvoker(delegate () { main.f.txtLog.AppendText($"{Environment.NewLine}{file} : 다운로드 실패"); }));
                }
                else
                {
                    main.f.txtLog.Invoke(new MethodInvoker(delegate () { main.f.txtLog.AppendText($"{Environment.NewLine}{file} : 다운로드 완료"); }));
                }
                
            }
            else
            {
                if (blError)
                {
                    main.f.txtLog.AppendText($"{Environment.NewLine}{file} : 다운로드 실패");
                }
                else
                {
                    main.f.txtLog.AppendText($"{Environment.NewLine}{file} : 다운로드 완료");
                }
            }

            return resultValue == "" ? "" : resultValue;

        }

        public string GetResourceFileName()
        {
            String strAppPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            String strFilePath = Path.Combine(strAppPath, "Resources");
            String strFullFilename = Path.Combine(strFilePath, "ffmpeg.exe"); // Resource에 담겨진 allpair의 경로를 exe 에서 바로 가져오기 위해 path를 가져옴..
            Debug.Print(strFullFilename);

            return strFullFilename == "" ? "" : strFullFilename;

        }



        public void KillAllFFMPEG()
        {
            Process killFfmpeg = new Process();
            ProcessStartInfo taskkillStartInfo = new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/F /IM ffmpeg.exe",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            killFfmpeg.StartInfo = taskkillStartInfo;
            killFfmpeg.Start();
        }


    }


}
