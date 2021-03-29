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

    void Awake()
    {
        Physics.autoSimulation = false;

        normalScene = SceneManager.GetActiveScene();
        normalPhyScene = normalScene.GetPhysicsScene();

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("Trajectory", parameters);
        predictionPhyScene = predictionScene.GetPhysicsScene();
        CreatePredicObstacles();

        pathLine = GetComponent<LineRenderer>();

        FadeController.FinishLoad();
    }
    void Start()
    {
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
            pathLine.positionCount = 0;
            pathLine.positionCount = maxIterations;//luego cambiar esto a lo qaue ha salkido de una que mal qesceiboi adjhaesjhfgszgjkbhdhkjfhjksdfghbkjsdkf

            for (int i = 0; i < maxIterations; i++)
            {
                predictionPhyScene.Simulate(Time.fixedDeltaTime);
                pathLine.SetPosition(i, ballCopy.transform.position);
            }

            Destroy(ballCopy);
        }

    }

}
