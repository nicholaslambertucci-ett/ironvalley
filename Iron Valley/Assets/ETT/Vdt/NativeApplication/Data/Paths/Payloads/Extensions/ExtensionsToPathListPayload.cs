namespace Ett.Vdt.NativeApplication.Data.Paths.Payloads.Extensions
{
    internal static class ExtensionsToPathListPayload
    {
        public static PathList ToPathList(this PathListPayload payload)
        {
            var pathList = new PathList
            {
                Paths = new Path[payload.paths?.Length ?? 0]
            };

            if (payload.paths == null)
                return pathList;

            for (var i = 0; i < pathList.Paths.Length; i++)
                pathList.Paths[i] = payload.paths[i].ToPath();

            return pathList;
        }
    }
}