

## ffmpeg Library
---
<br>

### **😀 사용법**
- TextBox에 url을 넣고 execute 합니다
<br><br><br><br>
### **😀 기능**
```csharp
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

    if (main.f.txtLog.InvokeRequired){
        main.f.txtLog.Invoke(new MethodInvoker(delegate () { main.f.txtLog.AppendText($"{Environment.NewLine}{file} : 시작"); }));
    }
    else{
        main.f.txtLog.AppendText($"{Environment.NewLine}{file : 시작}");
    }

    bool blError = false;
    string resultValue = String.Empty;
    while ((resultValue = pro.StandardError.ReadLine()) != null){
        Debug.Print(resultValue);
        if (resultValue.Contains("403")){
            blError = true;
            break;
        }
    }
    pro.WaitForExit();
    pro.Close();
    if (main.f.txtLog.InvokeRequired){
        if (blError){
            main.f.txtLog.Invoke(new MethodInvoker(delegate () { main.f.txtLog.AppendText($"{Environment.NewLine}{file} : 다운로드 실패"); }));
        }
        else{
            main.f.txtLog.Invoke(new MethodInvoker(delegate () { main.f.txtLog.AppendText($"{Environment.NewLine}{file} : 다운로드 완료"); }));
        }
    }
    else{
        if (blError){
            main.f.txtLog.AppendText($"{Environment.NewLine}{file} : 다운로드 실패");
        }
        else{
            main.f.txtLog.AppendText($"{Environment.NewLine}{file} : 다운로드 완료");
        }
    }
    return resultValue == "" ? "" : resultValue;
}

```
