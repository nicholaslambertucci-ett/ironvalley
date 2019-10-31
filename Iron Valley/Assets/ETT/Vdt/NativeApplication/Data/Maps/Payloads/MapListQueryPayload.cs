using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Maps.Payloads
{
    [Serializable]
    internal struct MapListQueryPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int activationId;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string activationType;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string[] excludedTags;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int pathId;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int poiId;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string[] tags;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int limit;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int offset;
    }
}