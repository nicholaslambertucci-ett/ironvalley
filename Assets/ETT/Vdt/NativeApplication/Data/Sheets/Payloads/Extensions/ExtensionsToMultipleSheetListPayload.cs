namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads.Extensions
{
    internal static class ExtensionsToMultipleSheetListPayload
    {
        public static MultipleSheetList ToMultipleSheetList(this MultipleSheetListPayload payload)
        {
            if (payload.activatedList == null)
                return new MultipleSheetList
                {
                    ActivatedList = new ActivatedSheetList[0]
                };

            var list = new MultipleSheetList
            {
                ActivatedList = new ActivatedSheetList[payload.activatedList.Length]
            };

            for (var i = 0; i < payload.activatedList.Length; i++)
                list.ActivatedList[i] = payload.activatedList[i].ToActivatedSheetList();

            return list;
        }
    }
}