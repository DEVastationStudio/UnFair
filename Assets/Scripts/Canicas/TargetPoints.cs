using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoints : MonoBehaviour
{
    public enum PosSpawn
    {
        North,
        South,
        East,
        West
    };
    [SerializeField] private PosSpawn posSpawn;
    [SerializeField] private Transform[] targets;

    public Transform[] GetTargets()
    {
        return targets;
    }

    public PosSpawn GetPosSpawn()
    {
        return posSpawn;
    }
}
