

## ffmpeg Library
---
<br>

### **π μ¬μ©λ²**
- TextBoxμ urlμ λ£κ³  execute ν©λλ€
<br><br><br><br>
### **π κΈ°λ₯**
```csharp
public string RunExecute(string strKey, string file)
{
    ProcessStartInfo pri = new ProcessStartInfo();
    Process pro = new Process();

    pri.FileName = @"cmd.exe";
    pri.CreateNoWindow = true;
    pri.UseShellExecute = false;

    pri.RedirectStandardInput = true;                //νμ€ μΆλ ₯μ λ¦¬λ€μ΄λ νΈ
    pri.RedirectStandardOutput = true;
    pri.RedirectStandardError = true;

    pro.StartInfo = pri;
    pro.Start();   //μ΄νλ¦¬μΌμ΄μ μ€

    pro.StandardInput.Write(strKey + Environment.NewLine);
    pro.StandardInput.Close();

    System.IO.StreamReader sr = pro.StandardOutput;

    if (main.f.txtLog.InvokeRequired){
        main.f.txtLog.Invoke(new MethodInvoker(delegate () { main.f.txtLog.AppendText($"{Environment.NewLine}{file} : μμ"); }));
    }
    else{
        main.f.txtLog.AppendText($"{Environment.NewLine}{file : μμ}");
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
            main.f.txtLog.Invoke(new MethodInvoker(delegate () { main.f.txtLog.AppendText($"{Environment.NewLine}{file} : λ€μ΄λ‘λ μ€ν¨"); }));
        }
        else{
            main.f.txtLog.Invoke(new MethodInvoker(delegate () { main.f.txtLog.AppendText($"{Environment.NewLine}{file} : λ€μ΄λ‘λ μλ£"); }));
        }
    }
    else{
        if (blError){
            main.f.txtLog.AppendText($"{Environment.NewLine}{file} : λ€μ΄λ‘λ μ€ν¨");
        }
        else{
            main.f.txtLog.AppendText($"{Environment.NewLine}{file} : λ€μ΄λ‘λ μλ£");
        }
    }
    return resultValue == "" ? "" : resultValue;
}

```
