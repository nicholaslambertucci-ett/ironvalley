using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Ett.IronValley.Scripts
{
    public class VideoLoader : MonoBehaviour
    {
        // public Color OutColor;
        // public Color InColor;

        private VideoPlayer videoPlayer;
        //video to load data
        public Video video;
        public RawImage rawImage;
        public float TimeToFade = 1f;

        public bool SetSizeAndPosition = false;

        public Vector2 RenderTextureSize = new Vector2(1920, 1080);

        enum FadeType { IN, OUT };

        //public for debug purpouse
        public bool IsVisible;

        // Start is called before the first frame update
        void Start()
        {
            videoPlayer = this.GetComponent<VideoPlayer>();
        }

        public void LoadVideo()
        {
            LoadVideo(video);
        }

        public void LoadVideo(Video video)
        {
            //look for the file to load in persistent data path
            var _fileName = Path.Combine(Directory.GetCurrentDirectory(), "Videos", video.Path);

            if (string.IsNullOrWhiteSpace(video.Path) || !File.Exists(_fileName) || videoPlayer == null) return;

            if (SetSizeAndPosition) SetRawImageSizeAndPos(video.Position, video.Size);

            videoPlayer.renderMode = VideoRenderMode.RenderTexture;

            if (videoPlayer.targetTexture != null)
            {
                videoPlayer.targetTexture.Release();
                Destroy(videoPlayer.targetTexture);

                videoPlayer.targetTexture = null;
            }

            videoPlayer.targetTexture = new RenderTexture(1, 1, 0);

            //Path.Combine(Application.persistentDataPath, video.Path);

            LoadVideo(_fileName);
        }

        public void LoadVideo(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || videoPlayer == null) return;

            videoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
            videoPlayer.loopPointReached += EndReached;
            videoPlayer.seekCompleted += Source_seekCompleted;

            LoadVideoPlayer(videoPlayer, path);
            //Debug.Log("Finished loading video scene with path: " + path + " waiting for prepareCompleted");
        }

        //valid only if recttransform is anchored at lower left corner
        public void SetRawImageSizeAndPos(Vector2 pos, Vector2 size)
        {
            var rt = this.rawImage.gameObject.GetComponent<RectTransform>();
            rt.anchoredPosition = pos;
            rt.sizeDelta = size;
        }

        private void LoadVideoPlayer(VideoPlayer v, string videoPath)
        {
            //  Debug.Log("[VIDEO] loading video " + videoPath);
            v.url = videoPath;
            v.Prepare();
        }

        private void VideoPlayer_prepareCompleted(VideoPlayer source)
        {
            videoPlayer.prepareCompleted -= VideoPlayer_prepareCompleted;
            //Debug.Log("[VIDEO] prepare completed for video " + source.url);

            RenderTexture tex;
            tex = new RenderTexture(source.texture.width, source.texture.height, 16, RenderTextureFormat.ARGB32);
            tex.Create();
            tex.wrapMode = TextureWrapMode.Repeat;

            Destroy(source.targetTexture);
            source.targetTexture = null;
            source.targetTexture = tex;
            rawImage.texture = tex;
            //Debug.Log("rawImage wrap mode: " + rawImage.texture.wrapMode);

            if (source.time != 0)
            {
                source.time = 0;
            }
            else
            {
                source.Play();
                FadeIn(TimeToFade, null);
                //StartCoroutine(FadeInVideo(TimeToFade));
            }
        }

        private void Source_seekCompleted(VideoPlayer source)
        {
            Debug.Log("Seek Completed, starting fade in");
            source.Play();
            source.seekCompleted -= Source_seekCompleted;
            //StartCoroutine(FadeInVideo(2));
            FadeIn(TimeToFade, null);
        }

        private void EndReached(VideoPlayer source)
        {
            videoPlayer.loopPointReached -= EndReached;
        }

        void FadeIn(float duration, Action onEnd)
        {
            StartCoroutine(Fade(FadeType.IN, duration, onEnd));
        }

        public void FadeOut(Action onEnd)
        {
            /*Debug.Log("Fading out checking GlobalAlpha: " + rawImage.material.GetFloat("_GlobalAlpha"));
            if (rawImage.material.GetFloat("_GlobalAlpha") > float.Epsilon)
            {
                StartCoroutine(FadeOutVideo(TimeToFade, onEnd));
            }
            else
            {
                Debug.Log("Not fading out, global alpha is almost 0!");
                if (onEnd != null) onEnd.Invoke();
            }*/

            StartCoroutine(Fade(FadeType.OUT, TimeToFade, onEnd));
        }

        void FadeOut(float duration, Action onEnd)
        {
            StartCoroutine(Fade(FadeType.OUT, duration, onEnd));
        }

        IEnumerator Fade(FadeType fadeType, float duration, Action onEnd)
        {
            //skip fading in if video is already visible
            // or skip fading out if video is already not visible
            if ((IsVisible && fadeType.Equals(FadeType.IN)) || (!IsVisible && fadeType.Equals(FadeType.OUT)))
            {
                onEnd?.Invoke();
                yield break;
            }

            IsVisible = fadeType == FadeType.IN ? IsVisible : false;

            float startTime = Time.time;
            float progress = 0;
            float elapsedTime = 0f;
            var audio = videoPlayer.gameObject.GetComponent<AudioSource>();

            if (rawImage.material != null)
                rawImage.material.SetFloat("_GlobalAlpha", fadeType == FadeType.IN ? 0 : 1);

            while (progress <= 1)
            {
                elapsedTime += Time.deltaTime;
                progress = elapsedTime / duration;

                if (rawImage.material != null)
                    rawImage.material.SetFloat("_GlobalAlpha", fadeType == FadeType.IN ? progress : 1f - progress);

                rawImage.color = new Color(1, 1, 1, Mathf.Clamp(fadeType == FadeType.IN ? progress : 1f - progress, 0, 1));
                if (audio) audio.volume = Mathf.Clamp(fadeType == FadeType.IN ? progress : 1f - progress, 1f, 0f);

                // Debug.Log("fade "+ fadeType+ " progress: " + progress + " Color: " + rawImage.color);

                yield return null;
            }

            if (rawImage.material != null)
                rawImage.material.SetFloat("_GlobalAlpha", fadeType == FadeType.IN ? 1 : 0);

            rawImage.color = new Color(1, 1, 1, fadeType == FadeType.IN ? 1 : 0);

            IsVisible = fadeType == FadeType.IN ? true : false;

            onEnd?.Invoke();
        }
        /*
        IEnumerator FadeOutVideo(float duration, Action onEnd)
        {
            IsVisible = false;

            float startTime = Time.time;
            float progress = 0;
            float elapsedTime = 0f;
            var audio = videoPlayer.gameObject.GetComponent<AudioSource>();
            if (rawImage.material != null)
                rawImage.material.SetFloat("GlobalAlpha", 1);
            while (progress <= 1)
            {
                elapsedTime += Time.deltaTime;
                progress = elapsedTime / duration;
                //Debug.Log(rawImage.material.GetFloat("_GlobalAlpha"));
                if (rawImage.material != null)
                    rawImage.material.SetFloat("_GlobalAlpha", 1f - progress);
                rawImage.color = Color.Lerp(InColor, OutColor, 1f - progress);
                // VideoInstancedMat.SetColor("_Tint", Color.Lerp(InColor, OutColor, progress));
                if (audio) audio.volume = Mathf.Clamp(1f - progress, 1f, 0f);
                //Debug.Log("progress: " + progress + "Color: " + VideoSkybox.GetColor("_Tint"));

                yield return null;
            }
            if (rawImage.material != null)
                rawImage.material.SetFloat("_GlobalAlpha", 0);
            rawImage.color = new Color(OutColor.r, OutColor.g, OutColor.b, 0);

            onEnd?.Invoke();
        }



        IEnumerator FadeInVideo(float duration)
        {
            float startTime = Time.time;
            float progress = 0;
            float elapsedTime = 0f;
            var audio = videoPlayer.gameObject.GetComponent<AudioSource>();//.GetTargetAudioSource(0);
            if (rawImage.material != null)
                rawImage.material.SetFloat("_GlobalAlpha", 0);

            //  VideoInstancedMat.SetColor("_Tint", OutColor);
            yield return new WaitForEndOfFrame();

            while (progress <= 1)
            {
                elapsedTime += Time.deltaTime;
                progress = elapsedTime / duration;

                if (rawImage.material != null)
                    rawImage.material.SetFloat("_GlobalAlpha", progress);
                rawImage.color = Color.Lerp(OutColor, InColor, progress);

                //  VideoInstancedMat.SetColor("_Tint", Color.Lerp(OutColor, InColor, progress));
                if (audio) audio.volume = Mathf.Clamp(progress, 0f, 1f);
                //Debug.Log("progress: "+progress+ "Color: "+VideoSkybox.GetColor("_Tint"));
                yield return null;
            }

            IsVisible = true;

            rawImage.material.SetFloat("GlobalAlpha", 1);
        }*/
    }
}