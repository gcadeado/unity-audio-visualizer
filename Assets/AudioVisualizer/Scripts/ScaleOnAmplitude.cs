using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    public AudioPeer audioPeer;

    public bool useBandBuffer = true;

    public float startScale, maxScale;

    void Update()
    {
        float amplitudeBufferScale = audioPeer.amplitudeBuffer * maxScale + startScale;
        float amplitudeScale = audioPeer.amplitude * maxScale + startScale;

        if (useBandBuffer)
        {
            transform.localScale = new Vector3(amplitudeBufferScale, amplitudeBufferScale, amplitudeBufferScale);
        }
        else
        {
            transform.localScale = new Vector3(amplitudeScale, amplitudeScale, amplitudeScale);
        }
    }
}
