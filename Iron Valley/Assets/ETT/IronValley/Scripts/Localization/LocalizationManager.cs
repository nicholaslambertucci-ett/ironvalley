using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ett.Scripts.Localization
{

    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance;

        public static string lang_iso;
        public bool isAndroidTest;
        private string _lang_test;

        public string lang_test;

        bool destroyedByDuplicate;

        public enum Language
        {
            Ita,
            Eng,
            Fra,
            Esp,
            Por,
            Deu,
            Kor,
            Chi,
            Jap,
            Rus
        }

        public static Language SelectedLanguage;
        public delegate void LanguageChanged(Language newLanguage);
        public static event LanguageChanged LanguageChangedEvent;

        IEnumerator WaitForLanguageConfiguration()
        {
            yield return null;
            //   yield return new WaitUntil(() => !string.IsNullOrEmpty(lang_iso));
            Init();
            ChangeLanguage(lang_iso);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                //DontDestroyOnLoad(this.gameObject);
                Instance = this;
                // lang_iso = string.IsNullOrEmpty(MainController.Language)? "en" : MainController.Language;
            }
            else
            {
                this.destroyedByDuplicate = true;
                Destroy(this);
                return;
            }
        }

        //  private Language currLanguage;
        // Use this for initialization
        void Init()
        {
            if (isAndroidTest)
            {
                //Debug.Log("language =" + lang_test);
                lang_iso = lang_test;
                ChangeLanguage(lang_iso);
            }
        }
        void Start()
        {
            //Init();
            StartCoroutine(WaitForLanguageConfiguration());
        }
        public void ChangeLanguage(string language_iso)
        {
            Debug.Log("language = " + language_iso);
            switch (language_iso)
            {
                case "it":
                    SelectedLanguage = Language.Ita;
                    break;
                case "en":
                    SelectedLanguage = Language.Eng;
                    break;
                case "fr":
                    SelectedLanguage = Language.Fra;
                    break;
                case "es":
                    SelectedLanguage = Language.Esp;
                    break;
                case "pt":
                    SelectedLanguage = Language.Por;
                    break;
                case "de":
                    SelectedLanguage = Language.Deu;
                    break;
                case "zh":
                    SelectedLanguage = Language.Chi;
                    break;
                case "ru":
                    SelectedLanguage = Language.Rus;
                    break;
                case "ja":
                    SelectedLanguage = Language.Jap;
                    break;
                case "ko":
                    SelectedLanguage = Language.Kor;
                    break;

            }
            LanguageChangedEvent(SelectedLanguage);
        }

        public void ChangeLanguage(Language language)
        {
            SelectedLanguage = language;
            LanguageChangedEvent(language);
        }

        /// <summary>
        /// Gets the string from a Localization Asset for the currently selected language
        /// </summary>
        /// <param name="list">the list to check</param>
        /// <returns>The string, if found, or an empty string if not found</returns>
        public static string GetCurrentLanguageStringFromAsset(LocalizationListManager list)
        {
            var res = "";
            foreach (var lt in list.LocalizedTexts)
            {
                if (lt.language.Equals(SelectedLanguage)) {
                    res = lt.text; break; 
                }
            }
            return res;
        }

    }
}