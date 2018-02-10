using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrogramTest : MonoBehaviour {

    /*void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 100, 44100);
        audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        Debug.Log("start playing... position is " + Microphone.GetPosition(null));
        audio.Play();
    }*/

    void Update()
    {
        float[] spectrum = new float[512];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

        for (int i = 1; i < spectrum.Length / 1.5f - 1; i++)
        {
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] * 2 - 8, 1), new Vector3(Mathf.Log(i), spectrum[i] * 2 - 8, 1), Color.blue);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
        }
    }
}