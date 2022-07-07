using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;


public enum  Week6AIScenario
{
    isCloseToPotentialFriend = 0,
    makeFriend = 1,
    canSeePotentialFriend = 2
}

public class Sensor : MonoBehaviour, ISense
{
    // HACK. Directly dragged, you SHOULD use Physics.SphereOverlap/OnTriggerEnter etc
    public GameObject potentialFriend;
    public bool makeFriend = false;
    

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set( Week6AIScenario.canSeePotentialFriend, CheckCanSee());
        aWorldState.Set( Week6AIScenario.makeFriend, makeFriend);
        aWorldState.Set( Week6AIScenario.isCloseToPotentialFriend, CheckIsCloseToPotentialFriend());
    }

    public bool CheckCanSee()
    {
        // I CAN see the guy
        RaycastHit info = new RaycastHit();
        if (Physics.Linecast(transform.position, potentialFriend.transform.position, out info))
        {
            if (info.transform == potentialFriend.transform)
            {
                // I saw the guy!!!
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    public bool CheckIsCloseToPotentialFriend()
    {
        if (Vector3.Distance(transform.position, potentialFriend.transform.position) < 5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}