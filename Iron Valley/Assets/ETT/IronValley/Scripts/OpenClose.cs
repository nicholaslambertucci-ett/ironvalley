using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ett.IronValley.Scripts
{
    public class OpenClose : MonoBehaviour
    {
        [Tooltip("The rect transform to move")]
        public RectTransform RectTransform;
        [Tooltip("The canvas group to fade")]
        public CanvasGroup CanvasGroup;
        [Tooltip("The current status. On startup initializes the selected status")]
        public bool IsOpen;
        [Tooltip("If the animation has to move check this bool")]
        public bool Move;
        [Tooltip("If the animation has to fade check this bool")]
        public bool Fade;
        [Tooltip("The time of the animation")]
        public float OpenCloseTime;
        [Header("The anchors to apply to the rect transform")]
        public Vector2 CloseAnchorMin;
        public Vector2 CloseAnchorMax;
        public Vector2 OpenAnchorMin;
        public Vector2 OpenAnchorMax;
        [Header("Events")]
        public UnityEvent EventOnOpenStart;
        public UnityEvent EventOnOpenEnd;
        public UnityEvent EventOnCloseStart;
        public UnityEvent EventOnCloseEnd;


        private bool _isAnimating;

        // Start is called before the first frame update
        void Start()
        {
            if (Move && !RectTransform) RectTransform = this.GetComponent<RectTransform>();

            //try to get a canvas group in this gameobject if not 
            if (Fade && !CanvasGroup) CanvasGroup = this.GetComponent<CanvasGroup>();

            if (IsOpen)
            {
                if (Move)
                {
                    RectTransform.anchorMin = OpenAnchorMin;
                    RectTransform.anchorMax = OpenAnchorMax;
                }
                if (Fade)
                {
                    CanvasGroup.alpha = 1;
                    CanvasGroup.interactable = true;
                    CanvasGroup.blocksRaycasts = true;
                }
            }
            else
            {
                if (Move)
                {
                    RectTransform.anchorMin = CloseAnchorMin;
                    RectTransform.anchorMax = CloseAnchorMax;
                }
                if (Fade)
                {
                    CanvasGroup.alpha = 0;
                    CanvasGroup.interactable = false;
                    CanvasGroup.blocksRaycasts = false;
                }
            }
        }

        public void Open()
        {
            EventOnOpenStart?.Invoke();
            StartCoroutine(OpenCloseRoutine(true, OpenCloseTime, () =>
            {
            /// Debug.Log($"{Time.time} end opening"); 
            EventOnOpenEnd?.Invoke();
            }));
        }

        public void Close()
        {
            EventOnCloseStart?.Invoke();
            StartCoroutine(OpenCloseRoutine(false, OpenCloseTime, () =>
            {
            //Debug.Log($"{Time.time} end closing"); 
            EventOnCloseEnd?.Invoke();
            }));
        }

        IEnumerator OpenCloseRoutine(bool toOpen, float time, Action onEnd)
        {
            if (_isAnimating) yield break;

            _isAnimating = true;

            if (Fade && CanvasGroup)
            {
                //disable interaction in any case when animating
                CanvasGroup.interactable = false;
                CanvasGroup.blocksRaycasts = false;
            }

            var startTime = Time.time;
            // Debug.Log($"startTime: {startTime}, toOpen? {toOpen}");

            while ((Time.time - startTime) < time)
            {
                var progress = (Time.time - startTime) / time;
                // Debug.Log("PROGRESS: " + progress);

                if (Move && RectTransform)
                {
                    RectTransform.anchorMin = Vector2.Lerp(CloseAnchorMin, OpenAnchorMin, toOpen ? progress : 1 - progress);
                    RectTransform.anchorMax = Vector2.Lerp(CloseAnchorMax, OpenAnchorMax, toOpen ? progress : 1 - progress);
                }
                if (Fade && CanvasGroup)
                {
                    CanvasGroup.alpha = toOpen ? progress : 1 - progress;
                }
                yield return null;
            }

            if (Move && RectTransform)
            {
                RectTransform.anchorMin = toOpen ? OpenAnchorMin : CloseAnchorMin;
                RectTransform.anchorMax = toOpen ? OpenAnchorMax : CloseAnchorMax;
            }
            if (Fade && CanvasGroup)
            {
                CanvasGroup.alpha = toOpen ? 1 : 0;
                CanvasGroup.interactable = toOpen ? true : false;
                CanvasGroup.blocksRaycasts = toOpen ? true : false;
            }

            //Debug.Log($"endTime: {Time.time}");
            onEnd.Invoke();

            IsOpen = toOpen;
            _isAnimating = false;
        }
    }
}