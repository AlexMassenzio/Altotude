using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private GameObject player;
	private Transform focus; //Which Transform the camera should track.

	private const float ZOOM_SPEED = 2;
	private float zoomMargin = 25;

	Coroutine lastCoroutine;

	void Start ()
	{
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		focus = player.transform;
		lastCoroutine = null;
	}
	
	void Update ()
	{
		transform.position += (focus.position - transform.position) * Time.deltaTime * 5;
		Vector3 temp = transform.position;
		temp.z = -10f;
		transform.position = temp;
	}
	
	/// <summary>
	/// Sets which Transform component the camera should follow.
	/// </summary>
	/// <param name="t">The desired Transform.</param>
	public void SetFocus(Transform t)
	{
		focus = t;
	}

	public void ZoomToWidth (float width)
	{
		
			if (lastCoroutine != null)
			{
				StopCoroutine (lastCoroutine);
			}
			lastCoroutine = StartCoroutine(ZoomToWidthHelper(width));
	}

	IEnumerator ZoomToWidthHelper(float width)
	{
		float progress = 0;
		while (progress < 1)
		{
			progress += Time.deltaTime * ZOOM_SPEED;
			yield return null;
		}
	}

	public void ResetZoom()
	{
		if (lastCoroutine != null)
		{
			StopCoroutine (lastCoroutine);
		}
		lastCoroutine = StartCoroutine(ResetZoomHelper());
	}

	IEnumerator ResetZoomHelper()
	{
		float startingSize = Camera.main.orthographicSize;
		float progress = 0;
		while (progress < 1)
		{
			progress += Time.deltaTime * ZOOM_SPEED;
			yield return null;
		}
	}
}
