using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    public AudioPeer audioPeer;

    public int band = 0;

    public bool useBandBuffer = true;

    Material m;

    [ColorUsage(true, true)]
    public Color initialColor;

    public float startScale, scaleMultiplier;
    // Update is called once per frame

    void Start()
    {
        m = GetComponent<MeshRenderer>().materials[0];
        m.SetColor("_EmissionColor", initialColor);
    }
    void Update()
    {
        if (useBandBuffer)
        {

            transform.localScale = new Vector3(transform.localScale.x, (audioPeer.audioBandBuffer[band] * scaleMultiplier) + startScale, transform.localScale.z);
            Color color = new Color(initialColor.r * audioPeer.audioBandBuffer[band], initialColor.g * audioPeer.audioBandBuffer[band], initialColor.b * audioPeer.audioBandBuffer[band]);
            m.SetColor("_EmissionColor", color);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, (audioPeer.audioBand[band] * scaleMultiplier) + startScale, transform.localScale.z);
            Color color = new Color(audioPeer.audioBand[band], audioPeer.audioBand[band], audioPeer.audioBand[band]);
            m.SetColor("_EmissionColor", color);
        }
    }
}
