using Ett.Vdt.NativeApplication.Data.Payloads;

namespace Ett.Vdt.NativeApplication.Data.Extensions
{
    internal static class ExtensionsToBasicConfigurationPayload
    {
        public static BasicConfiguration ToBasicConfiguration(this BasicConfigurationPayload payload)
            => new BasicConfiguration
            {
                Language = payload.language
            };
    }
}