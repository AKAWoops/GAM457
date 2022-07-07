using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEditor.UIElements;
using UnityEngine;

public class FindPotentialFriend : AntAIState
{
    public float speed = 100;

    public GameObject mainGameObject;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        // record the location of our MAIN gameobject with all the AIAgent stuff and rigidbodies etc for later use
        mainGameObject = aGameObject;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        // Small HACK: State prefabs are attached as children, so go get the parent rigidbody
        // GetComponentInParent<Rigidbody>().AddForce(new Vector3(Random.Range(-speed, speed), 0, Random.Range(-speed, speed)));
        
        // Slightly better version that can handle any GameObject child setup
        mainGameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-speed, speed), 0, Random.Range(-speed, speed)));
    }
}
