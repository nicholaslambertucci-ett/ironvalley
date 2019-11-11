using UnityEngine;

namespace Ett.IronValley.Scripts.Utilities
{
    public static class ExtensionsToTransform
    {
        public static T AttachChild<T>(this Transform parent, T child, bool worldPositionStays) where T : MonoBehaviour
        {
            var transform = child.transform;
            transform.SetParent(parent, worldPositionStays);
            return child;
        }

        public static void DestroyAndRemove(this Transform transform)
        {
            transform.SetParent(null);
            Object.Destroy(transform.gameObject);
        }

        public static Transform RemoveAllChildren(this Transform parent)
        {
            var childrenCount = parent.childCount;
            for (var i = 0; i < childrenCount; i++)
                parent.GetChild(0).DestroyAndRemove();
            return parent;
        }
    }
}