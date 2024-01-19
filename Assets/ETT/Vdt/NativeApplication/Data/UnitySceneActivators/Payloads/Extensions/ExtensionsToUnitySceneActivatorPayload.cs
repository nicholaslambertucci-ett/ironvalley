namespace Ett.Vdt.NativeApplication.Data.UnitySceneActivators.Payloads.Extensions
{
    internal static class ExtensionsToUnitySceneActivatorPayload
    {
        public static UnitySceneActivator ToUnityScene(this UnitySceneActivatorPayload payload)
            => new UnitySceneActivator
            {
                Code = payload.code,
                Extra = payload.extra,
                Name = payload.name
            };
    }
}