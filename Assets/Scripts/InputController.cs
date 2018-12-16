using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
	private float axisSensitivity = .1f;
	private bool axisMoved = false;
	[SerializeField]
	private PlayerController playerController;

	private float getAngle(float horizontal, float vertical) {
		float rad = Mathf.Atan2(vertical, horizontal);
		float deg = rad * Mathf.Rad2Deg;

		return deg < 0 ? 360 + deg : deg;
	}

	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			int layerMask = 1 << (int) LayerMasks.MapFeatures;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
				Node node = hit.transform.parent.gameObject.GetComponent<Node>();

				playerController.OnClickNode(node);
			}
		}

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)) {
			playerController.OnPrimaryAction();
		}

		if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton1)) {
			playerController.OnSecondaryAction();
		}

		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		if (!axisMoved && (
			horizontal > axisSensitivity
			|| horizontal < -axisSensitivity
			|| vertical > axisSensitivity
			|| vertical < -axisSensitivity
		)) {
			float angle = getAngle(horizontal, vertical);
			playerController.OnAxisInput(angle);
			axisMoved = true;
		} else if (horizontal == 0 && vertical == 0) {
			axisMoved = false;
		}
	}
}
