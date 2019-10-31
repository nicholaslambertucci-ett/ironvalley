namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads.Extensions
{
    internal static class ExtensionsToSheetPayload
    {
        public static Sheet ToSheet(this SheetPayload payload)
        {
            var sheet = new Sheet
            {
                Description = payload.description,
                Id = payload.id,
                MediaPath = payload.mediaPath,
                PoiId = payload.poiId,
                Subtitle = payload.subtitle,
                Tag = payload.tag,
                Title = payload.title,
                Items = new SheetItem[payload.sheetItems != null ? payload.sheetItems.Length : 0]
            };

            if (payload.sheetItems == null)
                return sheet;

            for (var i = 0; i < sheet.Items.Length; i++)
                sheet.Items[i] = payload.sheetItems[i].ToSheetItem();

            return sheet;
        }
    }
}