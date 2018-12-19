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

	void Start() {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	public void OnSelect(BaseEventData eventData) {
		meeple.OnHover();
	}

	public void OnDeselect(BaseEventData eventData) {
		meeple.OnHoverLeave();
	}

	public void OnClick() {
		PlayerController.Instance.SelectMeeple(meeple);
	}
}
