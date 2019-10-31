using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ett.Scripts.Localization
{
    public class LocalizationComponent : MonoBehaviour
    {
        public LocalizationListManager localizationList;

        public bool autoInit = true;

        [SerializeField] private TMPro.TMP_Text TMP_UIElement;
        [SerializeField] private Text Text_UIElement;


        LocalizationManager.Language lang;
        Dictionary<LocalizationManager.Language, string> localizationTextsDict;
        string textInLanguage;

        // Use this for initialization
        void Start()
        {
            LocalizationManager.LanguageChangedEvent += ChangeLanguage;
            if (autoInit)
            {
                ChangeLanguage(LocalizationManager.SelectedLanguage);
            }
        }

        public void Configure(string config)
        {

        }

        public void Init()
        {
            ChangeLanguage(LocalizationManager.SelectedLanguage);
        }

        void ChangeLanguage(LocalizationManager.Language newLanguage)
        {
            //Debug.Log("Localization Component Change Language: " + newLanguage);
            localizationTextsDict = new Dictionary<LocalizationManager.Language, string>();
            lang = newLanguage;
            foreach (LocalizedText locText in localizationList.LocalizedTexts)
            {
                localizationTextsDict.Add(locText.language, locText.text);
            }

            bool res = localizationTextsDict.TryGetValue(lang, out textInLanguage);
            if (res)
            {
                //if using TextMesh Pro Component
                if (TMP_UIElement)
                {
                    TMP_UIElement.SetText(textInLanguage);
                    TMP_UIElement.ForceMeshUpdate();
                }

                //if using Unity Text Component
                if (Text_UIElement) Text_UIElement.text = textInLanguage;

                //  GetComponent<Text>().text = textInLanguage;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}