using BeecareDeploy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;

namespace BeecareDeploy.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class WebHookController:ControllerBase
{
    private readonly DeployConfig _config;
    public WebHookController(DeployConfig config)
    {
        _config = config;
    }
    [HttpPost]
    public IActionResult PullEvent(string content)
    {
            ExecuteCommand(@$"C:\Users\Luis Cruz\Desktop\prueba.bat -branch {_config.Repo.Branch} -repopath {_config.Repo.LocalPath} -pythonexe {_config.PythonExe} " );
        return Ok("Thank you!");
    }
    public void ExecuteCommand(string command)
    {
        int ExitCode;
        ProcessStartInfo ProcessInfo;
        System.Diagnostics.Process? Process = new ();

        ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
        ProcessInfo.CreateNoWindow = true;
        ProcessInfo.UseShellExecute = false;

        Process = System.Diagnostics.Process.Start(ProcessInfo);
        Process?.WaitForExit();
        
        ExitCode = Process.ExitCode;
        Process.Close();

    }

}