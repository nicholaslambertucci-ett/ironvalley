using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Paths.Payloads
{
    [Serializable]
    public struct PathListPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public PathPayload[] paths;
    }
}