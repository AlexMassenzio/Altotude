using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrogramTest : MonoBehaviour {

    private const int SPECTRUM_SIZE = 128;
	public float SMOOTHING_MODIFIER = 2f;

    Queue<float[]> spectrumHistory;
    float[] spectrumSum;
	public float[] smoothSpectrumAvg;

    void Start()
    {
        spectrumSum = new float[SPECTRUM_SIZE];
		smoothSpectrumAvg = new float[SPECTRUM_SIZE];
        for(int i = 0; i < spectrumSum.Length; i++)
        {
            spectrumSum[i] = 0f;
			smoothSpectrumAvg[i] = 0f;
        }

        spectrumHistory = new Queue<float[]>();

        /*AudioSource audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 100, 44100);
        audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        Debug.Log("start playing... position is " + Microphone.GetPosition(null));
        audio.Play();*/
    }

    void Update()
    {
        float[] currentSpectrum = new float[SPECTRUM_SIZE];

        AudioListener.GetSpectrumData(currentSpectrum, 0, FFTWindow.Hamming);

        spectrumHistory.Enqueue(currentSpectrum);

        for (int i = 0; i < spectrumSum.Length; i++)
        {
            spectrumSum[i] += currentSpectrum[i];
            if (spectrumHistory.Count > 25)
            {
                spectrumSum[i] -= spectrumHistory.Peek()[i];
            }
        }

        if(spectrumHistory.Count > 25)
        {
            spectrumHistory.Dequeue();
        }
        
        float[] spectrumAverage = new float[SPECTRUM_SIZE];

        for (int i = 0; i < spectrumAverage.Length; i++)
        {
            spectrumAverage[i] = spectrumSum[i] / spectrumSum.Length;
        }
		
		for (int i = 1; i < currentSpectrum.Length / 2.5f - 1; i++)
        {
			smoothSpectrumAvg[i] += (spectrumAverage[i] - smoothSpectrumAvg[i]) * Time.deltaTime * SMOOTHING_MODIFIER;
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1)*2f, smoothSpectrumAvg[i - 1] * 25 - 6, 1), new Vector3(Mathf.Log(i)*2f, smoothSpectrumAvg[i] * 25 - 6, 1), Color.red);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrumAverage[i - 1] * 20 - 8, 1), new Vector3(Mathf.Log(i), spectrumAverage[i] * 20 - 8, 1), Color.blue);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), currentSpectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), currentSpectrum[i] - 10, 1), Color.green);
        }
    }
}