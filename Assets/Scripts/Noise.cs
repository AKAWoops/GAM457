using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
        public delegate void OnNoise(Vector3 position, float distance);

        public static event OnNoise SoundEvent;
        //invoke the event with this void is to make sure that the event isn't invoked if it has no listeners
        public static void MakeNoise(Vector3 Position, float Radius)
        {
            if (SoundEvent != null)
            {
                Debug.Log($"Noise made at {Position} with a radius of {Radius}");
                SoundEvent.Invoke(Position, Radius);
            }
            else Debug.Log($"SoundEvent wasn't invoked, as there are no listeners");
        }
    }
