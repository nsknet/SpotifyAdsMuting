using System;
using System.Runtime.InteropServices;
using SpotifyAdsMuting.AudioModels;

namespace SpotifyAdsMuting
{
    public class VolumeMixer
    {
        public static float? GetApplicationVolume(int pid)
        {
            var volume = GetVolumeObject(pid);
            if (volume == null)
                return null;

            volume.GetMasterVolume(out var level);
            Marshal.ReleaseComObject(volume);
            return level * 100;
        }

        public static bool? GetApplicationMute(int pid)
        {
            var volume = GetVolumeObject(pid);
            if (volume == null)
                return null;

            volume.GetMute(out var mute);
            Marshal.ReleaseComObject(volume);
            return mute;
        }

        public static void SetApplicationVolume(int pid, float level)
        {
            var volume = GetVolumeObject(pid);
            if (volume == null)
                return;

            var guid = Guid.Empty;
            volume.SetMasterVolume(level / 100, ref guid);
            Marshal.ReleaseComObject(volume);
        }

        public static void SetApplicationMute(int pid, bool mute)
        {
            var volume = GetVolumeObject(pid);
            if (volume == null)
                return;

            var guid = Guid.Empty;
            volume.SetMute(mute, ref guid);
            Marshal.ReleaseComObject(volume);
        }

        private static ISimpleAudioVolume GetVolumeObject(int pid)
        {
            // get the speakers (1st render + multimedia) device
            var deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out var speakers);

            // activate the session manager. we need the enumerator
            var iidIAudioSessionManager2 = typeof(IAudioSessionManager2).GUID;
            if (speakers == null)
                return null;
            speakers.Activate(ref iidIAudioSessionManager2, 0, IntPtr.Zero, out var o);
            var mgr = (IAudioSessionManager2)o;

            // enumerate sessions for on this device
            if (mgr == null)
                return null;
            mgr.GetSessionEnumerator(out var sessionEnumerator);
            sessionEnumerator.GetCount(out var count);

            // search for an audio session with the required name
            // NOTE: we could also use the process id instead of the app name (with IAudioSessionControl2)
            ISimpleAudioVolume volumeControl = null;
            for (var i = 0; i < count; i++)
            {
                sessionEnumerator.GetSession(i, out var ctl);
                ctl.GetProcessId(out var cpid);

                if (cpid == pid)
                {
                    volumeControl = ctl as ISimpleAudioVolume;
                    break;
                }
                Marshal.ReleaseComObject(ctl);
            }
            Marshal.ReleaseComObject(sessionEnumerator);
            Marshal.ReleaseComObject(mgr);
            Marshal.ReleaseComObject(speakers);
            Marshal.ReleaseComObject(deviceEnumerator);
            return volumeControl;
        }
    }
}