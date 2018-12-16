using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController> {
	private CameraFollow cameraFollow;
	private Camera cam;

	void Start () {
		cam = GetComponent<Camera>();
		cameraFollow = GetComponent<CameraFollow>();
	}

	public void FocusTarget(Vector3 target) {
		cameraFollow.FocusTarget(target);
	}

	public void Focus(Vector3 target, float distance, float angle, float speed) {
		cameraFollow.Focus(target, distance, angle, speed);
	}
}
