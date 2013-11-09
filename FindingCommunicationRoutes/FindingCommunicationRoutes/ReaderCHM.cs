using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FindingCommunicationRoutes
{
    public class ReaderCHM
    {
        private String _filePath;

        private String _outputPath;

        public ReaderCHM(String filePathCHM, String outputPathForStoringHTML)
        {
            if (filePathCHM.EndsWith(".chm"))
            {
                _filePath = filePathCHM;
                _outputPath = outputPathForStoringHTML;
            }
        }

        public int Decompile()
        {
            Process proc = new Process();

            try
            {
                proc.StartInfo.FileName = "hh.exe";
                proc.StartInfo.Arguments = "-decompile " + _outputPath + " " + _filePath;
                proc.Start();
                proc.WaitForExit();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return proc.ExitCode;
        }

        public string[] GetIndexFileFromOutputPath()
        {
            if(_outputPath != null)
                return Directory.GetFiles(_outputPath,"index.html");
            else
                return new string[0];
        }
    }
}
