using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherAI : MonoBehaviour
 {
    void Start()
    {
        //Subscribe to the event
        Noise.SoundEvent += OnHearNoise;
    }
    void OnDestroy()
    {
        //Unsubscribe from the event
        Noise.SoundEvent -= OnHearNoise;
    }

    void OnHearNoise(Vector3 Position, float radius)
    {
        //Do stuff...  
    }

}