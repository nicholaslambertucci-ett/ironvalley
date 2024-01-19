#if UNITY_EDITOR && VDT_V_2
using Ett.EditorTests;
using UnityEngine;

namespace Ett.Vdt.NativeApplication
{
    public partial class NativeAppController
    {
        static partial void SendNativeAppReady(string gameObjectName)
        {
            FakeNativeApp.Instance.CallFakeNative("onNativeAppReady", gameObjectName);
        }

    }
}
#endif