using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : Singleton<WorldMap> {
	[SerializeField]
	private GameObject nodePrefab;
	[SerializeField]
	private GameObject edgePrefab;
	[SerializeField]
	private List<Node> nodes;
	[SerializeField]
  private List<Edge> edges;

	public void Clear() {
		int childCount = transform.childCount;
		for (int i = childCount - 1; i >= 0; i--) {
			DestroyImmediate(transform.GetChild(i).gameObject);
		}
		nodes = new List<Node>();
		edges = new List<Edge>();
	}

	public Node GetNode(string nodeId) {
		return nodes.Find((n) => n.Id == nodeId);
	}

	public Edge GetEdge(string edgeId) {
		return edges.Find((e) => e.Id == edgeId);
	}

	public List<Edge> GetEdgesForNode(string nodeId) {
		return edges.FindAll(e => e.IsConnectedToNode(nodeId));
	}

	public List<Edge> GetEdgesForNode(Node node) {
		return GetEdgesForNode(node.Id);
	}

	public List<Node> GetNeighbors(string nodeId) {
		List<Edge> connectedEdges = GetEdgesForNode(nodeId);
		List<Node> neighbors = new List<Node>();

		connectedEdges.ForEach((e) => {
			string opposideNodeId = e.StartingNodeId == nodeId ? e.EndingNodeId : e.StartingNodeId;

			neighbors.Add(GetNode(opposideNodeId));
		});

		return neighbors;
	}

	public List<Node> GetNeighbors(Node node) {
		return GetNeighbors(node.Id);
	}

	public Node AddNode(string nodeId, float x, float y) {
		GameObject nodeObj = Object.Instantiate(
			nodePrefab,
			new Vector3(x, 0, y),
			Quaternion.identity,
			transform
		);

		Node node = nodeObj.GetComponent<Node>().Init(nodeId);
		nodeObj.GetComponent<Location>().Init("Node " + nodeId);

		nodes.Add(node);

		return node;
	}

	public Edge AddEdge(string edgeId, string startingNodeId, string endingNodeId) {
		GameObject edgeObj = Object.Instantiate(
			edgePrefab,
			Vector3.zero,
			Quaternion.identity,
			transform
		);

		Edge edge = edgeObj.GetComponent<Edge>().Init(edgeId, startingNodeId, endingNodeId);
		edges.Add(edge);

		return edge;
	}
}
