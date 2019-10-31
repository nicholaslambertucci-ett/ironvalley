using Ett.Vdt.NativeApplication.Data.Sheets;
using Ett.Vdt.NativeApplication.Data.Types;
using Ett.Vdt.NativeApplication.Data.Vrs.Payloads;
using Ett.Vdt.NativeApplication.Data.Vrs.Payloads.Extentions;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Vrs.Extentions
{
    public static class ExtensionsToSheetItem
    {
        public static Vr? GetVr(this SheetItem item)
        {
            if (item.Type != TypeOut.Vr)
                return null;

            if (item.Extra != null)
                return (Vr)item.Extra;

            var vrPayload = JsonUtility.FromJson<VrPayload>(item.ExtraData);
            var vr = vrPayload.ToVr();
            item.Extra = vr;
            return vr;
        }
    }
}