using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoiViewController : MonoBehaviour
{

    public Button PhotoGalleryBtn;
    public Button InsightsBtn;
    public Button VideoBtn;
    public Text AdviceTxt;

    private void Start()
    {

    }

    private enum ButtonType { Insights, MediaGallery, Video }

    public void Configure(IronPOI poi)
    {
        ConfigureButton(InsightsBtn, ButtonType.Insights, poi);
        ConfigureButton(PhotoGalleryBtn, ButtonType.MediaGallery, poi);
        ConfigureButton(VideoBtn, ButtonType.Video, poi);
        ///the advice text should be on when the poi is still not visited
        AdviceTxt.gameObject.SetActive(!GetPoiVisited(poi.Uuid));
    }

    private void ConfigureButton(Button btn, ButtonType type, IronPOI poi)
    {
        switch (type)
        {
            case ButtonType.Insights:
                btn.interactable = poi.Approfondimenti.HasValue && GetPoiVisited(poi.Uuid);
                btn.gameObject.SetActive(poi.Approfondimenti.HasValue);
                break;
            case ButtonType.MediaGallery:
                btn.interactable = poi.MediaGallery != null && poi.MediaGallery.Length > 0 && GetPoiVisited(poi.Uuid);
                btn.gameObject.SetActive(poi.Approfondimenti.HasValue);
                break;
            case ButtonType.Video:
                btn.interactable = string.IsNullOrWhiteSpace(poi.VideoMediaPath) && GetPoiVisited(poi.Uuid);
                btn.gameObject.SetActive(string.IsNullOrWhiteSpace(poi.VideoMediaPath));
                break;
            default:
                break;
        }
    }

    public void SetPoiVisited(System.Guid guid)
    {
        PlayerPrefs.SetInt($"poi_{guid}_visited", 1);
    }

    public bool GetPoiVisited(System.Guid guid)
    {
        return PlayerPrefs.GetInt($"poi_{guid}_visited") == 1;
    }
}
