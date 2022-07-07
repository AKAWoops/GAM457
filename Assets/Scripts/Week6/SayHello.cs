using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;
using Anthill.Utils;

public class SayHello : AntAIState
{
    public GameObject mainGO;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        mainGO = aGameObject;
    }

    public override void Enter()
    {
        base.Enter();
        
        StartCoroutine(DoHelloSequence());
    }

    private IEnumerator DoHelloSequence()
    {
        Debug.Log("I'm very nice don't ya know");
        yield return new WaitForSeconds(3f);
        Debug.Log("HEEELLLLLOO!");
        yield return new WaitForSeconds(2f);
        Debug.Log("Well.. you're my friend now, there's no turning back");
        mainGO.GetComponent<Sensor>().makeFriend = true;
        Finish();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
