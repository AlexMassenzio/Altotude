using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeGenerator : MonoBehaviour {

	private float divisionOfSpectrum = 10f;

	SpectrogramTest st;
	public GameObject refObj;
	GameObject[,] blocks;

	public float sensitivity = 50;

	// Use this for initialization
	void Start () {
		st = FindObjectOfType<SpectrogramTest>().GetComponent<SpectrogramTest>();
		blocks = new GameObject[st.spectrumSize, st.spectrumSize];
		Debug.Log(st.smoothSpectrumAvg.Length / divisionOfSpectrum - 1);
		for (int i = 0; i < st.smoothSpectrumAvg.Length / divisionOfSpectrum - 1; i++)
		{
			for (int j = 0; j < st.smoothSpectrumAvg.Length / divisionOfSpectrum - 1; j++)
			{
				blocks[i, j] = Instantiate(refObj, new Vector3(transform.position.x + i, transform.position.y, transform.position.z + j), transform.rotation, this.transform); //Mathf.Log(i)
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 placeholderBlockPos = new Vector3();

		for (int i = 0; i < st.smoothSpectrumAvg.Length / divisionOfSpectrum - 1; i++)
		{
			for (int j = 0; j < st.smoothSpectrumAvg.Length / divisionOfSpectrum - 1; j++)
			{
				placeholderBlockPos = blocks[i, j].transform.position;

				if (j == 0)
				{
					placeholderBlockPos.y = st.smoothSpectrumAvg[i] * sensitivity;
				}
				else if(i == 0)
				{
					placeholderBlockPos.y = st.smoothSpectrumAvg[j] * sensitivity;
				}
				else
				{
					placeholderBlockPos.y = (blocks[i,0].transform.position.y + blocks[0, j].transform.position.y) / 2f;
				}

				blocks[i,j].GetComponent<Rigidbody>().MovePosition(placeholderBlockPos);
			}
		}
	}
}
