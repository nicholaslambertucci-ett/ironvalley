using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ett.Scripts.Localization
{
    [CreateAssetMenu(fileName = "LocalizationData", menuName = "Localization/LocalizationData", order = 1)]
    public class LocalizationListManager : ScriptableObject
    {
        [SerializeField] 
        public LocalizedText[] LocalizedTexts;
    }
}
