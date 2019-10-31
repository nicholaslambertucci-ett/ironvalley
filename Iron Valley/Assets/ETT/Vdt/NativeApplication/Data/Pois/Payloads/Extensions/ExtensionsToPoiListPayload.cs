namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads.Extensions
{
    internal static class ExtensionsToPoiListPayload
    {
        public static PoiList ToGetPoiList(this PoiListPayload payload)
        {
            var poiList = new PoiList
            {
                Pois = new Poi[payload.pois == null ? 0 : payload.pois.Length]
            };

            if (payload.pois == null)
                return poiList;

            for (var i = 0; i < poiList.Pois.Length; i++)
                poiList.Pois[i] = payload.pois[i].ToPoi();


            return poiList;
        }
    }
}