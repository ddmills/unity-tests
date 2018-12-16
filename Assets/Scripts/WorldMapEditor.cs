using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldMap))]
public class WorldMapEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

        WorldMap wm = (WorldMap) target;

        if (GUILayout.Button("Generate")) {
            wm.Generate();
        }

        if (GUILayout.Button("Clear")) {
            wm.Clear();
        }
	}
}
