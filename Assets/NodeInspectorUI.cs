using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodeInspectorUI : Singleton<NodeInspectorUI> {
	[SerializeField]
	private GameObject meepleButtonPrefab;
	[SerializeField]
	private RectTransform meepleButtonContainer;
	[SerializeField]
	private GameObject defaultSelectedButton;
	[SerializeField]
	private Text nodeDisplayText;

	public void Hide() {
		GetComponent<Canvas>().enabled = false;
	}

	public void Show(Location location) {
		GetComponent<EventSystem>().SetSelectedGameObject(defaultSelectedButton);
		nodeDisplayText.text = location.Label;

		int childCount = meepleButtonContainer.childCount;
		for (int i = childCount - 1; i >= 0; i--) {
			DestroyImmediate(meepleButtonContainer.GetChild(i).gameObject);
		}

		GetComponent<Canvas>().enabled = true;

		location.Meeples.ForEach((m) => {
			GameObject button = Object.Instantiate(
				meepleButtonPrefab,
				Vector2.zero,
				Quaternion.identity,
				meepleButtonContainer.transform
			);
			button.GetComponent<MeepleSelectionButton>().Init(m);
		});
	}
}
