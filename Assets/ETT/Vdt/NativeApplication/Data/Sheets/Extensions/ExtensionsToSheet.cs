using Ett.Vdt.NativeApplication.Data.Sheets.Payloads;
using Ett.Vdt.NativeApplication.Data.Sheets.Payloads.Extensions;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Extensions
{
    internal static class ExtensionsToSheet
    {
        public static SheetPayload ToPayload(this Sheet sheet)
        {
            var payload = new SheetPayload
            {
                description = sheet.Description,
                id = sheet.Id,
                mediaPath = sheet.MediaPath,
                poiId = sheet.PoiId,
                subtitle = sheet.Subtitle,
                tag= sheet.Tag,
                title = sheet.Title,
                sheetItems= new SheetItemPayload[sheet.Items != null ? sheet.Items.Length : 0]
            };

            if (sheet.Items == null)
                return payload;

            for (var i = 0; i < payload.sheetItems.Length; i++)
                payload.sheetItems[i] = sheet.Items[i].ToPayload();

            return payload;
        }
    }
}