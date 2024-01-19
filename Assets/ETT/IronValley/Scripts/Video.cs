using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ett.IronValley.Scripts
{
    [System.Serializable]
    public class Video
    {
        [SerializeField] public string Path;
        [SerializeField] public Vector2 Position;
        [SerializeField] public Vector2 Size;
    }
}