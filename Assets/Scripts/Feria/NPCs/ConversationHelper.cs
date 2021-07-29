using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Globalization;
using UnityEngine.UI;

public class ConversationHelper : MonoBehaviour
{
    public enum CompareMode { LT, LTE, EQ, GTE, GT }
    public ConversationEvent[] OnConversationEndEvents;
    public ConversationEvent[] OnConversationPath;
    public PrefsInt[] requisites;
    [SerializeField] private GameObject _interactUI;
    private Image _interactUISprite;
    private PlayerInput _playerInput;
    private PlayerController _player;
    private int _conversationPath;
    private DialogueSystemTrigger _trigger;
    private bool _isInTrigger;

    public static NpcSpriteManager npcSpriteManager;

    void Start() {
        _trigger = GetComponent<DialogueSystemTrigger>();
        if (_player == null) _player = FindObjectOfType<PlayerController>();
        _playerInput = FindObjectOfType<PlayerInput>();
        if (_interactUI != null)
        {
            _interactUISprite = _interactUI.GetComponent<Image>();
        }
    }

    void Update()
    {
        if (_interactUI != null && _interactUI.activeSelf)
        {
            _interactUISprite.enabled = _playerInput.currentActionMap.name.Equals("ActionMap");
        }
    }

    public void SetConversationEnd(int path)
    {
        _conversationPath = path;
    }

    void OnConversationEnd(Transform actor)
    {

        _playerInput.SwitchCurrentActionMap("ActionMap");

        foreach(ConversationEvent c in OnConversationEndEvents)
        {
            c?.Invoke();
        }

        if (OnConversationPath.Length > _conversationPath) OnConversationPath[_conversationPath]?.Invoke();

        if (_isInTrigger) SetNearConversation(true);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") return;

        _isInTrigger = true;

        if (_player == null) _player = FindObjectOfType<PlayerController>();
        _player._conversation = this;
        
        SetNearConversation(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;

        _isInTrigger = false;
        SetNearConversation(false);
    }
    public void StartConversation()
    {
        //Esto es probable que lo mueva al método que actualiza cuando se cambia un playerpref si va muy mal
        if (requisites.Length != 0)
        {
            foreach(PrefsInt p in requisites)
            {
                bool success = true;
                foreach (IntTuple f in p.conditions)
                {
                    if (!success) continue;
                    
                    bool condition = false;
                    int testValue = PlayerPrefs.GetInt(f.name, 0);
                    switch (f.compareMode)
                    {
                        case ConversationHelper.CompareMode.LT:
                            condition = (testValue < f.value);
                            break;
                        case ConversationHelper.CompareMode.LTE:
                            condition = (testValue <= f.value);
                            break;
                        case ConversationHelper.CompareMode.GT:
                            condition = (testValue > f.value);
                            break;
                        case ConversationHelper.CompareMode.GTE:
                            condition = (testValue >= f.value);
                            break;
                        case ConversationHelper.CompareMode.EQ:
                        default:
                            condition = (testValue == f.value);
                            break;
                    }
                    if (!condition)
                    {
                        success = false;
                    }
                }
                if (success)
                {
                    _trigger.conversation = p.conversation;
                }
            }
        }

        if(_interactUI!=null)
            _interactUI.SetActive(false);
        _trigger.OnUse();
        _playerInput.SwitchCurrentActionMap("UIMap");
    }

    public void Fade(string scene)
    {
        _isInTrigger = false;
        FadeController.Fade(scene);
    }

    private void SetNearConversation(bool near)
    {
        _player._isNearConversation = near;
        _interactUI.SetActive(near);
    }

    public void SetProgression(int progression)
    {
        PlayerPrefs.SetInt("Progression", progression);
        if (npcSpriteManager != null) npcSpriteManager.UpdateSprites();
    }

    public void IncreaseInternalProgression(string name)
    {
        int value = PlayerPrefs.GetInt(name, 0);
        value++;
        if (value <= 2)
        {
            PlayerPrefs.SetInt(name, value);
        }
        else
        {
            PlayerPrefs.SetInt(name, 2);
        }
    }

    public void ChangeSong(int index)
    {
        if (AudioManager.instance != null) AudioManager.instance.changeTheme(index);
    }

    public void MoveNpc(string args)
    {
        string[] data = args.Split(';');
        if (data.Length != 2 && data.Length != 3)
        {
            Debug.LogError("MoveNpc needs at least two coordinates, in x;y[;duration] format.");
        }
        Vector2 position = new Vector2(float.Parse(data[0], CultureInfo.InvariantCulture), float.Parse(data[1], CultureInfo.InvariantCulture));
        float duration = data.Length == 3 ? float.Parse(data[2], CultureInfo.InvariantCulture) : 2;
        StartCoroutine(MoveNpcCR(position, duration));
    }

    private IEnumerator MoveNpcCR(Vector2 position, float duration)
    {
        float elapsedTime = 0;
        Vector3 oldPos = transform.position;
        Vector3 newPos = new Vector3(position.x, oldPos.y, position.y);
        //Quaternion oldRot = transform.rotation;
        //Quaternion newRot = oldRot * Quaternion.Euler(0, 180, 0);

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, elapsedTime/duration);
            elapsedTime += Time.deltaTime;
            //transform.rotation = Quaternion.Slerp(oldRot, newRot, elapsedTime/duration);
            yield return new WaitForEndOfFrame();
        }
        transform.position = newPos;
        //transform.rotation = newRot;
    }
    public void MovePlayer(string args)
    {
        string[] data = args.Split(';');
        if (data.Length != 2 && data.Length != 3)
        {
            Debug.LogError("MoveNpc needs at least two coordinates, in x;y[;duration] format.");
        }
        Vector2 position = new Vector2(float.Parse(data[0], CultureInfo.InvariantCulture), float.Parse(data[1], CultureInfo.InvariantCulture));
        float duration = data.Length == 3 ? float.Parse(data[2], CultureInfo.InvariantCulture) : 2;
        _player.MovePlayer(position, duration);
    }

}

[System.Serializable]
public class ConversationEvent : UnityEvent{}


[System.Serializable]
public class PrefsInt
{
    public IntTuple[] conditions;
    public string conversation;
}
[System.Serializable]
public class IntTuple
{
    public string name;
    public int value;
    public ConversationHelper.CompareMode compareMode = ConversationHelper.CompareMode.EQ;
}