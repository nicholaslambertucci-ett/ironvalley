using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Paths.Payloads
{
    [Serializable]
    internal struct PathRelCategoryPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField]public int categoryId;
        // ReSharper disable once InconsistentNaming
        [SerializeField]public int pathId;
    }
}