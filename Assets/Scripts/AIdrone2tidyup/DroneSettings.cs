using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSettings : MonoBehaviour
{
    [SerializeField]
    private float droneSpeed = 2f;

    public static float DroneSpeed => Instance.droneSpeed;

    [SerializeField]
    private float aggroRadius = 4f;

    public static float AggroRadius => Instance.aggroRadius;

    [SerializeField]
    private float attackRange = 3f;

    public static float AttackRange => Instance.attackRange;

    [SerializeField]
    private GameObject droneProjectilePrefab;
    public static GameObject DroneProjectilePrefab => Instance.droneProjectilePrefab;

    public static DroneSettings Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
}
