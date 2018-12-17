using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : Singleton<GameInitializer> {
	void Awake() {
		Generate();
	}

	void Generate() {
		WorldMap world = WorldMap.Instance;
		MeepleManager meeples = MeepleManager.Instance;

		world.Clear();

		world.AddNode("n1", 0, 0);
		world.AddNode("n2", -4, 0);
		world.AddNode("n3", -2, -2);
		Node home = world.AddNode("n4", 4, -2);
		world.AddNode("n5", 2, 2);
		world.AddNode("n6", -3, 2);
		world.AddNode("n7", -4, -5);
		world.AddNode("n8", -2, -6);
		world.AddNode("n9", -6, -3);
		world.AddNode("n10", 0, -5);
		world.AddNode("n11", -6, -3);

		world.AddEdge("e1", "n1", "n2");
		world.AddEdge("e2", "n2", "n3");
		world.AddEdge("e3", "n10", "n4");
		world.AddEdge("e4", "n4", "n1");
		world.AddEdge("e5", "n1", "n5");
		world.AddEdge("e6", "n4", "n5");
		world.AddEdge("e7", "n2", "n6");
		world.AddEdge("e8", "n7", "n3");
		world.AddEdge("e9", "n7", "n8");
		world.AddEdge("e10", "n10", "n8");
		world.AddEdge("e11", "n9", "n2");
		world.AddEdge("e12", "n7", "n11");

		meeples.AddMeeple(home.GetComponent<Location>());
	}
}
