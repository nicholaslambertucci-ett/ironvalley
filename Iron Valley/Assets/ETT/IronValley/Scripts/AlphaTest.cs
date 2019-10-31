using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets the minimum threshold of the Image alpha test to avoid clicks in transparent areas
/// </summary>
[RequireComponent(typeof(Image))]
public class AlphaTest : MonoBehaviour
{
    public bool HitTestActive;
    public float MinimumThreshold = .0001f;

    // Start is called before the first frame update
    void Start()
    {
        if (HitTestActive)
        {
            // Debug.Log(this.GetComponent<Image>().alphaHitTestMinimumThreshold);
            this.GetComponent<Image>().alphaHitTestMinimumThreshold = MinimumThreshold;
        }
    }
}
