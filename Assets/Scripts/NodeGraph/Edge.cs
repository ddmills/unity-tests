using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Edge : MonoBehaviour {
	[SerializeField]
	public string Id { get; private set; }
	[SerializeField]
	public string StartingNodeId { get; private set; }
	[SerializeField]
	public string EndingNodeId { get; private set; }
	public Node StartingNode {
		get {
			return WorldMap.Instance.GetNode(StartingNodeId);
		}
	}
	public Node EndingNode {
		get {
			return WorldMap.Instance.GetNode(EndingNodeId);
		}
	}
	public Edge Init(string id, string startingNodeId, string endingNodeId) {
		Id = id;
		StartingNodeId = startingNodeId;
		EndingNodeId = endingNodeId;

		return this;
	}

	void Start () {
		if (StartingNode == null || EndingNode == null) {
			return;
		}
		Vector3 direction = (StartingNode.Position - EndingNode.Position).normalized;
		float distance = Vector3.Distance(StartingNode.Position, EndingNode.Position);
		Vector3 middle = (StartingNode.Position + EndingNode.Position) / 2;

		transform.position = middle;
		transform.LookAt(EndingNode.Position);
		transform.localScale = new Vector3(
			transform.localScale.x,
			transform.localScale.y,
			distance
		);
	}

	public bool IsConnectedToNode(string nodeId) {
		return StartingNodeId == nodeId || EndingNodeId == nodeId;
	}
}
