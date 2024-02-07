using System.Collections;
using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(DoorTriggerInteraction))]
public class LabelHandle : Editor
{
    private static GUIStyle labelStyle;

    private void OnEnable()
    {
        labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.MiddleCenter;

    }

    private void OnSceneGUI()
    {
        DoorTriggerInteraction door = (DoorTriggerInteraction)target;

        Handles.BeginGUI();
        Handles.Label(door.transform.position + new Vector3(100f, 114f, 0f), door.CurrentDoorPosition.ToString(), labelStyle); //edit this float to change pos
        Handles.EndGUI();
    }



}
