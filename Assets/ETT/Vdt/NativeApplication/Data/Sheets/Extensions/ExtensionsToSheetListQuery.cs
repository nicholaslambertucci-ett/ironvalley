using System;
using Ett.Vdt.NativeApplication.Data.Sheets.Payloads;
using Ett.Vdt.NativeApplication.Data.Types;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Extensions
{
    internal static class ExtensionsToSheetListQuery
    {
        public static SheetListQueryPayload ToPayload(this SheetListQuery request)
        {
            if(!request.IsValid)
                throw new ArgumentException("Invalid sheet query");

            return new SheetListQueryPayload
            {
                activationId = request.ActivationId,
                activationType = ((InternalTypeIn) ((int) request.ActivationType)).ToString(),
                limit = request.Limit,
                offset = request.Offset,
                pathId = request.PathId
            };
        }
    }
}