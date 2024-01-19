using System;
using Ett.Vdt.NativeApplication.Data.Types;
using Activator = Ett.Vdt.NativeApplication.Data.Activators.Activator;

namespace Ett.Vdt.NativeApplication.Data.Sheets
{
    public struct MultipleSheetListQuery
    {
        public const int None = -1;

        public static MultipleSheetListQuery ForActivations(int pathId, params Activator[] activator)
            => new MultipleSheetListQuery(pathId, activator);

        public readonly Activator[] Activators;
        public readonly int Limit;
        public readonly int Offset;
        public readonly int PathId;

        public readonly bool IsValid;

        private MultipleSheetListQuery(int pathId, Activator[] activators, int? limit = null, int? offset = null)
        {
            if (activators == null)
                throw new ArgumentNullException(nameof(activators));

            if (activators.Length == 0)
                throw new ArgumentException($"activatore cannot be empty", nameof(activators));

            this.PathId = pathId;

            this.Limit = limit ?? None;
            if (this.Limit < None)
                this.Limit = None;

            this.Offset = offset ?? None;
            if (this.Offset < None)
                this.Offset = None;

            for (var i = 0; i < activators.Length; i++)
                if (!activators[i].IsValid)
                    throw new ArgumentException($"Provided activator at index {i} is invalid");

            this.Activators = activators;

            this.IsValid = true;
        }

    }
}