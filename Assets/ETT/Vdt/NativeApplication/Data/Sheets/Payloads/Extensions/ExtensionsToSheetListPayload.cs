namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads.Extensions
{
    internal static class ExtensionsToSheetListPayload
    {
        public static SheetList ToSheetList(this SheetListPayload payload)
        {
            var list = new SheetList
            {
                Sheets = new Sheet[payload.sheets?.Length ?? 0]
            };

            if (payload.sheets == null)
                return list;

            for (var i = 0; i < list.Sheets.Length; i++)
                list.Sheets[i] = payload.sheets[i].ToSheet();

            return list;
        }
    }
}