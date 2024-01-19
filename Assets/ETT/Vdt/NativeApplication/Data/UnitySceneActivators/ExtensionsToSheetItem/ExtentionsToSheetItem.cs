using Ett.Vdt.NativeApplication.Data.Sheets;
using Ett.Vdt.NativeApplication.Data.Types;
using Ett.Vdt.NativeApplication.Data.UnitySceneActivators.Payloads;
using Ett.Vdt.NativeApplication.Data.UnitySceneActivators.Payloads.Extensions;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.UnitySceneActivators.ExtensionsToSheetItem
{
    public static class ExtensionsToSheetItem
    {
        public static UnitySceneActivator? GetUnitySceneActivator(this SheetItem item)
        {
            if (item.Type != TypeOut.UnitySceneActivator)
                return null;

            if (item.Extra != null)
                return (UnitySceneActivator)item.Extra;

            var scenePayload = JsonUtility.FromJson<UnitySceneActivatorPayload>(item.ExtraData);
            var scene = scenePayload.ToUnityScene();
            item.Extra = scene;
            return scene;
        }
    }
}