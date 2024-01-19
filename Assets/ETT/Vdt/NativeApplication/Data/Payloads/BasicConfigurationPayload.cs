using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Payloads
{
    [Serializable]
    internal struct BasicConfigurationPayload
    {
        [SerializeField]
        // ReSharper disable once InconsistentNaming
        public string language;
    }
}
