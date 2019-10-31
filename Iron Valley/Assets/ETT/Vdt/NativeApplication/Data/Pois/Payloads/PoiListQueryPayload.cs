using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads
{
    [Serializable]
    internal struct PoiListQueryPayload
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
        [SerializeField] public int pathId;
        //// ReSharper disable once InconsistentNaming
        //[SerializeField] public int[] pathsId;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int limit;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int offset;



        //// ReSharper disable once InconsistentNaming
        //[SerializeField] public int[] excludePathCategoriesId;
        //// ReSharper disable once InconsistentNaming
        //[SerializeField] public string[] excludePathCategoriesTag;
        //// ReSharper disable once InconsistentNaming
        //[SerializeField] public int[] excludePathsId;
        // ReSharper disable once InconsistentNaming
        //// ReSharper disable once InconsistentNaming
        //[SerializeField] public int[] pathCategoriesId;
        //// ReSharper disable once InconsistentNaming
        //[SerializeField] public string[] pathCategoriesTag;
    }
}