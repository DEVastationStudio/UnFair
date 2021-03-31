using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ConversationHelper : MonoBehaviour
{
    public ConversationEvent[] OnConversationPath;
    [SerializeField] private GameObject _interactUI;
    private PlayerInput _playerInput;
    private PlayerController _player;
    private int _conversationPath;
    private DialogueSystemTrigger _trigger;

    void Start() {
        _trigger = GetComponent<DialogueSystemTrigger>();
        _player = FindObjectOfType<PlayerController>();
        _playerInput = FindObjectOfType<PlayerInput>();
    }

    public void SetConversationEnd(int path)
    {
        _conversationPath = path;
    }

    void OnConversationEnd(Transform actor)
    {
        _trigger.enabled = false;
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
        _trigger.enabled = true;
        _playerInput.SwitchCurrentActionMap("UIMap");
    }
}

[System.Serializable]
public class ConversationEvent : UnityEvent{}
