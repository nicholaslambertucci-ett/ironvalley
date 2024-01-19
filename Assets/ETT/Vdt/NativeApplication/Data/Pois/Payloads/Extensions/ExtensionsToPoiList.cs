namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads.Extensions
{
    internal static class ExtensionsToPoiList
    {
        public static PoiListPayload ToPayload(this PoiList list)
        {
            var payload = new PoiListPayload
            {
                pois = new PoiPayload[list.Pois == null ? 0 : list.Pois.Length]
            };

            if (list.Pois == null)
                return payload;

            for (var i = 0; i < payload.pois.Length; i++)
                payload.pois[i] = list.Pois[i].ToPayload();


            return payload;
        }
    }
}