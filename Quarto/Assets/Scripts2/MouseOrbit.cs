using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseOrbit : MonoBehaviour {

	public Transform target ;
	public float distance = 10.0f;
	public float distMin = 10.0f;
	public float distMax = 20.0f;

	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	private float x = 0.0f;
	private float y = 0.0f;



	void Start () {	

		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

	void Update () {
		if (target) {
			if(Input.GetMouseButton(2)){

				distance += Input.GetAxis("Mouse Y") * xSpeed * 0.003f;
				distance = Mathf.Clamp (distance, distMin, distMax);
			}
			if(Input.GetMouseButton(1)){

				x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
				y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

				y = ClampAngle(y, yMinLimit, yMaxLimit);
			}
				
			transform.rotation = Quaternion.Euler(y, x, 0);
			transform.position = Quaternion.Euler(y, x, 0) * new Vector3(0.0f, 0.0f, -distance) + target.position;
		}
	}

	float ClampAngle (float angle, float min, float max) {
		
		angle += Mathf.Floor (angle / 360) * 360;
		return Mathf.Clamp (angle, min, max);

	}

}