using Ett.Vdt.NativeApplication.Data.Types;

namespace Ett.Vdt.NativeApplication.Data.Maps
{
    public struct MapListQuery
    {
        public const int None = -1;

        public static MapListQuery ForPathEntryPoint(int pathId, 
            string[] tags = null, string[] excludedTags = null,
            int? limit = null, int? offset = null) 
            => new MapListQuery(pathId, TypeIn.EntryPoint,
                tags: tags, excludedTags: excludedTags,
                limit: limit, offset: limit);

        public readonly int ActivationId;
        public readonly TypeIn ActivationType;
        public readonly string[] ExcludedTags;
        public readonly int PathId;
        public readonly int PoiId;
        public readonly string[] Tags;

        public readonly int Limit;
        public readonly int Offset;

        private MapListQuery(int pathId, TypeIn activationType, 
            int? poiId = null, int? activationId = null,
            string[] tags = null, string[] excludedTags = null, 
            int? limit = null, int? offset= null)
        {
            this.ActivationId = activationId.HasValue && activationId.Value >= None
                ? activationId.Value
                : None;

            this.ActivationType = activationType;

            this.ExcludedTags = excludedTags;

            this.PathId = pathId;

            this.PoiId = poiId.HasValue && poiId.Value >= None
                ? poiId.Value
                : None;

            this.Tags = tags ?? new string[0];

            this.Limit = limit.HasValue && limit.Value >= None
                ? limit.Value
                : None;

            this.Offset = offset.HasValue && offset.Value >= None
                ? offset.Value
                : None;

            this.IsValid = true;
        }


        internal readonly bool IsValid;
    }
}