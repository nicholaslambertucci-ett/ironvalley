using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ett.IronValley.Scripts
{
    public class GPS_POI_UI : MonoBehaviour
    {
        public Button OpenCameraButton;

        public void Configure(Action onClick)
        {
            OpenCameraButton.onClick.RemoveAllListeners();

            OpenCameraButton.onClick.AddListener(() =>
            {
                onClick.Invoke();
            });
        }
    }
}