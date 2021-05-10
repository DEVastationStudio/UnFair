using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ConversationHelper : MonoBehaviour
{
    public enum CompareMode { LT, LTE, EQ, GTE, GT }
    public ConversationEvent[] OnConversationEndEvents;
    public ConversationEvent[] OnConversationPath;
    public PrefsInt[] requisites;
    [SerializeField] private GameObject _interactUI;
    private PlayerInput _playerInput;
    private PlayerController _player;
    private int _conversationPath;
    private DialogueSystemTrigger _trigger;
    private bool _isInTrigger;

    void Start() {
        _trigger = GetComponent<DialogueSystemTrigger>();
        _player = FindObjectOfType<PlayerController>();
        _playerInput = FindObjectOfType<PlayerInput>();

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
            c.Invoke();
        }

        if (OnConversationPath.Length > _conversationPath) OnConversationPath[_conversationPath]?.Invoke();

        if (_isInTrigger) SetNearConversation(true);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") return;

        _isInTrigger = true;
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