using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] private float speed = 2;
    private int currentTarget;
    private Rigidbody rig;
    private DynamicDifficultyManager DDM;


    void FixedUpdate()
    {
        if (!HUD_Marbles.startedPressed) { return; }
        if (targets == null || targets.Length < 2) { return; }
        if (transform.position != targets[currentTarget].position)
        {
            Vector3 objectPos = Vector3.MoveTowards(transform.position, targets[currentTarget].position, (speed * DDM.GetValue(0)) * Time.deltaTime);
            rig.MovePosition(objectPos);
        }
        else
        {
            currentTarget = (currentTarget + 1) % targets.Length; //now the obstacle with go till the end of the targets and go to the first again. In order to create a path that comes from the end to the beginning is necessary to create a condition to know when to do that
        }

    }

    public void SetTargets(Transform[] _targets)
    {
        DDM = FindObjectOfType<DynamicDifficultyManager>();
        rig = GetComponent<Rigidbody>();
        targets = _targets;
    }
}
