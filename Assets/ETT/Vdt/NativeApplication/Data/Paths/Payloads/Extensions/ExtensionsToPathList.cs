namespace Ett.Vdt.NativeApplication.Data.Paths.Payloads.Extensions
{
    internal static class ExtensionsToPathList
    {
        public static PathListPayload ToPayload(this PathList list)
        {
            var payload = new PathListPayload
            {
                paths = new PathPayload[list.Paths?.Length ?? 0]
            };

            if (list.Paths == null)
                return payload;

            for (var i = 0; i < payload.paths.Length; i++)
                payload.paths[i] = list.Paths[i].ToPayload();

            return payload;
        }
    }
}