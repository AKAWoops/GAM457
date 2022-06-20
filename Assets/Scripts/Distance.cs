using UnityEngine;
using System.Collections;

public class Distance : MonoBehaviour
{
    public Transform t1;
    public Transform t2;
    void Start()
    {

    }

    private void Update()
    {
        Debug.Log(Vector3.Distance(t1.position,t2.position));
    }


}