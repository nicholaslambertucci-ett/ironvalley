using System;
using Ett.Vdt.NativeApplication.Data.Types;

namespace Ett.Vdt.NativeApplication.Data.Activators.Payloads.Extensions
{
    internal static class ExtensionsToActivatorPayload
    {
        public static Activator ToActivator(this ActivatorPayload payload)
        {
            InternalTypeIn internalActivatorType;
            Enum.TryParse(payload.type, true, out internalActivatorType);
            return new Activator((TypeIn) ((int)internalActivatorType), payload.id);
        }
    }
}