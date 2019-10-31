using Ett.Scripts.Localization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ett.Scripts
{
    public class SceneManager : MonoBehaviour
    {
        private static SceneManager _instance;

        public static SceneManager Instance { get { return _instance; } }

        public float TimeToFade;

        public CanvasGroup SplashCanvasGroup;
        public CanvasGroup MainCanvasGroup;

        [Header("Language")]
        public Button LanguageButton;

        [Header("Webcam")]
        public CanvasGroup WebcamCanvas;
        public RawImage WebcamRawImage;

        public PoiViewController[] Pois;

        public static UnityEvent<Guid> PoiVisitedEvent;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            DontDestroyOnLoad(this.gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            //disable multitouch to avoid multiple clicks on map!
            Input.multiTouchEnabled = false;
            /*
            SheetItem MainItem = new SheetItem()
            {
                Title = "Iron Valley",
                Subtitle = "App",
                Description = "",
                Type = Type.SheetMain,
                MediaPath = "",
                Items = new SheetItem[] {
                    new SheetItem(){
                        Title = "Ferriere FIAT",
                        Subtitle = "POI_1",
                        Description = "Test description",
                        MediaPath = "",
                        Items = new SheetItem[]{
                            new SheetItem(){
                                Title = "",
                                Type = Type.Sheet_Link,
                                MediaPath = "external link",

                            },
                            new SheetItem(){
                                Title = "",
                                Type = Type.Sheet_Link,
                                MediaPath = "external link",

                            }
                        }
                        //Type = Type.
                    },
                    new SheetItem(){

                    },
                    new SheetItem(){

                    }
                }
            };
            */

            IronPOI[] ironPOIs = new IronPOI[]{
                new IronPOI(){
                    Title = "Lo stabilimento delle Officine Savigliano (SNOS)",
                    Description = "La Società Nazionale Officine Savigliano (SNOS), fondata a Savigliano nel 1879, aprì la sua sede a Torino nel 1899 per la produzione di costruzioni meccaniche, metalliche, elettriche, ferroviarie e tranviarie. Negli anni Venti del '900 questo stabilimento si estendeva su un'area di 55.000 metri quadrati, di cui 45.000 coperti, e al suo interno lavoravano oltre 1000 operai. Nel corso del Novecento fu teatro di grandi mobilitazioni operaie: gli scioperi del 1907, le occupazioni del cosiddetto \"Biennio rosso\" nel 1920-21, gli scontri insurrezionali per la Liberazione di Torino nel 1945. L'edificio, visibile oggi, è frutto dell’allargamento del 1917-1918 su progetto dall'architetto Enrico Bonicelli.",
                    Tag = "poi-1",
                    VideoMediaPath = "",
                    VideoLabel = "", //Title of the media
                    Uuid = Guid.NewGuid(),
                },
                new IronPOI(){
                    Title = "Lo stabilimento Michelin",
                    Description = "La Fabbrica Pneumatici Michelin, stabilimento italiano del gruppo francese eretto nel 1906, occupava la vasta area posta sud del fiume Dora, delimitata da Via Livorno, Corso Umbria e Via Daubree. Nei primi decenni di vita, al suo interno erano impiegati soprattutto operai, fabbri, meccanici e falegnami e il ciclo di lavorazione consisteva quasi completamente in operazioni manuali, dall’impastatura del caucciù per produrre la mescola alla pressatura della gomma sulle tele e all’estrazione degli pneumatici dagli stampi ancora caldi. Fino agli anni ’50 è lo stabilimento italiano più importante per la produzione di pneumatici per auto, biciclette, moto, mezzi pesanti, agricoli e aerei. Nel periodo 1930-1960 lo stabilimento subisce importanti trasformazioni e ingrandimenti, arrivando ad occupare nel 1970 circa 6000 dipendenti. L'impianto chiude la sua attività nel 1997.",
                    Tag = "poi-2",
                    Uuid = Guid.NewGuid(),
                },
                new IronPOI(){
                    Title = "Le Ferriere Fiat",
                    Description = "Inaugurato nel 1907 con la denominazione di Ferriere Piemontesi, lo stabilimento di proprietà del gruppo francese Ferriére-sous-Jougne fu acquisito nel 1917 dal gruppo Fiat. Destinato alla produzione e alla lavorazione dell'acciaio, si affermò presto come uno dei più importanti stabilimenti del gruppo industriale torinese. Nel corso della seconda guerra mondiale al suo interno vi lavoravano circa 5000 operai. Punto nevralgico dell'antifascismo e della Resistenza torinese, nell'aprile del 1945 le Ferriere Fiat furono teatro di violenti combattimenti tra formazioni partigiane e tedeschi nelle giornate dell'insurrezione di Torino. Nel 1978 le Ferriere confluirono nella Teksid, società che raggruppava tutte le attività metallurgiche e siderurgiche della Fiat. Dopo essere stata assorbita dalla Finsider (IRI) nel 1982, l'acciaieria venne chiusa definitivamente nel 1992 in seguito alla forte crisi che colpì la siderurgia italiana.",
                    Tag = "poi-3",
                    Uuid = Guid.NewGuid(),
                    Approfondimenti = new IronGallery(){
                        Items = new IronGalleryItem[]{
                            new IronGalleryItem(){
                                Title = "Item 1",
                                MediaPath = "Test.png",
                                ExternalLink = "WFT.it",
                            }
                        }
                    },
                    VideoLabel = "Video 1",
                    VideoMediaPath = "test.mp4",
                    MediaGallery = new IronGallery[]{
                        new IronGallery(){
                            Items = new IronGalleryItem[]{
                                new IronGalleryItem(){
                                    Title = "Item 1",
                                    MediaPath = "Test.png",
                                    ExternalLink = "https://www.google.com",
                                },
                            }
                        }
                    }
                }
            };

            Configure(ironPOIs);

            LanguageButton.onClick.AddListener(() =>
            {
                //TODO THIS SHOULD EXIT TO NATIVE APP TO CHANGE LANGUAGE AND DOWNLOAD CONTENTS
                Debug.Log($"Changing language! Current Language: {LocalizationManager.SelectedLanguage}");
                var newLan = LocalizationManager.SelectedLanguage.Equals(LocalizationManager.Language.Ita) ? LocalizationManager.Language.Eng : LocalizationManager.Language.Ita;
                LocalizationManager.Instance.ChangeLanguage(newLan);
            });          

            FadeInSplash();
        }

        public void Configure(IronPOI[] pois)
        {
            for (var i = 0; i < pois.Length; i++)
            {
                if (pois[i].Tag.Equals($"poi-{i + 1}")) //this should be useless
                    Pois[i].Configure(pois[i]);
            }
        }

        private void FadeInSplash()
        {
            Utilities.FadeIn(SplashCanvasGroup, Utilities.Easing.EXP, TimeToFade, () =>
            {
                FadeOutSplash();
            });
        }

        private void FadeOutSplash()
        {
            Utilities.FadeOut(SplashCanvasGroup, Utilities.Easing.EXP, TimeToFade, () =>
            {
                FadeInMap();
            });
        }

        private void FadeInMap()
        {
            Utilities.FadeIn(MainCanvasGroup, Utilities.Easing.EXP, TimeToFade, () =>
            {
                Debug.Log("interact!");
            });
        }

        public void OpenCamera()
        {

            // StartCoroutine(OpenCameraRoutine());

            StartCoroutine(WaitForCameraAuthorization(
              (b) =>
              {
                  if (b)
                  {
                      Debug.Log("camera authorized!");
                      WebCamTexture webcamTexture = new WebCamTexture();
                      //      webcamTexture.req
                      //      WebcamRawImage.texture = webcamTexture;

                      webcamTexture.Play();

                      Utilities.FadeIn(WebcamCanvas, Utilities.Easing.EXP, TimeToFade, () =>
                      {
                          Debug.Log("finished fading in camera!");
                      });
                  }
                  else
                  {
                      Debug.Log("no authorization!");
                  }
              }));


        }
        /*
        private IEnumerator OpenCameraRoutine()
        {
            yield return StartCoroutine(WaitForCameraAuthorization(
               (b) =>
               {
                   if (b)
                   {
                       WebCamTexture webcamTexture = new WebCamTexture();

                   }
                   else { 
                   
                   }
               }));
        }
        */
        private IEnumerator WaitForCameraAuthorization(Action<bool> onResult)
        {
            if (Application.HasUserAuthorization(UserAuthorization.WebCam)) { onResult(true); yield break; }

            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                Debug.Log("webcam authorized");
                onResult(true);
            }
            else
            {
                Debug.Log("webcam not authorized");
                onResult(false);
            }
        }
    }
}