using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	[SerializeField]
	private Vector3 target;
	[SerializeField]
	private float followDistance = 4f;
	[SerializeField]
	private float followAngle = 20f;
	[SerializeField]
	private float speed = .05f;

	void Update () {
		Vector3 newRotation = Quaternion.Euler(followAngle, 0, 0) * Vector3.back;
		Vector3 newPosition = target + (newRotation * followDistance);

		transform.position = Vector3.Lerp(transform.position, newPosition, speed);

		Vector3 difference = target - transform.position;
		Quaternion newRot = Quaternion.LookRotation(difference);
		transform.rotation = Quaternion.Lerp(transform.rotation, newRot, speed);
	}

	public void FocusTarget(Vector3 target) {
		this.target = target;
	}

	public void Focus(Vector3 target, float distance = 4f, float angle = 20f, float speed = .05f) {
		this.target = target;
		this.followDistance = distance;
		this.followAngle = angle;
		this.speed = speed;
	}
}
