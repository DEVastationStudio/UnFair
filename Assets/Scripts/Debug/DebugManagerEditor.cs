using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugManager))]
public class DebugManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DebugManager dm = (DebugManager)target;
        
        dm.npcSpriteManager = (NpcSpriteManager)EditorGUILayout.ObjectField("NPC Sprite Manager", dm.npcSpriteManager, typeof(NpcSpriteManager), true);
        
        dm.Progression = EditorGUILayout.IntField("Progression", dm.Progression);
        if (GUILayout.Button("Set Progression"))
        {
            dm.SetProgression();
        }

        dm.ProgAlt1 = EditorGUILayout.IntField("ProgAlt1 (Terror)", dm.ProgAlt1);
        if (GUILayout.Button("Set ProgAlt1"))
        {
            dm.SetProgAlt1();
        }

        dm.ProgAlt2 = EditorGUILayout.IntField("ProgAlt2 (Espejos)", dm.ProgAlt2);
        if (GUILayout.Button("Set ProgAlt2"))
        {
            dm.SetProgAlt2();
        }
    }
}
