using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerView))]
public class EditorHandler : Editor
{
    private void OnSceneGUI()
    {
        PlayerView pv = (PlayerView)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(pv.transform.position, Vector3.up, Vector3.forward, 360, pv.viewRadius);
        Vector3 viewAngleA = pv.DirFromAngle(-pv.viewAngle / 2, false);
        Vector3 viewAngleB = pv.DirFromAngle(pv.viewAngle / 2, false);

        Handles.DrawLine(pv.transform.position, pv.transform.position + viewAngleA * pv.viewRadius);
        Handles.DrawLine(pv.transform.position, pv.transform.position + viewAngleB * pv.viewRadius);
    }
}
