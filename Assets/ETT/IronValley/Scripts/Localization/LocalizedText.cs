using System;
using System.Collections;
using UnityEngine;

namespace Ett.Scripts.Localization
{

    [System.Serializable]
    public class LocalizedText
    {
        public LocalizationManager.Language language;
        [TextArea(1, 20)]
        public string text;
    }
}