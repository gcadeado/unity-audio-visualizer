using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnAudio : MonoBehaviour
{
    public int band;

    public AudioPeer audioPeer;
    public float minIntensity, maxIntensity;
    Light _light;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        _light.intensity = audioPeer.audioBandBuffer[band] * (maxIntensity - minIntensity) + minIntensity;
    }
}
