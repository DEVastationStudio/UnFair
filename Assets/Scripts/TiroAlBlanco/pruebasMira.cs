using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pruebasMira : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 viewPortPos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        if (!(viewPortPos.x > 1 || viewPortPos.x < 0 || viewPortPos.y > 1 || viewPortPos.y < 0)) 
        {
            Ray r = Camera.main.ViewportPointToRay(viewPortPos);
            Vector3 rPos = r.GetPoint(10);
            transform.position = new Vector3(rPos.x, rPos.y, transform.position.z);
        }
    }
}