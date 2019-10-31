using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ett.Scripts
{
    public static class Utilities
    {
        public enum FadeType { IN, OUT }

        public enum Easing { LIN, EXP }

        public static void FadeOut(CanvasGroup cg, Easing ease, float time, Action onEnd)
        {
            SceneManager.Instance.StartCoroutine(FadeCor(FadeType.OUT, ease, cg, time, onEnd));
        }

        public static void FadeIn(CanvasGroup cg, Easing ease, float time, Action onEnd)
        {
            SceneManager.Instance.StartCoroutine(FadeCor(FadeType.IN, ease, cg, time, onEnd));
        }

        static IEnumerator FadeCor(FadeType type, Easing ease, CanvasGroup cg, float duration, Action onEnd)
        {
            // Debug.Log($"starting fade {type} cor");
            var startTime = Time.time;

            while ((startTime + duration) > Time.time)
            {
                yield return null;
                var progress = (Time.time - startTime) / duration;
                cg.alpha = type.Equals(FadeType.IN) ? progress : 1 - progress;
                var a = cg.alpha;

                switch (ease)
                {
                    case Easing.LIN:
                        cg.alpha = a;
                        break;
                    case Easing.EXP:
                        cg.alpha = a * a * a * (a * (6f * a - 15f) + 10f);
                        break;
                    default:
                        break;
                }

                //Debug.Log(cg.alpha);
            }
            cg.alpha = type.Equals(FadeType.IN) ? 1 : 0;
            cg.blocksRaycasts = type.Equals(FadeType.IN) ? true : false;
            cg.interactable = type.Equals(FadeType.IN) ? true : false;
            onEnd?.Invoke();
        }



        public static void WaitThenDoAction(float waitTime, Action onEnd)
        {
            SceneManager.Instance.StartCoroutine(WaitThenDoActionCor(waitTime, onEnd));
        }

        static IEnumerator WaitThenDoActionCor(float waitTime, Action onEnd)
        {
            yield return new WaitForSeconds(waitTime);
            onEnd.Invoke();
        }
    }
}
