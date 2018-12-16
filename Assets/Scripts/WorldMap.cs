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

	public WorldMap Generate() {
		Debug.Log("Generating graph");

		Clear();

		AddNode("n1", 0, 0);
		AddNode("n2", -4, 0);
		AddNode("n3", -2, -2);
		AddNode("n4", 4, -2);
		AddNode("n5", 2, 2);
		AddNode("n6", -3, 2);
		AddNode("n7", -4, -5);
		AddNode("n8", -2, -6);
		AddNode("n9", -6, -3);
		AddNode("n10", 0, -5);
		AddNode("n11", -6, -3);

		AddEdge("e1", "n1", "n2");
		AddEdge("e2", "n2", "n3");
		AddEdge("e3", "n10", "n4");
		AddEdge("e4", "n4", "n1");
		AddEdge("e5", "n1", "n5");
		AddEdge("e6", "n4", "n5");
		AddEdge("e7", "n2", "n6");
		AddEdge("e8", "n7", "n3");
		AddEdge("e9", "n7", "n8");
		AddEdge("e10", "n10", "n8");
		AddEdge("e11", "n9", "n2");
		AddEdge("e12", "n7", "n11");

		return this;
	}

	void Awake() {
		Generate();
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
