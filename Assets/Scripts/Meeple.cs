using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meeple : MonoBehaviour {
	[SerializeField]
	private string label;
	public string Label {
		get {
			return label;
		}
	}
	[SerializeField]
	private Transform meepleSelectorHolder;
	[SerializeField]
	private GameObject selectorIndicatorPrefab;
	private GameObject selector;

	public Meeple Init(string label) {
		this.label = label;
		return this;
	}

	public void Select() {
		selector = Instantiate(
			selectorIndicatorPrefab,
			meepleSelectorHolder.position,
			Quaternion.identity,
			meepleSelectorHolder
		);
	}

	public void Deselect() {
		if (selector) {
			Destroy(selector);
		}
	}
}
