using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EnemyBaseStateV2
{
    protected EnemyBaseStateV2(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }

    protected GameObject gameObject;
    protected Transform transform;

    public abstract Type Tick();
}

