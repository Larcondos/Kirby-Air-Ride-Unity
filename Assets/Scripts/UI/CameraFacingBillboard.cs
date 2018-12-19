using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
	// Access to the Camera.
	public Camera m_Camera;

	// Initializes variables.
	void Start() {
		m_Camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}

	// Keeps the Canvas aimed at the Camera.
	void Update()
	{		
		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
			m_Camera.transform.rotation * Vector3.up);
	}
}