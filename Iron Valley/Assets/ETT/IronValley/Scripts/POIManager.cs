using Ett.IronValley.Scripts.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TU = Ett.Vdt.Utilities.TextureUtilities;

namespace Ett.IronValley.Scripts
{
    public class POIManager : MonoBehaviour
    {
        //a View Controller for each POI
        public PoiViewController[] POIS;

        [Tooltip("The content of the horizontal scrollview where to instantiate the gallery thumbnails prefabs")]
        public Transform GalleriesParent;
        [Tooltip("The prefab of the gallery thumbnail")]
        public GameObject GalleryThumbPrefab;
        [Tooltip("The image gallery prefab")]
        public GameObject GalleryPrefab;
        [Tooltip("Where to instantiate the horizontal snap gallery")]
        public Transform GalleryParent;

        //Poi Visited Event
        public delegate void PoiVisited(Guid guid);
        public static event PoiVisited PoiVisitedEvent;

        private int _currentGPS_Poi;

        public void Configure(Data.IronPOI[] pois)
        {
            // configure the PoiViewControllers
            for (var i = 0; i < pois.Length; i++)
            {
                //check the tag just to be sure
                if (pois[i].Tag.Equals($"poi-{i + 1}"))
                {
                    POIS[i].Configure(pois[i]);
                }
            }
        }

        public int GetCurrentGPS_Poi()
        {
            return _currentGPS_Poi;
        }

        public void TestVisitPoi(int nPoi)
        {
            SetPoiVisited(POIS[nPoi].Guid);
        }

        public static bool GetPoiVisited(Guid guid)
        {
            return PlayerPrefs.GetInt($"poi_{guid}_visited") == 1;
        }

        public static void SavePoiVisited() {
            PlayerPrefs.Save();
        }

        public static void SetPoiVisited(Guid guid)
        {
            //set the POI visited if it wasn't so
            if (!GetPoiVisited(guid))
            {
                PlayerPrefs.SetInt($"poi_{guid}_visited", 1);
                PoiVisitedEvent.Invoke(guid);
            }
        }

        public void ConfigureGallery(IronGallery[] galleryItems) {

            GalleriesParent.RemoveAllChildren();

            //when clicked, load galleryItems thumbnails
            foreach (var gallery in galleryItems)
            {
                Debug.Log($"{Time.time} loading item " + gallery.Title);
                var thumb = Instantiate(GalleryThumbPrefab, GalleriesParent);
                StartCoroutine(TU.LoadLocalTextureCoroutine(gallery.ThumbPath,
                (texture) =>
                {
                    Debug.Log($"{Time.time} finished loading item " + gallery.Title);

                    var galleryBtn = thumb.GetComponent<GalleryButton>();
                    galleryBtn.Configure(texture, gallery.Title,
                    //no gallery configuration until we click a thumbnail
                    //when the gallery button is clicked, configure and open the relative gallery
                    () =>
                    {
                        Debug.Log("gallery button clicked! configured with thumbnail: " + gallery.ThumbPath);
                        GalleryParent.RemoveAllChildren();
                        var galleryGO = Instantiate(GalleryPrefab, GalleryParent);
                        var paths = new string[gallery.Items.Length];
                        for (var i = 0; i < gallery.Items.Length; i++)
                        {
                            paths[i] = gallery.Items[i].MediaPath;
                        }
                        var gal = galleryGO.GetComponent<ConfigureGallery>();
                        gal.Configure(paths);

                        gal.CloseButton.onClick.AddListener(() =>
                        {
                            GalleryParent.gameObject.GetComponent<OpenClose>().Close();
                        });

                        GalleryParent.gameObject.GetComponent<OpenClose>().Open();
                    });
                }));
            }
        }
    }
}