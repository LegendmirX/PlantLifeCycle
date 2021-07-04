using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(SalvageSwarm))]
public class DiscSamplingEditor : Editor 
{
    //void OnSceneGUI()
    //{
    //    SalvageSwarm salvageSwarm = (SalvageSwarm)target;
    //}

    //public override void OnInspectorGUI()
    //{
    //    SalvageSwarm salvageSwarm = (SalvageSwarm)target;

    //    if (GUILayout.Button("Generate"))
    //    {
    //        Transform parent = new GameObject("SalvageSwarmPreview").transform;
    //        parent.SetParent(salvageSwarm.transform);
    //        parent.position = salvageSwarm.transform.position;

    //        Vector3 spawnArea = salvageSwarm.GetSpawnArea();
    //        Vector3 spawnStartPos = new Vector3(salvageSwarm.transform.position.x - spawnArea.x / 2, salvageSwarm.transform.position.y - spawnArea.y / 2, salvageSwarm.transform.position.z - spawnArea.z / 2);
    //        List<Vector3> spawns = salvageSwarm.GenerateSalvageSwarmPoints();

    //        foreach(Vector3 spawn in spawns)
    //        {
    //            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

    //            go.transform.position = spawnStartPos + spawn;
    //            go.transform.SetParent(parent);
    //        }
    //    }
    //}
}
