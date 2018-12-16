using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
	[SerializeField]
	public string Id { get; private set; }
	[SerializeField]
	public Vector3 Position {
		get {
			return new Vector3(transform.position.x, 0, transform.position.z);
		}
	}
	[SerializeField]
	private bool isSelected = false;
	[SerializeField]
	private float lerpSpeed = .06f;

	public Node Init(string id) {
		Id = id;

		return this;
	}

	void Start () {
		if (isSelected) {
			Select();
		}
	}

	void Update() {
		if (isSelected) {
			Vector3 targetPosition = new Vector3(transform.position.x, .1f, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed);
		} else {
			Vector3 targetPosition = new Vector3(transform.position.x, 0, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed);
		}
	}

	public void Select() {
		isSelected = true;
	}

	public void Deselect() {
		isSelected = false;
	}
}
