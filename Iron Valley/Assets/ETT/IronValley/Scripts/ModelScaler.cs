using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ett.IronValley.Scripts
{
    public class ModelScaler : MonoBehaviour
    {
        public Vector3 ClosedScale = new Vector3(0, 0, 0);
        public Vector3 OpenScale = new Vector3(1, 1, 1);
        public float TimeToShow = 3f;
        private bool _isOpen;

        // Start is called before the first frame update
        void Start()
        {
            _isOpen = false;
            this.transform.localScale = ClosedScale;
        }

        public void Open(Action onEnd)
        {
            if (_isOpen) return;

            StartCoroutine(ScaleCor(ClosedScale, OpenScale, TimeToShow, onEnd));
        }

        public void Close(Action onEnd)
        {
            if (!_isOpen) return;
            StartCoroutine(ScaleCor(OpenScale, ClosedScale, TimeToShow, onEnd));
        }
        IEnumerator ScaleCor(Vector3 start, Vector3 end, float duration, Action onEnd)
        {
            var startTime = Time.time;
            while ((startTime + duration) > Time.time)
            {
                yield return null;
                ///TODO!
                var progress = (Time.time - startTime) / duration;
                this.transform.localScale = Vector3.Lerp(start, end, progress);
            }

            onEnd?.Invoke();
        }
        
    }
}
