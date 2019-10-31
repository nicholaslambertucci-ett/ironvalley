using Ett.Vdt.NativeApplication.Data.Sheets.Payloads.Extensions;

namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads.Extensions
{
    internal static class ExtensionsToPoiDetailsPayload
    {
        public static PoiDetails ToPoiDetails(this PoiDetailsPayload payload)
        {
            return new PoiDetails
            {
                Description = payload.description,
                Id = payload.id,
                MediaPath = payload.mediaPath,
                Sheet = payload.sheet.ToSheet(),
                Subtitle = payload.subtitle,
                Title = payload.title
            };
        }
    }
}