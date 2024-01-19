using Ett.Vdt.NativeApplication.Data.Sheets.Payloads;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Extensions
{
    internal static class ExtensionsToSheetList
    {
        public static SheetListPayload ToPayload(this SheetList list)
        {
            var payload = new SheetListPayload
            {
                sheets = new SheetPayload[list.Sheets?.Length ?? 0]
            };

            if (list.Sheets == null)
                return payload;

            for (var i = 0; i < payload.sheets.Length; i++)
                payload.sheets[i] = list.Sheets[i].ToPayload();

            return payload;
        }
    }
}