﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllerInput : MonoBehaviour {
	public SteamVR_TrackedObject trackedObject;
	public SteamVR_Controller.Device device;

	// Teleporter
	public LineRenderer laser;
	public GameObject teleportAimerObject;
	public Vector3 teleportLocation;
	public GameObject player;
	public LayerMask laserMask;
	public static float yNudgeAmount = 1f; // specific to teleportAimerObject height
	private static readonly Vector3 yNudgeVector = new Vector3(0f, yNudgeAmount, 0f);

	// Use this for initialization
	void Start () {
		trackedObject = GetComponent<SteamVR_TrackedObject>();
		//laser = GetComponentInChildren<LineRenderer>();
	}

	void setLaserStart (Vector3 startPos) {
		laser.SetPosition(0, startPos);
	}

	void setLaserEnd (Vector3 endPos) {
		laser.SetPosition(1, endPos);
	}

	// Update is called once per frame
	void Update () {
		device = SteamVR_Controller.Input((int) trackedObject.index);
		// trigger pressed down
		if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger)) {
			laser.gameObject.SetActive(true);
			teleportAimerObject.SetActive(true);

			//laser.SetPosition(0, gameObject.transform.position);
			setLaserStart(gameObject.transform.position);
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, 15, laserMask)) {
				teleportLocation = hit.point;
				laser.SetPosition(1, teleportLocation);
				teleportAimerObject.transform.position = new Vector3(teleportLocation.x, 
					teleportLocation.y + yNudgeAmount, 
					teleportLocation.z);
			} else {
				//teleportLocation = new Vector3(transform.forward.x * 15 + transform.position.x,
				//	transform.forward.y * 15 + transform.position.y,
				//	transform.forward.z * 15 + transform.position.z);

				teleportLocation = transform.position + 15 * transform.forward;
				RaycastHit groundRay;
				if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 17, laserMask)) {
					teleportLocation.y = groundRay.point.y; 
					//teleportLocation = new Vector3(transform.forward.x * 15 + transform.position.x,
					//	groundRay.point.y, transform.forward.z * 15 + transform.position.z);
				}
			}
			//laser.SetPosition(1, transform.forward * 15 + transform.position);
			//teleportAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmount, 0);
			setLaserEnd(teleportLocation);
			teleportAimerObject.transform.position = teleportLocation + yNudgeVector;
		}
		// trigger released
		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
			laser.gameObject.SetActive(false);
			teleportAimerObject.SetActive(false);       
			player.transform.position = teleportLocation;   
		}
	}
}
