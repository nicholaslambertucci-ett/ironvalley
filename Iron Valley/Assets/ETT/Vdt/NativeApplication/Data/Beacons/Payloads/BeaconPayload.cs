using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Beacons.Payloads
{
    [Serializable]
    public struct BeaconPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int id;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int major;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int minor;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public float radius;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string uuid;
    }
}