using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    public int samplesNumber = 512;
    float[] samplesLeft, samplesRight;
    public int frequencyBandsNumber = 8;
    float[] frequencyBands, frequencyBandsHighest, bandBuffer;
    public float[] audioBand, audioBandBuffer;
    public float amplitude, amplitudeBuffer;
    float amplitudeHighest;
    public float audioProfile = 5f;

    public enum Channel { Stereo, Left, Right };
    public Channel channel = new Channel();

    float[] bufferDecrease;
    AudioSource _audioSource;
    void Awake()
    {
        samplesLeft = new float[samplesNumber];
        samplesRight = new float[samplesNumber];
        frequencyBands = new float[frequencyBandsNumber];
        frequencyBandsHighest = new float[frequencyBandsNumber];
        bandBuffer = new float[frequencyBandsNumber];
        bufferDecrease = new float[frequencyBandsNumber];
        audioBand = new float[frequencyBandsNumber];
        audioBandBuffer = new float[frequencyBandsNumber];
    }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitudes();
        AudioProfile(audioProfile);
    }

    void AudioProfile(float profile)
    {
        for (int i = 0; i < frequencyBandsNumber; i++)
        {
            frequencyBandsHighest[i] = profile;
        }
    }

    void GetAmplitudes()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;
        for (int i = 0; i < frequencyBandsNumber; i++)
        {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuffer += audioBandBuffer[i];
        }
        if (currentAmplitude > amplitudeHighest)
        {
            amplitudeHighest = currentAmplitude;
        }
        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < frequencyBandsNumber; i++)
        {
            if (frequencyBands[i] >= frequencyBandsHighest[i])
            {
                frequencyBandsHighest[i] = frequencyBands[i];
            }
            audioBand[i] = (frequencyBands[i] / frequencyBandsHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / frequencyBandsHighest[i]);
        }
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(samplesLeft, 0, FFTWindow.Blackman);
        _audioSource.GetSpectrumData(samplesRight, 1, FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for (int i = 0; i < frequencyBandsNumber; i++)
        {
            if (frequencyBands[i] >= bandBuffer[i])
            {
                bandBuffer[i] = frequencyBands[i];
                bufferDecrease[i] = 0.005f;
            }
            if (frequencyBands[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.05f;
            }
        }
    }

    void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < frequencyBandsNumber; i++)
        {
            float sampleAvg = 0f;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == frequencyBandsNumber - 1)
            {
                sampleCount += samplesNumber % (count + sampleCount);
            }

            for (int j = 0; j < sampleCount; j++)
            {
                switch (channel)
                {
                    case Channel.Left:
                        sampleAvg += samplesLeft[count] * (count + 1);
                        break;
                    case Channel.Right:
                        sampleAvg += samplesRight[count] * (count + 1);
                        break;
                    default:
                        sampleAvg += samplesLeft[count] + samplesRight[count] * (count + 1);
                        break;
                }
                count++;
            }

            sampleAvg /= count;
            frequencyBands[i] = sampleAvg * 10;
        }
    }
}
