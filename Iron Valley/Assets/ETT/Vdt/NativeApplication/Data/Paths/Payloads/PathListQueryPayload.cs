using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Paths.Payloads
{
    [Serializable]
    internal struct PathListQueryPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int[] categoriesId;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string[] categoriesTag;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int[] excludeCategoriesId;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string[] excludeCategoriesTag;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int[] ids;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int limit;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int offset;
    }
}