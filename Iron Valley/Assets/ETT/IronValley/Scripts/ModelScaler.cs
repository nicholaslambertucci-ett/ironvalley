using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScaler : MonoBehaviour
{
    public Vector3 StartScale = new Vector3(0, 0, 0);
    public Vector3 EndScale = new Vector3(1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = StartScale;
    }

    IEnumerator ScaleCor(Vector3 start, Vector3 end, float duration, Action onEnd)
    {
        var startTime = Time.time;
        while ((startTime + duration) > Time.time)
        {
            yield return null;
            ///TODO!
            var progress = (Time.time - startTime) / duration;
            var newScale = new Vector3();
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, newScale, progress);
        }

        onEnd.Invoke();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
