﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{
    private AudioSource _audioSource;

    public float[] _samples = new float[512];
    public float[] _sampleBuffer = new float[512];
    private float[] _sampleBudderDecrease = new float[512];

    public float[] _freqBand = new float[8];

    public float[] _bandBuffer = new float[8];
    private float[] _bufferDecrease = new float[8];


    public float[] _freqBandHighest = new float[8];
    public float[] _audioBand = new float[8];
    public float[] _audioBandBuffer = new float[8];


    // Start is called before the first frame update
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
        SampleBuffer();
        CreateAudioBands();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }


    void MakeFrequencyBands()
    {
        int count = 0;
        for(int i = 0; i < 8; i++)
        {
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            float average = 0;

            if(i == 7)
            {
                sampleCount += 2;
            }

            for(int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                    count++;
            }

            average /= count;

            _freqBand[i] = average * 10;
        }
    }


    void BandBuffer()
    {
       for(int g = 0; g < 8; g++)
        {
            if(_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.01f;
            }
            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void SampleBuffer()
    {
        for (int g = 0; g < 512; g++)
        {
            if (_samples[g] > _sampleBuffer[g])
            {
                _sampleBuffer[g] = _samples[g];
                _sampleBudderDecrease[g] = 0.005f;
            }
            if (_samples[g] < _sampleBuffer[g])
            {
                _sampleBuffer[g] -= _sampleBudderDecrease[g];
                _sampleBudderDecrease[g] *= 1.2f;
            }
        }
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if(_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }


    public bool CheckBand(float cutoff, int band)
    {
        if(_audioBand[band] > cutoff) {
            return true;
        } else
        {
            return false;
        }
    }

    public bool CheckAllBands(float cutoff)
    {
        bool b = false;
        for (int i = 0; i < 8; i++)
        {
            if(_audioBand[i] > cutoff)
            {
                b = true;
            }
        }
        return b;
    }

    public bool CheckBandAverage(float cutoff)
    {
        float avg = 0;
        for (int i = 0; i < 8; i++)
        {
            avg += _audioBand[i];
        }

        avg /= 8;

        if(avg > cutoff)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
