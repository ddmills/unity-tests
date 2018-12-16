using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	private Node selectedNode;
	[SerializeField]
	private ViewMode viewMode;

	void Start() {
		viewMode = ViewMode.WorldMap;
		SelectNode("n1");
		ViewMap();
	}

	void SelectNode(string nodeId) {
		Node ns = WorldMap.Instance.GetNode(nodeId);
		if (ns) {
			OnClickNode(ns);
		}
	}

	public void OnClickNode(Node node) {
		if (selectedNode) {
			if (selectedNode == node) {
				OnPrimaryAction();
				return;
			} else {
				selectedNode.Deselect();
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
		if (viewMode == ViewMode.Detail) {
			ViewMap();
		}
	}

	public void OnAxisInput(float inputAngle) {
		if (viewMode == ViewMode.WorldMap) {
			NavigateMap(inputAngle);
		}
	}

	public void ViewNodeDetails(Node node) {
		this.selectedNode = node;
		this.viewMode = ViewMode.Detail;
		CameraController.Instance.Focus(node.Position, 3f, 40f, .2f);
	}

	public void ViewMap() {
		this.viewMode = ViewMode.WorldMap;
		CameraController.Instance.Focus(selectedNode.Position, 16f, 60f, .1f);
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
			float normalizedNodeAngle = deg < 0 ? 360 + deg : deg;
			float angleDiff = Mathf.Abs(inputAngle - normalizedNodeAngle);

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
