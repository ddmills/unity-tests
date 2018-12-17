using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour {
	[SerializeField]
	private string label;
	[SerializeField]
	private List<Meeple> meeples = new List<Meeple>();
	[SerializeField]
	private List<Transform> slots = new List<Transform>();
	public List<Meeple> Meeples {
		get {
			return meeples;
		}
	}

	public Location Init(string label) {
		this.label = label;
		return this;
	}

	private Transform GetSlot() {
		return slots.Find((s) => s.childCount == 0);
	}

	public void AddMeeple(Meeple meeple) {
		meeples.Add(meeple);
		Transform slot = GetSlot();
		meeple.transform.parent = slot;
		meeple.transform.localPosition = Vector3.zero;
	}
}
