using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// In the choose experience window this script is attached to the instanced Buttons of the selected poi
/// </summary>
public class GalleryButton : MonoBehaviour
{
    public RawImage Thumbnail;
    public Text Title;
    public Button Button;

    public void Configure(Texture2D tex, string title, Action onClick)
    {   

        Thumbnail.texture = tex;
        Thumbnail.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = (float)tex.width / tex.height;

        Title.text = title;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => onClick());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
