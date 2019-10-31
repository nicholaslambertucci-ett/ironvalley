#if UNITY_ANDROID //&& !UNITY_EDITOR
using UnityEngine;

namespace Ett.Vdt
{
    public static partial class MsgController
    {

        public static readonly AndroidJavaObject Jo;

        static partial void PlatformOnExitToApp()
        {
            Jo.Call("onExit");
        }

        static partial void PlatformOnReady()
        {
            Jo.Call("onReady");
        }

        static partial void PlatformOnEnterVr(string device)
        {
            Jo.Call("OnEnterVR, device: " + device);
        }

        static partial void PlatformOnExitVr()
        {
            Jo.Call("OnExitVR");
        }

        

        static MsgController()
        {
            var jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            //Jo = new AndroidJavaObject("com.ettsolutions.virtuallyyoursunity.UnityPlayerActivity");
            Jo = jo;
        }
    }
}
#endif