using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads
{
    [Serializable]
    internal struct PoiRelCategoryPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField]public int categoryId;
        // ReSharper disable once InconsistentNaming
        [SerializeField]public int poiId;
    }
}