using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NvNodeApi.Api;

namespace NvNodeApi.Interfaces
{
    public interface INvShadowPlayAPI : IApi
    {
        //////////////////////////////////////////////////////////////////////////////////
        // Instant Replay

        /// <summary>
        /// Enables/disables Instant Replay
        /// </summary>
        Task EnableIr(bool enable);

        /// <summary>
        /// Gets the enable/disable state of Instant Replay
        /// </summary>
        Task<bool> GetIrEnabled();

        /// <summary>
        /// Gets the running state of Instant Replay
        /// </summary>
        Task<bool> GetIrRunning();

        /// <summary>
        /// Gets the Instant Replay settings
        /// </summary>
        Task<InstantReplaySettings> GetIrSettings();

        /// <summary>
        /// Sets the Instant Replay settings
        /// </summary>
        Task SetIrSettings(InstantReplaySettings settings);

        /// <summary>
        /// Saves Instant Replay
        /// </summary>
        Task SaveIr();

        /// <summary>
        /// Gets the Instant Replay buffer length, in seconds.
        /// </summary>
        Task<float> GetBufferLength();

        //////////////////////////////////////////////////////////////////////////////////
        // Manual Record

        /// <summary>
        /// Enables/disables Manual Record
        /// </summary>
        Task EnableRecord(bool enable);

        /// <summary>
        /// Gets the enable/disable state of Manual Record
        /// </summary>
        Task<bool> GetRecordEnabled();

        /// <summary>
        /// Gets the running state of Manual Record
        /// </summary>
        Task<bool> GetRecordRunning();

        /// <summary>
        /// Gets the Manual Record settings
        /// </summary>
        Task<ManualRecordSettings> GetRecordSettings();

        /// <summary>
        /// Sets the Manual Record settings
        /// </summary>
        Task SetRecordSettings(ManualRecordSettings settings);

        //////////////////////////////////////////////////////////////////////////////////
        // Broadcast

        //////////////////////////////////////////////////////////////////////////////////
        // Basic OSC handling

        /// <summary>
        /// Open/closes OSC
        /// </summary>
        Task OpenOsc(bool open);

        /// <summary>
        /// Open OSC Preferences
        /// </summary>
        Task OpenOscPreferences();

        //////////////////////////////////////////////////////////////////////////////////
        // Basic ShadowPlay handling

        /// <summary>
        /// Launch ShadowPlay
        /// </summary>
        Task LaunchSp(bool launch);

        /// <summary>
        /// Gets ShadowPlay run state
        /// </summary>
        Task<bool> GetSpRunning();

        /// <summary>
        /// Gets HDR Active state
        /// </summary>
        Task<bool> GetHdrActive();

        // Gets ShadowPlay support state
        // bool GetSpSupported(string lang);

        //////////////////////////////////////////////////////////////////////////////////
        // Video-related interfaces

        /// <summary>
        /// Requests a video be trimmed
        /// </summary>
        Task TrimVideo(string srcVideo, float start, float end, string dstVideo);

        /////////////////////////////////////////////////////////////////////////////////
        // Desktop Capture handling

        /// <summary>
        /// Set Desktop Capture enable/disable
        /// </summary>
        Task EnableDesktopCapture(bool enable);

        /// <summary>
        /// Get if Desktop Capture is enabled/disabled
        /// </summary>
        Task<bool> GetDesktopCaptureEnabled();

        /// <summary>
        /// Gets if Desktop Capture is supported
        /// </summary>
        Task<bool> GetDesktopCaptureSupported();

        /// <summary>
        /// Gets the supported reasons for Desktop Capture. This is an extended version of GetDesktopCaptureSupported()
        /// </summary>
        Task<bool> GetDesktopCaptureSupportedReasons();
    }
}
