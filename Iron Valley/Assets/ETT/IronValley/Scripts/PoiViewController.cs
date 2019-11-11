using Ett.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ett.IronValley.Scripts
{
    public class PoiViewController : MonoBehaviour
    {
        [SerializeField]
        public Guid Guid;
        //just to have the Guid in editor
        public string TestGuid;

        public OpenClose GPSPanel;
        public OpenClose MapPanel;

        //public OpenClose WebcamPage;

        private void Start()
        {

        }

        public void Configure(Data.IronPOI poi)
        {
            this.Guid = poi.Uuid;
            TestGuid = this.Guid.ToString();

            POIManager.PoiVisitedEvent += ((guid) =>
            {
                if (this.Guid.Equals(guid))
                {
                    Debug.Log($"poi {guid} visited");

                    //this configures the action that should happen when the OpenCameraButton of GPS_POI_UI is clicked
                    GPSPanel.gameObject.GetComponent<GPS_POI_UI>().Configure(
                    () =>
                    {
                        //every time we configure the webcam to restart with new listeners, since webcam page is shared between views
                        ConfigureWebcamPage(poi);
                        //MainManager.Instance.OpenWebCam();
                    });

                    GPSPanel.Open();
                }
            });

            MapPanel.gameObject.GetComponent<MapPOI_UI>().Configure(poi);
        }

        public void ConfigureWebcamPage(Data.IronPOI poi)
        {
            MainManager.Instance.WebcamPage.gameObject.GetComponent<WebcamUI>().Configure(poi);
        }

        private bool _isMapViewOpen;
        private bool _isGpsViewOpen;

        public void OpenCloseMapView()
        {
            var toOpen = !_isMapViewOpen;
            OpenCloseMapPoiView(toOpen);
        }

        public void OpenCloseMapPoiView(bool toOpen)
        {
            if ((toOpen && _isMapViewOpen) || (!toOpen && !_isMapViewOpen)) return;

            _isMapViewOpen = toOpen;
        }

        public void OpenCloseGpsPoiView()
        {
            var toOpen = !_isGpsViewOpen;
            OpenCloseGpsPoiView(toOpen);
        }

        public void OpenCloseGpsPoiView(bool toOpen)
        {
            if ((toOpen && _isGpsViewOpen) || (!toOpen && !_isGpsViewOpen)) return;

            _isGpsViewOpen = toOpen;
        }
    }
}
