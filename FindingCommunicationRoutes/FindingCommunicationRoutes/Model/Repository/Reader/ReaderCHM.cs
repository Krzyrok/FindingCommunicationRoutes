using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Class used for load chm file, decompile it and localize the index.html file.
    /// </summary>
    public class ReaderCHM
    {
        #region private fields
        /// <summary>
        /// The chm file path
        /// </summary>
        private String _chmFilePath;

        /// <summary>
        /// The output path for decompile.
        /// </summary>
        private String _outputPath;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderCHM"/> class.
        /// </summary>
        /// <param name="CHMfilePath">The CHM file path.</param>
        /// <param name="outputPathForStoringHTML">The output path for storing HTML.</param>
        public ReaderCHM(String CHMfilePath, String outputPathForStoringHTML)
        {
            if (CHMfilePath.EndsWith(".chm"))
            {
                _chmFilePath = CHMfilePath;
                _outputPath = outputPathForStoringHTML;
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Decompiles loaded chm file. To do so uses windows application hh.exe
        /// </summary>
        /// <returns>Exit Code of hh.exe</returns>
        public int Decompile()
        {
            Process proc = new Process();

            try
            {
                proc.StartInfo.FileName = "hh.exe";
                proc.StartInfo.Arguments = "-decompile " + _outputPath + " " + _chmFilePath;
                proc.Start();
                proc.WaitForExit();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return proc.ExitCode;
        }

        /// <summary>
        /// Gets the index file from output path.
        /// </summary>
        /// <returns>Index.html paths</returns>
        public string[] GetIndexFileFromOutputPath()
        {
            if(_outputPath != null && !_outputPath.Equals(""))
                return Directory.GetFiles(_outputPath,"index.html");
            else
                return new string[0];
        }
        #endregion
    }
}
