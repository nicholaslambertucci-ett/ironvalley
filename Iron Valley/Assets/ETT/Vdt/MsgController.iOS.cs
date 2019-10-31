#if UNITY_IPHONE //&& !UNITY_EDITOR
#define VDT_IOS
#endif

#if VDT_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
#endif

namespace Ett.Vdt
{
    public static partial class MsgController
    {
#if VDT_IOS

        [DllImport("__Internal")]
        private static extern void onExit();

        [DllImport("__Internal")]
        private static extern void onReady();

        [DllImport("__Internal")]
        private static extern void onEnterVR(string device);

        [DllImport("__Internal")]
        private static extern void onExitVR();



        static partial void PlatformOnExitToApp()
        {
            onExit();
        }

        static partial void PlatformOnReady()
        {
            onReady();
        }

        static partial void PlatformOnEnterVr(string device)
        {
            onEnterVR(device);
        }

        static partial void PlatformOnExitVr()
        {
            onExitVR();
        }
#endif
    }
}
