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

	public Location Init(string label) {
		this.label = label;
		return this;
	}

	public void AddMeeple(Meeple meeple) {
		meeples.Add(meeple);
		meeple.transform.position = transform.position;
	}
}
