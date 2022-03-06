using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NvNodeApi
{
    internal interface INvShadowPlayAPI : IApi
    {
        //////////////////////////////////////////////////////////////////////////////////
        // Instant Replay

        // Enables/disables Instant Replay
        void EnableIr(bool enable);

        // Gets the enable/disable state of Instant Replay
        bool GetIrEnabled();

        // Gets the running state of Instant Replay
        bool GetIrRunning();

        // Gets the Instant Replay settings
        // InstantReplaySettings GetIrSettings();

        // Sets the Instant Replay settings
        // void SetIrSettings(InstantReplaySettings settings);

        // Saves Instant Replay
        void SaveIr();

        // Gets the Instant Replay buffer length, in seconds.
        float GetBufferLength();

        //////////////////////////////////////////////////////////////////////////////////
        // Manual Record

        // Enables/disables Manual Record
        void EnableRecord(bool enable);

        // Gets the enable/disable state of Manual Record
        bool GetRecordEnabled();

        // Gets the running state of Manual Record
        bool GetRecordRunning();

        // Gets the Manual Record settings
        // ManualRecordSettings GetRecordSettings();

        // Sets the Manual Record settings
        // void SetRecordSettings(ManualRecordSettings settings);

        //////////////////////////////////////////////////////////////////////////////////
        // Broadcast

        //////////////////////////////////////////////////////////////////////////////////
        // Basic OSC handling

        // Open/closes OSC
        void OpenOsc(bool open);

        // Open OSC Preferences
        void OpenOscPreferences();

        //////////////////////////////////////////////////////////////////////////////////
        // Basic ShadowPlay handling

        // Launch ShadowPlay
        void LaunchSp(bool launch);

        // Gets ShadowPlay run state
        bool GetSpRunning();

        // Gets HDR Active state
        bool GetHdrActive();

        // Gets ShadowPlay support state
        bool GetSpSupported();

        //////////////////////////////////////////////////////////////////////////////////
        // Video-related interfaces

        // Requests a video be trimmed
        void TrimVideo(string srcVideo, float start, float end, string dstVideo);

        /////////////////////////////////////////////////////////////////////////////////
        // Desktop Capture handling

        // Set Desktop Capture enable/disable
        void EnableDesktopCapture(bool enable);

        // Get if Desktop Capture is enabled/disabled
        bool GetDesktopCaptureEnabled();

        // Gets if Desktop Capture is supported
        bool GetDesktopCaptureSupported();

        // Gets the supported reasons for Desktop Capture. This is an extended version of GetDesktopCaptureSupported()
        // DesktopCaptureSupportedReasons GetDesktopCaptureSupportedReasons();
    }
}
