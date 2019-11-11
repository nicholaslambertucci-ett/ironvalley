using Ett.IronValley.Scripts.Utilities;
using Ett.Scripts.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TU = Ett.Vdt.Utilities.TextureUtilities;

namespace Ett.IronValley.Scripts
{
    public class WebcamUI : MonoBehaviour
    {
        [Tooltip("Where to instantiate the buttons with the contents of the current selected poi")]
        public Transform BtnContainer;
        [Tooltip("The poi button prefab to instantiate in the camera page")]
        public GameObject BtnPrefab;
    /*    [Tooltip("The main Panel with the Galleries, with a canvas group to open/close it")]
        public OpenClose GalleriesPanel;
        [Tooltip("The content of the horizontal scrollview where to instantiate the gallery thumbnails prefabs")]
        public Transform GalleriesParent;
        [Tooltip("The prefab of the gallery thumbnail")]
        public GameObject GalleryThumbPrefab;
        [Tooltip("The image gallery prefab")]
        public GameObject GalleryPrefab;
        [Tooltip("Where to instantiate the horizontal snap gallery")]
        public Transform GalleryParent;
*/
        public LocalizationListManager VideoLocalizationList;
        public LocalizationListManager GalleryLocalizationList;
        public LocalizationListManager InsightsLocalizationList;

        //configure the page every time we open it
        public void Configure(Data.IronPOI poi)
        {
            BtnContainer.RemoveAllChildren();
            //GalleriesParent.RemoveAllChildren();
            /*

            ///clear children of the BtnContainer
            for (var c = 0; c < BtnContainer.childCount; c++)
            {
                Destroy(BtnContainer.GetChild(c).gameObject);
            }

            ///clear children of the GalleriesParent
            for (var c = 0; c < GalleriesParent.childCount; c++)
            {
                Destroy(GalleriesParent.GetChild(c).gameObject);
            }*/

            /// If has Insights
            if (poi.Insights.HasValue)
            {
                var title = LocalizationManager.GetCurrentLanguageStringFromAsset(InsightsLocalizationList);//.LocalizedTexts[(int)LocalizationManager.SelectedLanguage];

                ConfigurePoi(title, new IronGallery[] { poi.Insights.Value });

                /*
                //instantiate a button 
                var btn = Instantiate(BtnPrefab, BtnContainer);

                btn.GetComponent<PoiButton>().Configure(InsightsLocalizationList, () =>
                {
                    Debug.Log("should open Gallery of Galleries!");
                    MainManager.Instance.OpenGalleries();
                });*/
            }

            /// If poi has MediaGallery
            if (poi.MediaGallery != null && poi.MediaGallery.Length > 0)
            {
                //.LocalizedTexts[(int)LocalizationManager.SelectedLanguage];

                ConfigurePoi(poi.MediaGalleryLabel, poi.MediaGallery);


                /*
                //instantiate a button 
                var btn = Instantiate(BtnPrefab, BtnContainer);

                btn.GetComponent<PoiButton>().Configure(GalleryLocalizationList, () =>
                {
                    Debug.Log("should open Gallery of Galleries!");
                    MainManager.Instance.OpenGalleries();
                });*/

                //configure listeners
                /*  foreach (var gallery in poi.MediaGallery)
                  {

                      StartCoroutine(TU.LoadLocalTextureCoroutine(gallery.ThumbPath,
                      (texture) =>
                      {
                          var thumb = Instantiate(GalleryThumbPrefab, GalleriesParent);
                          var galleryBtn = thumb.GetComponent<GalleryButton>();
                          galleryBtn.Configure(texture, gallery.Title, () =>
                          {
                              Debug.Log("gallery button configured with texture: " + gallery.ThumbPath);
                          });
                      }));
                  }*/
            }
        }

        private void ConfigurePoi(string title, IronGallery[] galleryItems)
        {
            //instantiate a button in the camera page with available content - gallery of galleries, insights, video, vr?
            var btn = Instantiate(BtnPrefab, BtnContainer);

            //configure the button that will open the Gallery of Galleries
            btn.GetComponent<PoiButton>().Configure(title, () =>
            {
                MainManager.Instance.POIManager.ConfigureGallery(galleryItems);

                /*
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
                */

                Debug.Log("coming from Webcam view, should open Gallery of Galleries!");
                MainManager.Instance.OpenGalleries();
                MainManager.Instance.CloseWebcam();
            });


        }

    }
}
