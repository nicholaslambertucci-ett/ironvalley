using Ett.IronValley.Scripts;
using Ett.IronValley.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPOI_UI : MonoBehaviour
{
    public Button PhotoGalleryBtn;
    public Button InsightsBtn;
    public Button VideoBtn;
    public Text AdviceTxt;

    private enum ButtonType { Insights, MediaGallery, Video }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Configure(IronPOI poi)
    {
        Debug.Log($"Configure poi: {poi.Title} MainManager.GetPoiVisited({poi.Uuid}): " + MainManager.GetPoiVisited(poi.Uuid));

        /// configure the buttons states on this page
        ConfigureButton(InsightsBtn, ButtonType.Insights, poi);
        ConfigureButton(PhotoGalleryBtn, ButtonType.MediaGallery, poi);
        ConfigureButton(VideoBtn, ButtonType.Video, poi);

        /// the advice text should be on when the poi is still not visited
        AdviceTxt.gameObject.SetActive(!MainManager.GetPoiVisited(poi.Uuid));
    }

    private void ConfigureButton(Button btn, ButtonType type, IronPOI poi)
    {
        

        switch (type)
        {
            case ButtonType.Insights:
                btn.interactable = poi.Insights.HasValue && MainManager.GetPoiVisited(poi.Uuid);
                btn.gameObject.SetActive(poi.Insights.HasValue);
                if (btn.interactable && btn.gameObject.activeSelf)
                    ConfigureButtonGalleries(btn, new IronGallery[] { poi.Insights.Value });
           /*     btn.gameObject.GetComponent<PoiButton>().Configure("",
                        () =>
                        {
                            MainManager.Instance.POIManager.ConfigureGallery(
                            new IronGallery[] {
                            poi.Insights.Value
                            });
                            MainManager.Instance.OpenGalleries();
                        });
                        */
                break;
            case ButtonType.MediaGallery:
                btn.interactable = poi.MediaGallery != null && poi.MediaGallery.Length > 0 && MainManager.GetPoiVisited(poi.Uuid);
                btn.gameObject.SetActive(poi.MediaGallery != null && poi.MediaGallery.Length > 0);
                if (btn.interactable && btn.gameObject.activeSelf)
                    ConfigureButtonGalleries(btn, poi.MediaGallery);

                break;
            case ButtonType.Video:
                btn.interactable = string.IsNullOrWhiteSpace(poi.VideoMediaPath) && MainManager.GetPoiVisited(poi.Uuid);
                btn.gameObject.SetActive(string.IsNullOrWhiteSpace(poi.VideoMediaPath));
                break;
            default:
                break;
        }
    }

    private void ConfigureButtonGalleries(Button b, IronGallery[] ironGalleries)
    {

        b.gameObject.GetComponent<PoiButton>().Configure("",
        () =>
        {
            MainManager.Instance.POIManager.ConfigureGallery(ironGalleries);
            MainManager.Instance.OpenGalleries();
        });

        
    }
}
