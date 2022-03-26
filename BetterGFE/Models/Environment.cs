using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterGFE.Models
{
    public class Environment
    {
        public static string ConfigurationPath = Path.Combine(ExecutableDirectory, "config.bin");
        /// <summary>
        /// executable file path
        /// </summary>
        public static string ExecutablePath 
        { 
            get 
            {
                return System.Reflection.Assembly.GetExecutingAssembly().Location;
            }
        }
        /// <summary>
        /// executable file directory
        /// </summary>
        public static string ExecutableDirectory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(ExecutablePath);
            }
        }
    }
}
