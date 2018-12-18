using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MeepleSelectionButton : MonoBehaviour, ISelectHandler, IDeselectHandler {
	private Meeple meeple;

	public MeepleSelectionButton Init(Meeple meeple) {
		this.meeple = meeple;
		GetComponentInChildren<Text>().text = meeple.Label;

		return this;
	}

	public void OnSelect(BaseEventData eventData) {
		meeple.Select();
	}

	public void OnDeselect(BaseEventData eventData) {
		meeple.Deselect();
	}
}
