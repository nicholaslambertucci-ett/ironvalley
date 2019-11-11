using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ett.IronValley.Scripts.Data
{
    [System.Serializable]

    public struct IronPOI
    {
        [SerializeField] public string Title;
        [SerializeField] public string Description;
        [SerializeField] public string Tag; //poi-1, poi-2, poi-3

        //dallo sheetitem di tipo gallery il cui tag è video
        [SerializeField] public string VideoMediaPath;
        [SerializeField] public string VideoLabel;

        //dallo sheetitem che attiva una sheet il cui tag è approfondimenti
        [SerializeField] public IronGallery? Insights;

        //dallo sheetitem che attiva una sheet il cui tag è foto
        [SerializeField] public IronGallery[] MediaGallery; //gallery foto o gallery di gallery
        [SerializeField] public string MediaGalleryLabel; // titolo della gallery

        [SerializeField] Ett.Vdt.NativeApplication.Data.Vrs.Vr? VrView;

        [SerializeField] public Guid Uuid;

    }
}

