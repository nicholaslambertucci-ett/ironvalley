namespace Ett.Vdt.NativeApplication.Data.Beacons.Payloads.Extensions
{
    internal static class ExtensionsToBeaconPayload
    {
        public static Beacon ToBeacon(this BeaconPayload payload)
        {
            return new Beacon
            {
                Id = payload.id,
                Major = payload.major,
                Minor = payload.minor,
                Radius = payload.radius,
                Uuid = payload.uuid
            };
        }
    }
}