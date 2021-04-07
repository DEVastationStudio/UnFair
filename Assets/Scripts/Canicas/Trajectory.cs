using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int maxIterations;
    private Scene normalScene;
    private Scene predictionScene;
    private PhysicsScene normalPhyScene;
    private PhysicsScene predictionPhyScene;
    private LineRenderer pathLine;
    private GameObject ballCopy;
    [SerializeField] private GameObject[] obstacles;
    private List<GameObject> predictionObstacles = new List<GameObject>();
    private Ray ray;
    [SerializeField] private GameObject thrower;

    void Awake()
    {
        /*Physics.autoSimulation = false;

        normalScene = SceneManager.GetActiveScene();
        normalPhyScene = normalScene.GetPhysicsScene();

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("Trajectory", parameters);
        predictionPhyScene = predictionScene.GetPhysicsScene();
        CreatePredicObstacles();*/

        

        FadeController.FinishLoad();
    }
    void Start()
    {
        pathLine = GetComponent<LineRenderer>();
        FadeController.FinishLoad();
        //ray = new Ray(thrower.transform.position, thrower.transform.forward);
        //Debug.DrawRay(thrower.transform.position, thrower.transform.forward, Color.red);
    }

    void Update()
    {
        //Debug.DrawRay(thrower.transform.position, thrower.transform.forward * 100f, Color.red);
    }

    void FixedUpdate()
    {
        /*if (normalPhyScene.IsValid())
        {
            normalPhyScene.Simulate(Time.fixedDeltaTime);
        }*/
    }

    private void CreatePredicObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i].gameObject.GetComponent<Collider>() != null)
            {
                GameObject fakeT = Instantiate(obstacles[i].gameObject);
                fakeT.transform.position = obstacles[i].transform.position;
                fakeT.transform.rotation = obstacles[i].transform.rotation;
                Renderer fakeR = fakeT.GetComponent<Renderer>();
                if (fakeR)
                {
                    fakeR.enabled = false;
                }
                SceneManager.MoveGameObjectToScene(fakeT, predictionScene);
                predictionObstacles.Add(fakeT);
            }

        }
        /*foreach(Transform t in obstacles.transform){
            if(t.gameObject.GetComponent<Collider>() != null){
                GameObject fakeT = Instantiate(t.gameObject);
                fakeT.transform.position = t.position;
                fakeT.transform.rotation = t.rotation;
                Renderer fakeR = fakeT.GetComponent<Renderer>();
                if(fakeR){
                    fakeR.enabled = false;
                }
                SceneManager.MoveGameObjectToScene(fakeT, predictionScene);
                dummyObstacles.Add(fakeT);
            }
        }*/
    }

    public void PathCreation(GameObject ball, Vector3 currentPos, Vector3 force)
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
            Marble marble = ballCopy.GetComponent<Marble>();
            pathLine.positionCount = 0;
            pathLine.positionCount = maxIterations;//luego cambiar esto a lo que ha salido de una
            int aux = 0;
            for (int i = 0; i < maxIterations; i++)
            {
                if (marble.GetCollided())
                {
                    aux++;
                }
                predictionPhyScene.Simulate(Time.fixedDeltaTime);
                pathLine.SetPosition(i, ballCopy.transform.position);
                if (aux > 2)
                {
                    pathLine.positionCount = i;
                    Destroy(ballCopy);
                    return;
                }
            }
            Destroy(ballCopy);
        }
    }

    public void LineCreation(Vector3 origin, Vector3 dst)
    {
        pathLine.positionCount = 0;
        pathLine.positionCount = 2;
        pathLine.SetPosition(0, origin);
        pathLine.SetPosition(1, dst);
    }
}
