using System;
using UnityEngine;

namespace Ett.Vdt.Data
{
    [Serializable]
    public struct Message
    {
        [SerializeField]
        public string Type;
        [SerializeField]
        public string Payload;

    }
}
