using Ett.Scripts.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoiButton : MonoBehaviour
{
    public Text Title;
    public Button Button;

    public void Configure(string title, Action onClick)
    {
        //if title is not empty and Title is not null set it
        if (!string.IsNullOrWhiteSpace(title) && Title)
            this.Title.text = title;
        Button.onClick.AddListener(() => onClick());
    }
}
