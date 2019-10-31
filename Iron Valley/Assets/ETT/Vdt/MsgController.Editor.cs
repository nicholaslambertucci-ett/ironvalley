#if UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IPHONE
using Ett.Vdt.Fake;
using UnityEngine;

namespace Ett.Vdt
{
    public static partial class MsgController
    {

        static partial void PlatformOnExitToApp()
        {
            Debug.Log("OnExitToApp");
            FakeNativeApp.Instance.CallFakeNative("OnExitToApp");   
        }

        static partial void PlatformOnEnterVr(string device)
        {
            Debug.LogFormat("OnEnterVr: {0}", device);
            FakeNativeApp.Instance.CallFakeNative("OnEnterVr", device);
        }

        static partial void PlatformOnExitVr()
        {
            Debug.Log("OnExitVr");
            FakeNativeApp.Instance.CallFakeNative("OnExitVr");
        }

        static partial void PlatformOnReady()
        {
            Debug.Log("OnReady");
            FakeNativeApp.Instance.CallFakeNative("OnReady");
        }

        static partial void PlatformOnReady(string gameObjectName)
        {
            Debug.LogFormat("OnReady - {0}", gameObjectName);
            FakeNativeApp.Instance.CallFakeNative("OnReady", gameObjectName);
        }
    }
}
#endif