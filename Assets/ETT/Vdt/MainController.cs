using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ett.Vdt
{
    public class MainController : MonoBehaviour
    {
        public static MainController Instance = null;

        public static string Language = string.Empty;

        public static string LastPayload;

        public static string LastOrientation = string.Empty;




        public void Configure(string config)
        {
            Debug.Log("received configure: " + config);
            try
            {
                var m = JsonUtility.FromJson<Data.Message>(config);

                var mType = m.Type?.ToLower();
                if (string.IsNullOrEmpty(mType))
                {
                    Debug.LogWarning("Cannot configure unity app. type is null or empty");
                    return;
                }

                switch (mType)
                {
                    case "currentorientation":
                        LastOrientation = m.Payload;
                        return;
                    case "language":
                        Language = m.Payload;
                        return;
                }

                LastPayload = m.Payload;

                SceneManager.LoadScene(m.Type);

            }
            catch (ArgumentException ex)
            {
                Debug.Log("Error deserializing message " + ex.Message);
            }
        }

        public void ExitFromApp()
        {
            /*
            var last = ScreenOrientation.Portrait;

            //if orientation is saved on start, go back to it
            if (LastOrientation != null) {

                try
                {
                    last = (ScreenOrientation)Enum.Parse(typeof(ScreenOrientation), LastOrientation);
                    Debug.Log("last orientation parsed: "+last);
                }
                catch (Exception ex) {
                    last = ScreenOrientation.Portrait;
                    Debug.LogError("error parsing orientation "+ex.Message);
                }
            }

            Screen.orientation = last;
            */

            //MsgController.OnExitToApp();

            _exitRequested = true;

            SceneManager.activeSceneChanged += this.SceneManagerOnActiveSceneChanged;
            //carica scena iniziale per reset
            //SceneManager.LoadScene(0);
            SceneManager.LoadScene(this._exitScene);
        }

        private static bool _exitRequested;

        //flag to check where the OnDestroy method call comes from
        private bool destroyedByDuplicate = false;

        [SerializeField] private string _exitScene = "ExitScene";

        private void OnDestroy()
        {
            if (this.destroyedByDuplicate)
            {
                Debug.Log(Time.time + " MainController.OnDestroy by duplicate");
                this.destroyedByDuplicate = false;
                return;
            }
        }

        private void Start()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(this.gameObject);
                Instance = this;
            }
            else
            {
                this.destroyedByDuplicate = true;
                Destroy(this);
                return;
            }


            this.SendReady();
        }

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene scene)
        {
            if (string.IsNullOrEmpty(scene.name) || !scene.name.Equals(this._exitScene) || !_exitRequested)
                return;

            SceneManager.activeSceneChanged -= this.SceneManagerOnActiveSceneChanged;
            _exitRequested = false;

            MsgController.OnExitToApp();
        }

#if VDT_V_2
        private void SendReady()
        {
            MsgController.OnReady(this.name);
        }

#else
        private void SendReady()
        {
            MsgController.OnReady();
        }
#endif
    }
}
