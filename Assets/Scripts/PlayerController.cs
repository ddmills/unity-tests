using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController> {
	[SerializeField]
	private Node selectedNode;
	[SerializeField]
	private Meeple selectedMeeple;
	[SerializeField]
	private ViewMode viewMode;

	void Start() {
		viewMode = ViewMode.WorldMap;
		SelectNode("n1");
		ViewMap();
	}

	public void SelectNode(string nodeId) {
		Node ns = WorldMap.Instance.GetNode(nodeId);
		if (ns) {
			OnClickNode(ns);
		}
	}

	public void DeselectNode() {
		selectedNode.Deselect();
	}

	public void SelectMeeple(Meeple meeple) {
		selectedMeeple = meeple;
		viewMode = ViewMode.Meeple;
		CameraController.Instance.Focus(meeple.transform.position, 1f, 40f, .05f);
		NodeInspectorUI.Instance.ShowMeeple(meeple);
	}

	public void DeselectMeeple() {
		if (selectedMeeple) {
			selectedMeeple.OnHoverLeave();
			selectedMeeple = null;
		};
		ViewNodeDetails(selectedNode);
	}

	public void OnClickMeeple(Meeple meeple) {
		SelectMeeple(meeple);
	}

	public void OnClickNode(Node node) {
		if (selectedNode) {
			if (selectedNode == node) {
				OnPrimaryAction();
				return;
			} else {
				DeselectNode();
			}
		}

		selectedNode = node;
		selectedNode.Select();
		CameraController.Instance.FocusTarget(selectedNode.Position);
	}

	public void OnPrimaryAction() {
		if (viewMode == ViewMode.WorldMap) {
			ViewNodeDetails(this.selectedNode);
		}
	}

	public void OnSecondaryAction() {
		if (viewMode == ViewMode.Location) {
			ViewMap();
		}

		if (viewMode == ViewMode.Meeple) {
			DeselectMeeple();
		}
	}

	public void OnAxisInput(float inputAngle) {
		if (viewMode == ViewMode.WorldMap) {
			NavigateMap(inputAngle);
		}
	}

	public void ViewNodeDetails(Node node) {
		this.selectedNode = node;
		this.viewMode = ViewMode.Location;
		CameraController.Instance.Focus(node.Position, 3f, 40f, .05f);
		Location location = node.gameObject.GetComponent<Location>();
		NodeInspectorUI.Instance.ShowLocation(location);
	}

	public void ViewMap() {
		this.viewMode = ViewMode.WorldMap;
		NodeInspectorUI.Instance.Hide();
		CameraController.Instance.Focus(selectedNode.Position, 16f, 60f, .05f);
	}

	private float NormalizeAngle(float angle) {
		return angle < 0 ? 360 + angle : angle;
	}

	public void NavigateMap(float inputAngle) {
		List<Node> neighbors = WorldMap.Instance.GetNeighbors(selectedNode);
		Node closest = null;
		float closestAngle = Mathf.Infinity;
		Vector3 startPos = selectedNode.Position;

		neighbors.ForEach((n) => {
			Vector3 relativePos = (n.Position - startPos).normalized;
			float rad = Mathf.Atan2(relativePos.z, relativePos.x);
			float deg = rad * Mathf.Rad2Deg;
			float normalizedNodeAngle = NormalizeAngle(deg);
			float normalizeInputAngle = NormalizeAngle(inputAngle);
			float angleDiff = Mathf.Abs(normalizeInputAngle - normalizedNodeAngle);

			if (angleDiff <= closestAngle) {
				closest = n;
				closestAngle = angleDiff;
			}
		});

		if (closest != null) {
			SelectNode(closest.Id);
		}
	}
}
