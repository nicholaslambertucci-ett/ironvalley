using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Activators.Payloads
{
    [Serializable]
    internal struct ActivatorPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int id;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string type;
    }
}