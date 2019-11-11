using Ett.IronValley.Scripts.Data;
using Ett.Scripts.Localization;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UTI = Ett.IronValley.Scripts.Utilities.Utilities;

namespace Ett.IronValley.Scripts
{
    public class MainManager : MonoBehaviour
    {
        private static MainManager _instance;
        public static MainManager Instance { get { return _instance; } }

        public float TimeToFade;

        public CanvasGroup SplashCanvasGroup;
        public CanvasGroup MainCanvasGroup;
        public CanvasGroup MapCanvas; //MapCanvas starts visible inside the MainCanvasGroup

        public string ConfigFileName;

        // [Header("POIS")]
        // [Tooltip("Map Pois")]
        // public PoiViewController[] MapPois;

        // [Tooltip("GPS Pois")]
        //public GPSViewController[] GPSPois;

        [Header("Language")]
        public Button LanguageButton;

        [Header("Webcam Page")]
        public CanvasGroup WebcamCanvas;
        public RawImage WebcamRawImage;
        public OpenClose WebcamPage;
        public ModelScaler Prisma;

        [Header("Gallery of Galleries Page")]
        public OpenClose GalleriesPage;

        [Header("PoiManager")]
        public POIManager POIManager;

        WebCamTexture _webcamTexture;
        private bool _webcamAuthorized;
        private bool _webcamInitialized;

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

            var basePath = "";
#if UNITY_EDITOR
            basePath = "D:/ETT/PROJECTS/Iron Valley/UnityProjects/Iron Valley/Assets/ETT/IronValley/Graphics/assets/";
#elif UNITY_ANDROID && !UNITY_EDITOR
            basePath = Application.persistentDataPath;
#endif
            #region TEST_REGION
            IronPOI[] ironPOIs = new IronPOI[]{
                new IronPOI(){
                    Title = "Lo stabilimento delle Officine Savigliano (SNOS)",
                    Description = "La Società Nazionale Officine Savigliano (SNOS), fondata a Savigliano nel 1879, aprì la sua sede a Torino nel 1899 per la produzione di costruzioni meccaniche, metalliche, elettriche, ferroviarie e tranviarie. Negli anni Venti del '900 questo stabilimento si estendeva su un'area di 55.000 metri quadrati, di cui 45.000 coperti, e al suo interno lavoravano oltre 1000 operai. Nel corso del Novecento fu teatro di grandi mobilitazioni operaie: gli scioperi del 1907, le occupazioni del cosiddetto \"Biennio rosso\" nel 1920-21, gli scontri insurrezionali per la Liberazione di Torino nel 1945. L'edificio, visibile oggi, è frutto dell’allargamento del 1917-1918 su progetto dall'architetto Enrico Bonicelli.",
                    Tag = "poi-1",
                    VideoMediaPath = "",
                    VideoLabel = "", //Title of the media
                    Uuid = Guid.Parse("6a4bdcc5-3091-4085-b815-1a12b8d7dd1f"),//Guid.NewGuid(),
                },
                new IronPOI(){
                    Title = "Lo stabilimento Michelin",
                    Description = "La Fabbrica Pneumatici Michelin, stabilimento italiano del gruppo francese eretto nel 1906, occupava la vasta area posta sud del fiume Dora, delimitata da Via Livorno, Corso Umbria e Via Daubree. Nei primi decenni di vita, al suo interno erano impiegati soprattutto operai, fabbri, meccanici e falegnami e il ciclo di lavorazione consisteva quasi completamente in operazioni manuali, dall’impastatura del caucciù per produrre la mescola alla pressatura della gomma sulle tele e all’estrazione degli pneumatici dagli stampi ancora caldi. Fino agli anni ’50 è lo stabilimento italiano più importante per la produzione di pneumatici per auto, biciclette, moto, mezzi pesanti, agricoli e aerei. Nel periodo 1930-1960 lo stabilimento subisce importanti trasformazioni e ingrandimenti, arrivando ad occupare nel 1970 circa 6000 dipendenti. L'impianto chiude la sua attività nel 1997.",
                    Tag = "poi-2",
                    Uuid = Guid.Parse("0e232ceb-bad8-43bc-923f-ba79d3d0b680"),//((Guid.NewGuid(),
                },
                new IronPOI(){
                    Title = "Le Ferriere Fiat",
                    Description = "Inaugurato nel 1907 con la denominazione di Ferriere Piemontesi, lo stabilimento di proprietà del gruppo francese Ferriére-sous-Jougne fu acquisito nel 1917 dal gruppo Fiat. Destinato alla produzione e alla lavorazione dell'acciaio, si affermò presto come uno dei più importanti stabilimenti del gruppo industriale torinese. Nel corso della seconda guerra mondiale al suo interno vi lavoravano circa 5000 operai. Punto nevralgico dell'antifascismo e della Resistenza torinese, nell'aprile del 1945 le Ferriere Fiat furono teatro di violenti combattimenti tra formazioni partigiane e tedeschi nelle giornate dell'insurrezione di Torino. Nel 1978 le Ferriere confluirono nella Teksid, società che raggruppava tutte le attività metallurgiche e siderurgiche della Fiat. Dopo essere stata assorbita dalla Finsider (IRI) nel 1982, l'acciaieria venne chiusa definitivamente nel 1992 in seguito alla forte crisi che colpì la siderurgia italiana.",
                    Tag = "poi-3",
                    Uuid = Guid.Parse("0da7a477-0f9a-4ac5-8ab7-e028822c682c"),// Guid.NewGuid(),
                    Insights = new IronGallery(){
                        Items = new IronGalleryItem[]{
                            new IronGalleryItem(){
                                Title = "Item 1",
                                MediaPath = basePath + "/Savignano.jpg",
                                ExternalLink = "WFT.it",
                            }
                        }
                    },
                    VideoLabel = "Video 1",
                    VideoMediaPath = "test.mp4",
                    MediaGalleryLabel = "Test Gallery",
                    MediaGallery = new IronGallery[]{
                        new IronGallery(){
                            ThumbPath = basePath + "/Savignano.jpg",
                            Title = "Ziocan",
                            Items = new IronGalleryItem[]{
                                new IronGalleryItem(){
                                    Title = "Ferriere Fiat",
                                    MediaPath = basePath + "/Ferriere Fiat.jpg",
                                    ExternalLink = "https://www.google.com",
                                },
                                new IronGalleryItem(){
                                    Title = "Savignano",
                                    MediaPath = basePath + "/Savignano.jpg",
                                    ExternalLink = "https://www.google.com",
                                },
                                new IronGalleryItem(){
                                    Title = "Michelin",
                                    MediaPath = basePath + "/Michelin.jpg",
                                    ExternalLink = "https://www.google.com",
                                }
                            }
                        },
                        new IronGallery(){
                            ThumbPath = basePath + "/Michelin.jpg",
                            Title = "Gallery Michelin",
                            Items = new IronGalleryItem[]{
                                new IronGalleryItem(){
                                    Title = "Michelin",
                                    MediaPath = basePath + "/Michelin.jpg",
                                    ExternalLink = "https://www.google.com",
                                },
                                new IronGalleryItem(){
                                    Title = "Savignano",
                                    MediaPath = basePath + "/Savignano.jpg",
                                    ExternalLink = "https://www.google.com",
                                },
                                new IronGalleryItem(){
                                    Title = "Ferriere Fiat",
                                    MediaPath = basePath + "/Ferriere Fiat.jpg",
                                    ExternalLink = "https://www.google.com",
                                }
                            }
                        }
                    }
                }
            };
#endregion TEST_REGION

            var PATH = "";

#if UNITY_EDITOR
            PATH = Application.dataPath;
#elif UNITY_ANDROID && !UNITY_EDITOR
            PATH = Application.persistentDataPath;
#endif
            var sr = new StreamReader(PATH + "/" + ConfigFileName);
            var fileContents = sr.ReadToEnd();
            sr.Close();


            var ironContainer = new IronPOIContainer() { IronPOIs = ironPOIs };

            //Debug.Log(JsonUtility.ToJson(ironContainer));

           // var ironContainer = JsonUtility.FromJson<IronPOIContainer>(fileContents);

            POIManager.Configure(ironContainer.IronPOIs);// ironPOIs);

            LanguageButton.onClick.AddListener(() =>
            {
                //TODO THIS SHOULD EXIT TO NATIVE APP TO CHANGE LANGUAGE AND DOWNLOAD CONTENTS
                Debug.Log($"Changing language! Current Language: {LocalizationManager.SelectedLanguage}");
                var newLan = LocalizationManager.SelectedLanguage.Equals(LocalizationManager.Language.Ita) ? LocalizationManager.Language.Eng : LocalizationManager.Language.Ita;
                LocalizationManager.Instance.ChangeLanguage(newLan);
            });

            FadeInSplash();
        }

#region MAIN_CONTAINERS_ANIMATION
        private void FadeInSplash()
        {
            UTI.FadeIn(SplashCanvasGroup, UTI.Easing.EXP, TimeToFade, () =>
            {
                FadeOutSplash();
            });
        }

        private void FadeOutSplash()
        {
            UTI.FadeOut(SplashCanvasGroup, UTI.Easing.EXP, TimeToFade, () =>
            {
                FadeInMap();
            });
        }

        private void FadeInMap()
        {
            UTI.FadeIn(MainCanvasGroup, UTI.Easing.EXP, TimeToFade, () =>
            {
                Debug.Log("interact!");
            });
        }
#endregion MAIN_CONTAINERS_ANIMATION

#region UI_PAGES_ANIMATION
        public void OpenWebCam(int poi_index)
        {
            Debug.Log($"poi {poi_index} opened webcam!");

            if (_webcamAuthorized && _webcamInitialized)
            {
                //play webcam again 
                _webcamTexture.Play();
                OpenWebcamUIElements();
            }
            else
            {
                StartCoroutine(WaitForWebcamAuthorization((b) =>
                {
                    if (b)
                    {
                        Debug.Log("camera authorized!");
                        _webcamAuthorized = true;

                        StartCoroutine(WaitForWebcamInitialization(() =>
                        {
                            Debug.Log("camera initialized");
                            _webcamInitialized = true;
                            OpenWebcamUIElements();
                        }));
                    }
                    else
                    {
                        Debug.Log("no authorization!");
                    }
                }));
            }
        }

        public void CloseWebcam()
        {
            _webcamTexture.Stop();
            var wbcoc = WebcamCanvas.GetComponent<OpenClose>();
            wbcoc.Close();
            Prisma.Close(null);
            WebcamPage.Close();
            UTI.FadeIn(MainCanvasGroup, UTI.Easing.EXP, wbcoc.OpenCloseTime, null);
        }

        /// <summary>
        /// Opens the Gallery Page
        /// </summary>
        public void OpenGalleries()
        {
            GalleriesPage.Open();
        }

        private void OpenWebcamUIElements()
        {
            //open the right windows and show 3dModel
            var timeToFade = WebcamCanvas.GetComponent<OpenClose>().OpenCloseTime;
            UTI.FadeOut(MainCanvasGroup, UTI.Easing.EXP, timeToFade, null);
            WebcamCanvas.GetComponent<OpenClose>().Open();
            Prisma.Open(() =>
            {
                Debug.Log("3d prism visible!");
                //TODO instantiate elements for the poi
                //var cui = WebcamPage.gameObject.GetComponent<WebcamUI>();
                // cui.Configure();

                //open the camera ui page 
                WebcamPage.Open();
            });
        }

        private IEnumerator WaitForWebcamInitialization(Action onEnd)
        {
            var highest = new Resolution();
            WebCamDevice deviceToUse = new WebCamDevice();

            foreach (var dev in WebCamTexture.devices)
            {
                //in editor this is always null 
                if (dev.availableResolutions == null)
                {
                    highest.width = 1920; highest.height = 1080;
                }
                else
                {
                    foreach (var res in dev.availableResolutions)
                    {
                        Debug.Log(dev.name + " res: " + res.ToString());
                        if (res.width > highest.width)
                        {
                            highest.width = res.width;
                            highest.height = res.height;
                            highest.refreshRate = res.refreshRate;
                            deviceToUse = dev;
                        }
                    }
                }
            }

            Debug.Log("highest resolution is: " + highest.ToString());

            //take the highest resolution
            _webcamTexture = new WebCamTexture(deviceToUse.name, highest.width, highest.height, highest.refreshRate);
            WebcamRawImage.texture = _webcamTexture;
            _webcamTexture.Play();

            //wait some frames for a real camera size
            while (_webcamTexture.width < 100)
            {
                yield return null;
            }
            //set aspect ratio of rawImage
            WebcamRawImage.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = _webcamTexture.width / (float)_webcamTexture.height;
            // Debug.Log("device webcamTexture: " + WebcamRawImage.texture.width + "x" + WebcamRawImage.texture.height + " fps: " + webcamTexture.requestedFPS);    
            onEnd.Invoke();
        }

        private IEnumerator WaitForWebcamAuthorization(Action<bool> onResult)
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

#endregion UI_PAGES_ANIMATION

#region POI_MANAGEMENT

        public static bool GetPoiVisited(System.Guid guid)
        {
            return POIManager.GetPoiVisited(guid);
        }

        public static void SetPoiVisited(System.Guid guid)
        {
            POIManager.SetPoiVisited(guid);
        }


#endregion POI_MANAGEMENT

        //useless
        /* private void OnApplicationQuit()
         {
             POIManager.SavePoiVisited();
         }*/
    }
}