using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PointScriptableObject")]
public class PointSO : ScriptableObject
{
    public Vector3[] seedPoints;

    public Vector3[] moldPoints;

    public Vector3[] fishWaypoints;

    public Vector3[] stickPoints;

    
}
