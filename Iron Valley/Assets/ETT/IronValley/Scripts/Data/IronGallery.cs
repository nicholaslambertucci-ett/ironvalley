using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct IronGallery
{
    [SerializeField] public string Title;
    [SerializeField] public string ThumbPath;
    [SerializeField] public IronGalleryItem[] Items;
}