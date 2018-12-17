using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeepleManager : Singleton<MeepleManager> {
	[SerializeField]
	private GameObject meeplePrefab;
	[SerializeField]
	private List<Meeple> meeples;

	public Meeple AddMeeple(Location location) {
		GameObject meepleObj = Object.Instantiate(
			meeplePrefab,
			Vector3.zero,
			Quaternion.identity,
			transform
		);

		Meeple meeple =  meepleObj.GetComponent<Meeple>();

		location.AddMeeple(meeple);

		return meeple;
	}
}
