using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;


namespace Ett.Vdt.Utilities
{
    public static class TextureUtilities
    {


        public static Texture LoadTexture(string mediaPath)
        {
            byte[] fileData = new byte[] { };
            fileData = File.ReadAllBytes(mediaPath);

            if (fileData == null) return null;

            var texture = new Texture2D(2, 2, TextureFormat.RGB24, false, true);

            texture.LoadImage(fileData);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.Apply();

            return texture;
        }

        public static Texture LoadTexture(byte[] bytes)
        {
            var texture = new Texture2D(2, 2, TextureFormat.RGB24, false, true);
            texture.LoadImage(bytes);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.Apply();
            return texture;
        }

        public static Texture LoadTexture(string mediaPath, TextureFormat format, bool useMipmaps, TextureWrapMode wrapMode)
        {
            var fileData = File.ReadAllBytes(mediaPath);
            var texture = new Texture2D(2, 2, format, useMipmaps, true);
            texture.LoadImage(fileData);
            texture.wrapMode = wrapMode;
            texture.Apply();
            return texture;
        }

        public static IEnumerator LoadLocalTextureCoroutine(string path, UnityAction<Texture2D> completeCallback = null)
        {

#if UNITY_ANDROID && !UNITY_EDITOR
            path = $"file://{path}";
#endif
            /*
            using (var request = UnityWebRequest.Get(path))
            {
                request.downloadHandler = new DownloadHandlerTexture();
                yield return request.SendWebRequest();

                while (request.isDone) yield return null;
                
                var texture = DownloadHandlerTexture.GetContent(request);
                if (texture == null)
                    yield break;

                completeCallback?.Invoke(texture);
            }*/

            Debug.Log("loading texture from path: "+path);
            var www = UnityWebRequestTexture.GetTexture(path);
            yield return www.SendWebRequest();

            while (!www.isDone) yield return null;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
                yield break;
            }

            completeCallback?.Invoke(((DownloadHandlerTexture)www.downloadHandler).texture);

        }

        public static IEnumerator LoadLocalTextureCoroutine(string path, RawImage rawImage,
            AspectRatioFitter aspectRatioFitter = null, Color? imageColor = null,
            UnityAction<RawImage, AspectRatioFitter, Color?> completeCallback = null)
        {
#if UNITY_ANDROID
            path = $"file://{path}";
#endif
            using (var request = UnityWebRequest.Get(path))
            {
                request.downloadHandler = new DownloadHandlerTexture();
                yield return request.SendWebRequest();

                var texture = DownloadHandlerTexture.GetContent(request);
                if (texture == null)
                    yield break;

                rawImage.texture = texture;

                if (imageColor.HasValue)
                    rawImage.color = imageColor.Value;

                if (aspectRatioFitter != null)
                    aspectRatioFitter.aspectRatio = (float)texture.width / texture.height;

                completeCallback?.Invoke(rawImage, aspectRatioFitter, imageColor);
            }
        }

        public static RawImage SetAlpha(RawImage img, float a)
        {
            var tempColor = img.color;
            tempColor.a = a; //1f makes it fully visible, 0f makes it fully transparent.
            img.color = tempColor;

            return img;
        }

        public static void ClearTexture(Texture texture, RenderTexture rt)
        {
            Graphics.Blit(texture, rt);
        }
    }
}