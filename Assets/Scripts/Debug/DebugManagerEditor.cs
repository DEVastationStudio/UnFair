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
        dm.Stars1 = EditorGUILayout.IntField("Stars1 (Tiro al blanco)", dm.Stars1);
        if (GUILayout.Button("Set Stars1"))
        {
            dm.SetStars(1, dm.Stars1);
        }
        dm.Stars2 = EditorGUILayout.IntField("Stars2 (Caballos)", dm.Stars2);
        if (GUILayout.Button("Set Stars2"))
        {
            dm.SetStars(2, dm.Stars2);
        }
        dm.Stars3 = EditorGUILayout.IntField("Stars3 (Patos)", dm.Stars3);
        if (GUILayout.Button("Set Stars3"))
        {
            dm.SetStars(3, dm.Stars3);
        }
        dm.Stars4 = EditorGUILayout.IntField("Stars4 (Canicas)", dm.Stars4);
        if (GUILayout.Button("Set Stars4"))
        {
            dm.SetStars(4, dm.Stars4);
        }
        if (GUILayout.Button("Reset Zenobia Tokens"))
        {
            PlayerPrefs.SetFloat("EarnedTokens", 0);
            PlayerPrefs.SetFloat("UsedTokens", 0);
        }
    }
}
