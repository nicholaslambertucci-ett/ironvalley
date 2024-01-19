using System;
using Ett.Vdt.NativeApplication.Data.Activators.Extensions;
using Ett.Vdt.NativeApplication.Data.Activators.Payloads;
using Ett.Vdt.NativeApplication.Data.Sheets.Payloads;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Extensions
{
    internal static class ExtensionsToMultipleSheetListQuery
    {
        public static MultipleSheetListQueryPayload ToPayload(this MultipleSheetListQuery query)
        {
            if(!query.IsValid)
                throw new ArgumentException($"Provided query is invalid", nameof(query));

            var payload = new MultipleSheetListQueryPayload
            {
                activators = new ActivatorPayload[query.Activators.Length],
                limit = query.Limit,
                offset = query.Offset,
                pathId = query.PathId
            };

            for (var i = 0; i < query.Activators.Length; i++)
                payload.activators[i] = query.Activators[i].ToPayload();

            return payload;
        }
    }
}