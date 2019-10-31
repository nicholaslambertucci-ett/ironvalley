using Ett.Vdt.NativeApplication.Data.Sheets.Payloads;
using Ett.Vdt.NativeApplication.Data.Sheets.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Types;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Extensions
{
    public static class ExtensionsToSheetItem
    {
        public static Sheet? GetSheet(this SheetItem item)
        {
            if (item.Type != TypeOut.Sheet)
                return null;

            if (item.Extra != null)
                return (Sheet)item.Extra;

            var sheetPayload = JsonUtility.FromJson<SheetPayload>(item.ExtraData);
            var sheet = sheetPayload.ToSheet();
            item.Extra = sheet;
            return sheet;
        }

        internal static SheetItemPayload ToPayload(this SheetItem item)
            => new SheetItemPayload
            {
                id = item.Id,
                extraData = item.ExtraData,
                type = ((InternalTypeOut)((int)item.Type)).ToString()
            };
    }
}