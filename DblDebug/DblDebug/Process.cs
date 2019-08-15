using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DblDebug
{
    public class ShellProcess
    {
        public List<string> Output { get; } = new List<string>();
        public ShellProcess()
        {
        }

        public async Task<List<string>>Run(string command, string arguments, string workingDirectory)
        {
            Process process = new Process();
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            //* Set your output and error (asynchronous) handlers
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            return Output;
        }

        protected void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            //* Do your stuff with the output (write to console/log/StringBuilder)
            if(default(string) != outLine.Data)
            {
                Output.AddRange(outLine.Data.Split('\n'));
            }
        }
    }
}
