using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DucksGameManager : MonoBehaviour
{
    public Duck duckPrefab;
    private int _playerScore, _aiScore;
    public TMP_Text pScoreText, aScoreText, timerText, titleText, endGameText, countdownText;
    public GameObject mainMenu, menu;
    public LayerMask duckMask;
    public Button startGameButton;
    public RodAiScript aiScript;
    public RodController rodController;
    private bool _spawnedBigDuck;
    private const int maxTime = 60;
    private float _bigDuckSpawnTime;

    [Header("Control por mando")]
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _resetButton;
    [SerializeField] private DynamicDifficultyManager _ddm;
    [SerializeField] private ConversationHelper _npcConversationHelper;
    [SerializeField] private PlayerInput _playerInput;

    [Header("VFX Manager")]
    public VFXManager _vfxManager;

    public int playerScore
    {
        get { return _playerScore; }
        set { OnPlayerScoreUpdate(value); _playerScore = value; }
    }
    public int aiScore
    {
        get { return _aiScore; }
        set { OnAiScoreUpdate(value); _aiScore = value; }
    }

    private float _actualTime;
    public bool gameOver;

    public int totalDucks = 250;

    private int _goldDucks, _blackDucks, _greenDucks, _redDucks;
    public bool gameStarted;
    public bool noBadDucks;

    [SerializeField] private GameObject _ajustes, _basePauseMenu, _titleScreen, _primerAjustesBtn, _startBtn, _entrarAjustesBtn;
    private bool _isPause = false;

    private List<Vector3> _spawnPositions;
    private List<GameObject> _spawnedDucks; 
    [SerializeField] private GameObject _playerMagnet, _aiMagnet;
    public GameObject[] duckSpawnPoints;

    void Start()
    {
        _playerInput.SwitchCurrentActionMap("UIMap");
        
        _spawnedDucks = new List<GameObject>();
        _spawnPositions = new List<Vector3>();

        titleText.text += "\nStars: " + GameProgress.GetStars(3);
        noBadDucks = true;
        _goldDucks  = Mathf.RoundToInt(totalDucks*0.05f);
        _blackDucks = _goldDucks + Mathf.RoundToInt(totalDucks*0.12f);
        _greenDucks = _blackDucks + Mathf.RoundToInt(totalDucks*0.3f);
        _redDucks   = _greenDucks + Mathf.RoundToInt(totalDucks*0.3f);
        _eventSystem.SetSelectedGameObject(_startButton);
        StartCoroutine(GenerateDucks());
    }

    private IEnumerator GenerateDucks()
    {
        /*Duck duck;
        float angle;
        float radius;
        Vector3 pos;
        bool freeSpace;

        int attempts;

        for (int i = 0; i < totalDucks; i++)
        {
            attempts = 0;
            freeSpace = false;
            while (!freeSpace)
            {
                angle = Random.Range(0f, 360f);
                radius = Random.Range(4f, 15f);
                pos = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
                if (!Physics.CheckSphere(pos, 0.03f*40, duckMask))
                {
                    duck = Instantiate(duckPrefab, pos, Quaternion.Euler(-90, 0, 0));
                    duck._gameManager = this;
                    if (i < _goldDucks)
                    {
                        duck.type = Duck.Type.GOLD;
                    }
                    else if (i < _blackDucks)
                    {
                        duck.type = Duck.Type.BLACK;
                    }
                    else if (i < _greenDucks)
                    {
                        duck.type = Duck.Type.PLAYER;
                    }
                    else if (i < _redDucks)
                    {
                        duck.type = Duck.Type.AI;
                    }
                    else
                    {
                        duck.type = Duck.Type.NORMAL;
                    }
                    freeSpace = true;

                    _spawnedDucks.Add(duck.gameObject);
                    _spawnPositions.Add(pos);
                }
                else
                {
                    attempts++;
                    if (attempts%20 == 0)
                    {
                        yield return null;
                    }
                    if (attempts > 200)
                    {
                        break;
                    }
                }
                //yield return null;
            }
        }*/
        startGameButton.interactable = true;
        FadeController.FinishLoad();
        yield return null;
    }

    private void OnPlayerScoreUpdate(int value)
    {
        pScoreText.text = "Player: " + value;
    }
    private void OnAiScoreUpdate(int value)
    {
        aScoreText.text = "Opponent: " + value;
    }

    public void StartGame()
    {
        AudioManager.instance.FadeOut(13,0.1f);
        StartCoroutine(Countdown());
    }
    private IEnumerator Countdown() 
    {
        countdownText.gameObject.SetActive(true);
        int count = 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        countdownText.text = "";
        countdownText.gameObject.SetActive(false);
        
        _actualTime = maxTime;
        _spawnedBigDuck = false;
        gameStarted = true;
        aiScript.enabled = true;
        rodController._mouseDown = false;

        _bigDuckSpawnTime = Random.Range(12, 15);

        _playerInput.SwitchCurrentActionMap("ActionMap");
        AudioManager.instance.FadeIn(13,0.1f);
        yield return TimerUpdate();
    }

    private IEnumerator TimerUpdate()
    {
        while (_actualTime >= 0)
        {
            timerText.text = "Time: " + _actualTime;
            yield return new WaitForSeconds(1);
            _actualTime--;

            if (!gameStarted) yield break;

            if (!_spawnedBigDuck)
            {
                Duck duck;

                if (_actualTime % 3 == 0 && _spawnedDucks.Count < 200)
                {
                    foreach (GameObject s in duckSpawnPoints)
                    {
                        duck = Instantiate(duckPrefab, s.transform.position, Quaternion.Euler(-90, 0, 0));
                        duck._gameManager = this;

                        float rng = Random.Range(0f,1f);

                        if (rng < 0.35f) //0.35
                        {
                            duck.type = Duck.Type.NORMAL;
                        }
                        else if (rng < 0.60f) //0.25
                        {
                            duck.type = Duck.Type.PLAYER;
                        }
                        else if (rng < 0.75f) //0.15
                        {
                            duck.type = Duck.Type.GOLD;
                        }
                        else if (rng < 0.90f) //0.15
                        {
                            duck.type = Duck.Type.BLACK;
                        }
                        else //0.1
                        {
                            duck.type = Duck.Type.TIME;
                        }

                        _spawnedDucks.Add(duck.gameObject);
                    }
                }

                if (_actualTime <= _bigDuckSpawnTime)
                {
                    _spawnedBigDuck = true;
                    duck = Instantiate(duckPrefab, new Vector3(0,4,0), Quaternion.Euler(-90, 0, 0));
                    duck._gameManager = this;
                    duck.type = Duck.Type.BIG;
                    _spawnedDucks.Add(duck.gameObject);
                }
            }
            
        }
        //Finish game
        gameOver = true;
        endGameText.text = "Score: " + _playerScore + "/" + _aiScore;

        //Calculate stars
        int stars = 0;
        if (_playerScore > _aiScore)
        {
            stars = 1;
            int difference = Mathf.Abs(_playerScore-_aiScore);
            if (noBadDucks && difference >= 5)
            {
                stars = 3;
            }
            else if (noBadDucks || difference >= 5)
            {
                stars = 2;
            }
        }
        GameProgress.SetStars(3, stars);
        _ddm.SetValue(1, (stars)/3f);

        //Calculate score difference dynamic difficulty input
        int clampedDiff = Mathf.Clamp(Mathf.Abs(_playerScore-_aiScore), 0, 8);
        _ddm.SetValue(2, (clampedDiff-1)/7f);

        _ddm.SaveParameters();
        endGameText.text += "\nStars: " + stars;
        menu.SetActive(true);
        
        _npcConversationHelper.StartConversation();
    }

    public void ResetScene()
    {
        
        AudioManager.instance.changeTheme(12);

        //FadeController.Fade("Ducks");
        //Hide end-of-game HUD
        menu.SetActive(false);


        //Reset rod positions
        rodController.gameObject.transform.localPosition = Vector3.zero;
        aiScript.gameObject.transform.localPosition = Vector3.zero;

        //Reset variables
        titleText.text += "\nStars: " + GameProgress.GetStars(3);
        noBadDucks = true;
        gameOver = false;
        playerScore = 0;
        aiScore = 0;
        gameStarted = false;
        aiScript.enabled = false;
        noBadDucks = true;

        timerText.text = "Time: 0";

        //Remove generated ducks
        foreach (GameObject g in _spawnedDucks)
        {
            Destroy(g);
        }

        //Respawn ducks
        /*
        Duck duck;
        float angle;
        float radius;

        int i = 0;

        _spawnedDucks.Clear();
        
        foreach (Vector3 pos in _spawnPositions)
        {
            angle = Random.Range(0f, 360f);
            radius = Random.Range(4f, 15f);
            
            duck = Instantiate(duckPrefab, pos, Quaternion.Euler(-90, 0, 0));
            duck._gameManager = this;
            if (i < _goldDucks)
            {
                duck.type = Duck.Type.GOLD;
            }
            else if (i < _blackDucks)
            {
                duck.type = Duck.Type.BLACK;
            }
            else if (i < _greenDucks)
            {
                duck.type = Duck.Type.PLAYER;
            }
            else if (i < _redDucks)
            {
                duck.type = Duck.Type.AI;
            }
            else
            {
                duck.type = Duck.Type.NORMAL;
            }
            
            i++;

            _spawnedDucks.Add(duck.gameObject);
        }*/
        

        //Reset magnet tags
        _playerMagnet.tag = "Magnet";
        _aiMagnet.tag = "Magnet";

        //Show start-of-game HUD
        mainMenu.SetActive(true);
        _eventSystem.SetSelectedGameObject(_startButton);
        _playerInput.SwitchCurrentActionMap("UIMap");
    }

    public void SetUIMap()
    {
        _playerInput.SwitchCurrentActionMap("UIMap");
    }

    public void SetLastDuck(float value)
    {
        _ddm.SetValue(0, value);
        _ddm.SetValue(3, (value>=0.5f?1:0));
    }

    public void OpenAjustes(bool isPause)
    {
        _isPause = isPause;
        _ajustes.SetActive(false);
        if (_isPause)
            _basePauseMenu.SetActive(false);
        else
            _titleScreen.SetActive(false);

        _ajustes.SetActive(true);
        _eventSystem.SetSelectedGameObject(_primerAjustesBtn);
    }
    public void CloseAjustes()
    {
        _ajustes.SetActive(false);
        if (_isPause)
        {
            _basePauseMenu.SetActive(true);
            _eventSystem.SetSelectedGameObject(_startBtn);
        }
        else
        {
            _titleScreen.SetActive(true);
            _eventSystem.SetSelectedGameObject(_entrarAjustesBtn);
        }
    }

    public void IncreaseTimer()
    {
        if (_actualTime > 0)
        {
            _actualTime += 5;
            timerText.text = "Time: " + _actualTime;
        }
    }
}
