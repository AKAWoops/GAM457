using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGUY : MonoBehaviour
{
    [SerializeField]
    Color tintColor = Color.green;
    [SerializeField]
    Color tinColorForRaycastAll = Color.yellow;
    [SerializeField]
    bool multiple;

    // Update is called once per frame
    void Update()
    {
        if (multiple)
            RaycastMultiple();
        else
            RaycastSingle();
    }

    private void RaycastSingle()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        float maxDistance = 10f;

        //                            [magnitude] total distance 10 meters for eg  
        Debug.DrawRay(origin, direction * maxDistance, Color.red);
        Ray ray = new Ray(origin, direction);

        //RaycastHit hitinfo;
        bool result = Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance);

        if (result)
        {
            raycastHit.collider.GetComponent<Renderer>().material.color = tintColor;
        }
    }
    private void RaycastMultiple()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Debug.DrawRay(origin, direction * 10f, Color.yellow);
        Ray ray = new Ray(origin, direction);
        // this returns everything raycasts hits took me ages to understand what the unity site was on about when i used it remember this
        var multipleHits = Physics.RaycastAll(ray);
        foreach (var raycastHit in multipleHits)
        {
            raycastHit.collider.GetComponent<Renderer>().material.color = Color.blue;
        }

    }
}

