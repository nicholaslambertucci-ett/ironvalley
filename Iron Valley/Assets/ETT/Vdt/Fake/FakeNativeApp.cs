using UnityEngine;

namespace Ett.Vdt.Fake
{
    public class FakeNativeApp : MonoBehaviour
    {
        public static FakeNativeApp Instance { get; private set; }

        public void CallFakeNative(string methodName)
        {
            if (this._logicGameObject == null || !this._logicGameObject)
            {
                Debug.LogWarningFormat("Calling {0} on unset logic gameobject", methodName);
                return;
            }

            this._logicGameObject.SendMessage(methodName);
        }

        public void CallFakeNative(string methodName, object value)
        {
            if (this._logicGameObject == null || !this._logicGameObject)
            {
                Debug.LogWarningFormat("Calling {0} on unset logic gameobject", methodName);
                return;
            }

            this._logicGameObject.SendMessage(methodName, value);
        }

        public void UnitySendMessage(string gameObjectName, string methodName)
        {
            var go = GameObject.Find(gameObjectName);
            if (go == null || !go)
            {
                Debug.LogFormat("UnitySendMessage {0} - Cannot find gameObject {1}", methodName, gameObjectName);
                return;
            }

            go.SendMessage(methodName);
        }

        public void UnitySendMessage(string gameObjectName, string methodName, object value)
        {
            var go = GameObject.Find(gameObjectName);
            if (go == null || !go)
            {
                Debug.LogFormat("UnitySendMessage {0} - Cannot find gameObject {1}", methodName, gameObjectName);
                return;
            }

            go.SendMessage(methodName, value);
        }


        [SerializeField] private GameObject _logicGameObject;

        // Use this for initialization
        private void Start()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
                
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
}
