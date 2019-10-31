using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ett.Vdt
{
    public static partial class MsgController
    {
        public static void OnEnterVR(string device)
        {
            PlatformOnEnterVr(device);
        }

        public static void OnExitToApp()
        {
            PlatformOnExitToApp();
        }

        public static void OnExitVR()
        {
            PlatformOnExitVr();
        }

        public static void OnReady()
        {
            PlatformOnReady();
        }

        public static void OnReady(string gameObjectName)
        {
            PlatformOnReady(gameObjectName);
        }






        #region Platform Methods

        static partial void PlatformOnExitToApp();
        static partial void PlatformOnEnterVr(string device);
        static partial void PlatformOnExitVr();
        static partial void PlatformOnReady();

        static partial void PlatformOnReady(string gameObjectName);

        #endregion

    }
}
