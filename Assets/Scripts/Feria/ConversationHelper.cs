using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ConversationHelper : MonoBehaviour
{
    public ConversationEvent[] OnConversationPath;
    public PrefsInt[] requisites;
    [SerializeField] private GameObject _interactUI;
    private PlayerInput _playerInput;
    private PlayerController _player;
    private int _conversationPath;
    private DialogueSystemTrigger _trigger;

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
                    if (PlayerPrefs.GetInt(f.name, 0) != f.value)
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
        OnConversationPath[_conversationPath].Invoke();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") return;

        _player._isNearConversation = true;
        _player._conversation = this;
        _interactUI.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        _player._isNearConversation = false;
        _interactUI.SetActive(false);
    }
    public void StartConversation()
    {
        _trigger.OnUse();
        _playerInput.SwitchCurrentActionMap("UIMap");
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
}