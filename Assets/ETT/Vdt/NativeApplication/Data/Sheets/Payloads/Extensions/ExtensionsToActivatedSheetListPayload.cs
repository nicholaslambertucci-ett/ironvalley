using Ett.Vdt.NativeApplication.Data.Activators.Payloads.Extensions;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads.Extensions
{
    internal static class ExtensionsToActivatedSheetListPayload
    {
        public static ActivatedSheetList ToActivatedSheetList(this ActivatedSheetListPayload payload)
            => new ActivatedSheetList
            {
                Activator = payload.activator.ToActivator(),
                List = payload.list.ToSheetList()
            };
    }
}