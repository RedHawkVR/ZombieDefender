using UnityEngine;

public class Cam : MonoBehaviour {
	// allows for panning
	void Update () {
		float xAxis = Input.GetAxis("Horizontal");
		float zAxis = Input.GetAxis("Vertical");

		gameObject.transform.Translate(xAxis, 0.0f, zAxis);
	}
}
