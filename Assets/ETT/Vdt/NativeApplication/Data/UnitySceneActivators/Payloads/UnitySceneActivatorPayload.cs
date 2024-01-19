using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.UnitySceneActivators.Payloads
{
    [Serializable]
    internal struct UnitySceneActivatorPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string code;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string extra;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string name;
    }
}