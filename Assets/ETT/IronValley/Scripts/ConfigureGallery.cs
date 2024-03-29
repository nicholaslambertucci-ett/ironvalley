﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using TU = Ett.Vdt.Utilities.TextureUtilities;

namespace Ett.IronValley.Scripts
{
    public class ConfigureGallery : MonoBehaviour
    {
        [Tooltip("Check this if you want to set the Pages Sprites via editor")]
        public bool AutoConfigure;
        [Tooltip("Set this sprite array if using AutoConfig, else it can be left empty - use the texture one as it's faster")]
        public Sprite[] PageSprites;
        [Tooltip("Set this texture array if using AutoConfig, else it can be left empty")]
        public string[] PageTexturePaths;

        [Header("UI Elements")]
        public HorizontalScrollSnap horizontalScrollSnap;
        [Tooltip("The close gallery button")]
        public Button CloseButton;
        [Header("PREFABS")]
        [Tooltip("Add a prefab here with a image if working with sprites or a RawImage if working with textures")]
        public GameObject PagePrefab;
        [Tooltip("Add a prefab for the pagination toggle to show which page is currently shown")]
        public GameObject PaginationToggle;


        // Start is called before the first frame update
        void Start()
        {
            if (AutoConfigure)
            {
                if (PageSprites.Length > 0)
                    Configure(PageSprites);
                if (PageTexturePaths.Length > 0)
                    Configure(PageTexturePaths);
            }
        }

        //don't use this method
        //use the texture version!
        public void Configure(Sprite[] pageSprites)
        {
            Debug.LogWarning("Use the texture version!!");
            foreach (var p in pageSprites)
            {
                var t = Instantiate(PaginationToggle, horizontalScrollSnap.Pagination.transform);
                var g = Instantiate(PagePrefab);
                g.GetComponent<Image>().sprite = p;
                horizontalScrollSnap.AddChild(g);
            }
        }

        public void Configure(string[] paths)
        {
            foreach (var p in paths)
            {
                var pt = Instantiate(PaginationToggle, horizontalScrollSnap.Pagination.transform);
                var g = Instantiate(PagePrefab);
                // StartCoroutine(LoadRawImageTexture(g.GetComponent<RawImage>(), p));
                StartCoroutine(TU.LoadLocalTextureCoroutine(p, (tex) =>
                {
                    g.transform.GetChild(0).GetComponent<RawImage>().texture = tex;
                    g.transform.GetChild(0).GetComponent<AspectRatioFitter>().aspectRatio = tex.width / (float)tex.height;
                }));

                horizontalScrollSnap.AddChild(g);
            }
        }
    }
}