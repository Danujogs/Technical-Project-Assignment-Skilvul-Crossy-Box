using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    // static akan membuat variabel ini shared saling terbagi pada semua tree yang ada
    public static List<Vector3> AllPositions = new List<Vector3>();

    private void OnEnable()
    {
        AllPositions.Add(this.transform.position);
        Debug.Log(AllPositions.Count);
    }

    private void OnDisable()
    {
        AllPositions.Remove(this.transform.position);
    }

}
