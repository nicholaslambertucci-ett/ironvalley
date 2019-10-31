using System;
using Ett.Vdt.NativeApplication.Data.Types;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads.Extensions
{
    internal static class ExtensionsToSheetItemPayload
    {
        public static SheetItem ToSheetItem(this SheetItemPayload payload)
        {
            InternalTypeOut internalType;
            var item = new SheetItem
            {
                Id = payload.id,
                ExtraData = payload.extraData,
                Type = Enum.TryParse(payload.type, true, out internalType)
                ? (TypeOut)((int)internalType)
                : TypeOut.Unknown
            };
            
            return item;
        }
    }
}