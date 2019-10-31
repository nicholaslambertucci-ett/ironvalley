using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Maps.Payloads
{
    [Serializable]
    internal struct MapListPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public MapPayload[] maps;
    }
}