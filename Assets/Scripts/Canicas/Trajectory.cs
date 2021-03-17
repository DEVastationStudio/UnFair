using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int maxIterations;
    Scene normalScene;
    Scene predictionScene;

    PhysicsScene normalPhyScene;
    PhysicsScene predictionPhyScene;

    LineRenderer pathLine;

    GameObject ballCopy;


    void Start()
    {
        Physics.autoSimulation = false;

        normalScene = SceneManager.GetActiveScene();
        normalPhyScene = normalScene.GetPhysicsScene();

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("Trajectory", parameters);
        predictionPhyScene = predictionScene.GetPhysicsScene();

        pathLine = GetComponent<LineRenderer>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (normalPhyScene.IsValid())
        {
            normalPhyScene.Simulate(Time.fixedDeltaTime);
        }
    }

    public void pathCreation(GameObject ball, Vector3 currentPos, Vector3 force)
    {
        if (normalPhyScene.IsValid() && predictionPhyScene.IsValid())
        {
            if (ballCopy == null)
            {
                ballCopy = Instantiate(ball);
                SceneManager.MoveGameObjectToScene(ballCopy, predictionScene);
            }

            ballCopy.transform.position = currentPos;
            ballCopy.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            pathLine.positionCount = 0;
            pathLine.positionCount = maxIterations;

            for (int i = 0; i < maxIterations; i++)
            {
                predictionPhyScene.Simulate(Time.fixedDeltaTime);
                pathLine.SetPosition(i, ballCopy.transform.position);
            }

            Destroy(ballCopy);
        }

    }


}
