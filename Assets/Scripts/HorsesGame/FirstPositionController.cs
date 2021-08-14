using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPositionController : MonoBehaviour
{
    [SerializeField] private CrownManager[] horses;
    private float maxZ;
    private CrownManager firstHorse;

    void FixedUpdate()
    {

        foreach (CrownManager horse in horses)
        {
            if (maxZ < horse.transform.position.z)
            {
                maxZ = horse.transform.position.z;
                horse.ActivateCrown();
                if (firstHorse != null && firstHorse != horse)
                {
                    firstHorse.DeactivateCrown();
                }
                firstHorse = horse;
            }
        }
    }

    public void Init()
    {
        maxZ = horses[0].transform.position.z;//-1;
        firstHorse = null;
        foreach (CrownManager horse in horses)
        {
            horse.DeactivateCrown();
        }
    }
}
