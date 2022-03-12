using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NvNodeApi.Core;
using NvNodeApi.Interfaces;

namespace NvNodeApi.Api
{
    public class NvShadowPlayAPI : INvShadowPlayAPI
    {
        public NvShadowPlayAPI() => ((IApi)this).Init();

        bool IApi.IsSupportedCheck()
        {
            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////
        // Instant Replay
        public async Task EnableIr(bool enable)
        {
            if (await GetSpRunning())
            {
                var req = new JObject
                {
                    ["status"] = enable
                };
                await Client.Post("/ShadowPlay/v.1.0/InstantReplay/Enable", req);
            }
            else
            {
                throw new InvalidOperationException("ShadowPlay is not running.");
            }
        }

        public async Task<bool> GetIrEnabled()
        {
            var response = await Client.Get("/ShadowPlay/v.1.0/InstantReplay/Enable");
            string resStr = await response.Content.ReadAsStringAsync();
            JObject res = JObject.Parse(resStr);
            return (bool)res["status"];
        }

        public async Task<bool> GetIrRunning()
        {
            if (await GetSpRunning())
            {
                var response = await Client.Get("/ShadowPlay/v.1.0/InstantReplay/Running");
                string resStr = await response.Content.ReadAsStringAsync();
                JObject res = JObject.Parse(resStr);
                return (bool)res["running"];
            }
            else
            {
                throw new InvalidOperationException("ShadowPlay is not running.");
            }
        }

        public async Task<InstantReplaySettings> GetIrSettings()
        {
            if (await GetSpRunning())
            {
                var response = await Client.Get("/ShadowPlay/v.1.0/InstantReplay/Settings");
                string resStr = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<InstantReplaySettings>(resStr);
            }
            else
            {
                throw new InvalidOperationException("ShadowPlay is not running.");
            }
        }

        public async Task SetIrSettings(InstantReplaySettings settings)
        {
            if (await GetSpRunning())
            {
                var req = JObject.FromObject(settings);
                await Client.Post("/ShadowPlay/v.1.0/InstantReplay/Settings", req);
            }
            else
            {
                throw new InvalidOperationException("ShadowPlay is not running.");
            }
        }

        public async Task SaveIr()
        {
            if (await GetIrRunning())
            {
                var req = new JObject { };
                await Client.Post("/ShadowPlay/v.1.0/InstantReplay/Save", req);
            }
            else
            {
                throw new InvalidOperationException("Instant Replay is not running.");
            }
        }

        public async Task<float> GetBufferLength()
        {
            if (await GetIrRunning())
            {
                var response = await Client.Get("/ShadowPlay/v.1.0/InstantReplay/BufferLength");
                string resStr = await response.Content.ReadAsStringAsync();
                JObject res = JObject.Parse(resStr);
                return (float)res["lengthSeconds"];
            }
            else
            {
                throw new InvalidOperationException("Instant Replay is not running.");
            }
        }

        //////////////////////////////////////////////////////////////////////////////////
        // Manual Record
        public async Task EnableRecord(bool enable)
        {
            if (await GetSpRunning())
            {
                var req = new JObject
                {
                    ["status"] = enable
                };
                await Client.Post("/ShadowPlay/v.1.0/Record/Enable", req);
            }
            else
            {
                throw new InvalidOperationException("ShadowPlay is not running.");
            }
        }

        public async Task<bool> GetRecordEnabled()
        {
            var response = await Client.Get("/ShadowPlay/v.1.0/Record/Enable");
            string resStr = await response.Content.ReadAsStringAsync();
            JObject res = JObject.Parse(resStr);
            return (bool)res["status"];
        }

        public async Task<bool> GetRecordRunning()
        {
            if (await GetSpRunning())
            {
                var response = await Client.Get("/ShadowPlay/v.1.0/Record/Running");
                string resStr = await response.Content.ReadAsStringAsync();
                JObject res = JObject.Parse(resStr);
                return (bool)res["running"];
            }
            else
            {
                return false;
            }
        }

        public async Task<ManualRecordSettings> GetRecordSettings()
        {
            if (await GetSpRunning())
            {
                var response = await Client.Get("/ShadowPlay/v.1.0/Record/Settings");
                string resStr = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ManualRecordSettings>(resStr);
            }
            else
            {
                throw new InvalidOperationException("ShadowPlay is not running.");
            }
        }

        public async Task SetRecordSettings(ManualRecordSettings settings)
        {
            if (await GetSpRunning())
            {
                var req = JObject.FromObject(settings);
                await Client.Post("/ShadowPlay/v.1.0/Record/Settings", req);
            }
            else
            {
                throw new InvalidOperationException("ShadowPlay is not running.");
            }
        }

        //////////////////////////////////////////////////////////////////////////////////
        // Broadcast

        //////////////////////////////////////////////////////////////////////////////////
        // Basic OSC handling
        public async Task OpenOsc(bool open)
        {
            if (await GetSpRunning())
            {
                var req = new JObject
                {
                    ["open"] = open
                };
                await Client.Post("/ShadowPlay/v.1.0/OpenOsc", req);
            }
            else
            {
                throw new InvalidOperationException("ShadowPlay is not running.");
            }
        }

        public async Task OpenOscPreferences()
        {
            if (await GetSpRunning())
            {
                var req = new JObject
                {
                    ["open"] = true
                };
                await Client.Post("/ShadowPlay/v.1.0/OpenOscPreferences", req);
            }
            else
            {
                throw new InvalidOperationException("ShadowPlay is not running.");
            }
        }

        //////////////////////////////////////////////////////////////////////////////////
        // Basic ShadowPlay handling
        public async Task LaunchSp(bool launch)
        {
            var req = new JObject
            {
                ["launch"] = launch
            };
            await Client.Post("/ShadowPlay/v.1.0/Launch", req);
        }

        public async Task<bool> GetSpRunning()
        {
            var response = await Client.Get("/ShadowPlay/v.1.0/Launch");
            string resStr = await response.Content.ReadAsStringAsync();
            JObject res = JObject.Parse(resStr);
            return (bool)res["launch"];
        }

        public async Task<bool> GetHdrActive()
        {
            if (await GetSpRunning())
            {
                var response = await Client.Get("/ShadowPlay/v.1.0/GetHDRState");
                string resStr = await response.Content.ReadAsStringAsync();
                JObject res = JObject.Parse(resStr);
                return (bool)res["active"];
            }
            else
            {
                throw new InvalidOperationException("ShadowPlay is not running.");
            }
        }

        // public async Task<bool> GetSpSupported(string lang)
        // {
        //     var req = new JObject
        //     {
        //         ["language"] = lang
        //     };
        //     var response = await Client.Post("/ShadowPlay/v.1.0/GetSupported", req);
        //     string resStr = await response.Content.ReadAsStringAsync();
        //     JObject res = JObject.Parse(resStr);
        //     return (bool)res["supported"];
        // }

        //////////////////////////////////////////////////////////////////////////////////
        // Video-related interfaces
        public async Task TrimVideo(string srcVideo, float start, float end, string dstVideo)
        {
            var req = new JObject
            {
                ["input"] = srcVideo,
                ["output"] = dstVideo,
                ["headTrimMs"] = start * 1000f,
                ["lengthMs"] = (end - start) * 1000f
            };
            await Client.Post("/ShadowPlay/v.1.0/Video/Trim", req);
        }

        /////////////////////////////////////////////////////////////////////////////////
        // Desktop Capture handling
        public async Task EnableDesktopCapture(bool enable)
        {
            var req = new JObject
            {
                ["enable"] = enable
            };
            await Client.Post("/ShadowPlay/v.1.0/DesktopCapture/Enable", req);
        }

        public async Task<bool> GetDesktopCaptureEnabled()
        {
            var response = await Client.Get("/ShadowPlay/v.1.0/DesktopCapture/Enable");
            string resStr = await response.Content.ReadAsStringAsync();
            JObject res = JObject.Parse(resStr);
            return (bool)res["enable"];
        }

        public async Task<bool> GetDesktopCaptureSupported()
        {
            var response = await Client.Get("/ShadowPlay/v.1.0/DesktopCapture/Support");
            string resStr = await response.Content.ReadAsStringAsync();
            JObject res = JObject.Parse(resStr);
            return (bool)res["support"];
        }

        public async Task<bool> GetDesktopCaptureSupportedReasons()
        {
            var response = await Client.Get("/ShadowPlay/v.1.0/DesktopCapture/Support/Reason");
            string resStr = await response.Content.ReadAsStringAsync();
            JObject res = JObject.Parse(resStr);
            return (bool)res["support"];
        }
    }

    public class ManualRecordSettings
    {
        public string quality { get; set; }
        public string resolution { get; set; }
        public uint framerate { get; set; }
        public uint bitrateBps { get; set; }
    }
    public class InstantReplaySettings : ManualRecordSettings
    {
        public uint replayLengthSeconds { get; set; }
    }
}
