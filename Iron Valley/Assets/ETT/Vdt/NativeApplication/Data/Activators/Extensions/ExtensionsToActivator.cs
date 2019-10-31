using System;
using Ett.Vdt.NativeApplication.Data.Activators.Payloads;
using Ett.Vdt.NativeApplication.Data.Types;

namespace Ett.Vdt.NativeApplication.Data.Activators.Extensions
{
    internal static class ExtensionsToActivator
    {
        public static ActivatorPayload ToPayload(this Activator activator)
        {
            if(!activator.IsValid)
                throw new ArgumentException("Provided activator is invalid");

            return new ActivatorPayload
            {
                id = activator.Id,
                type = ((InternalTypeIn) ((int) activator.Type)).ToString(),
            };
        }
    }
}