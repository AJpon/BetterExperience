using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using Newtonsoft.Json.Linq;
using NvNodeApi.Core;
using NvNodeApi.Api;
using NvNodeApi.Interfaces;

namespace NvNodeApi
{
    public class NvNodeApiWrapper
    {
        public uint ServerPort { get => Client.ServerPort; private set => Client.ServerPort = value; }
        public string SecurityToken { get => Client.SecurityToken; private set => Client.SecurityToken = value; }
        /// <summary>
        /// Map name of the memory map file that holds the server settings
        /// </summary>
        private const string SERVER_CONFIG_MAP_NAME = "{8BA1E16C-FC54-4595-9782-E370A5FBE8DA}";

        // Apis
        public INvShadowPlayAPI ShadowPlay;

        public NvNodeApiWrapper()
        {
            Init();
            ShadowPlay = new NvShadowPlayAPI();
        }

        private void Init()
        {
            if (IsNvNodeServiceRunning())
            {
                GetServerInfo();
                return;
            }
            else
            {
                throw new InvalidOperationException("NvNode service is not running");
            }
        }

        // Check the status of NVIDIA Web Helper running
        public static bool IsNvNodeServiceRunning()
        {
            return (Process.GetProcessesByName("NVIDIA Web Helper").Length > 0);
        }

        // Get NvNode server settings
        // Get the server port and security token from the memory map file
        private void GetServerInfo()
        {
            try
            {
                var mmf = MemoryMappedFile.OpenExisting(SERVER_CONFIG_MAP_NAME);
                using (var stream = mmf.CreateViewStream())
                using (var reader = new StreamReader(stream))
                {
                    // Debug.WriteLine("Reading server settings from memory map file");
                    var data = reader.ReadToEnd();
                    // Debug.WriteLine(data);
                    // Debug.Write("\n");
                    JObject config = JObject.Parse(data);
                    ServerPort = (uint)config["port"];
                    SecurityToken = (string)config["secret"];
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
