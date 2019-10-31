#if UNITY_ANDROID && !UNITY_EDITOR && VDT_V_2
using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication
{
    public partial class NativeAppController
    {
        static partial void SendNativeAppReady(string gameObjectName)
        {
            try
            {
                MsgController.Jo.Call("onNativeAppReady", gameObjectName);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An error occurred while calling onNativeAppReady ({0}): {1}", gameObjectName, ex.Message);
            }
        }
    }
}
#endif