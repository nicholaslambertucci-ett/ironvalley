using Ett.Vdt.NativeApplication.Data.Types;

namespace Ett.Vdt.NativeApplication.Data.Sheets
{
    public struct SheetListQuery
    {
        public const int None = -1;

        public static SheetListQuery ForPathEntryPoint(int pathId)
            => new SheetListQuery(pathId, TypeIn.EntryPoint);

        public static SheetListQuery ForActivation(int pathId, int activationId, TypeIn activationType)
            => new SheetListQuery(pathId, activationType, activationId);

        public readonly int ActivationId;
        public readonly TypeIn ActivationType;
        public readonly int Limit;
        public readonly int Offset;
        public readonly int PathId;

        public readonly bool IsValid;

        private SheetListQuery(int pathId, TypeIn activationType, int? activationId = null, int? limit = null, int? offset = null)
        {
            this.ActivationId = activationId ?? -1;
            if (this.ActivationId < None)
                this.ActivationId = None;

            this.ActivationType = activationType;
            this.PathId = pathId;

            this.Limit = limit ?? None;
            if (this.Limit < None)
                this.Limit = None;

            this.Offset = offset ?? None;
            if (this.Offset < None)
                this.Offset = None;

            this.IsValid = true;
        }
    }
}