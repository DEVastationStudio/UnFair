using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers;

public class DialogueSkipButton : MonoBehaviour
{
    [SerializeField] private StandardDialogueUI _dialogueUI;
    private PlayerInputs inputs;
    public bool skip;


    // Start is called before the first frame update
    void Awake()
    {
        inputs = new PlayerInputs();
    }

    void TrySkip()
    {
        skip = true;
        _dialogueUI.OnContinue();

    }

    void OnConversationLine(Subtitle subtitle)
    {
        if (skip) 
            StartCoroutine(RemindSkip());
    }

    void OnConversationStart(Transform actor)
    {
        skip = false;
        StopCoroutine(RemindSkip());
    }

    IEnumerator RemindSkip()
    {
        yield return new WaitForSeconds(0.1f);
        if (DialogueManager.DisplaySettings.subtitleSettings.continueButton != DisplaySettings.SubtitleSettings.ContinueButtonMode.Never)
            _dialogueUI.OnContinue();

    }


    void OnConversationResponseMenu(Response[] responses)
    {
        skip = false;
    }

    void OnConversationEnd(Transform actor)
    {
        skip = false;
    }

    private void OnEnable()
    {
        inputs.Enable();
        InputDeviceManager.RegisterInputAction("Esc Action", inputs.UIMap.EscAction);
    }

    private void OnDisable()
    {
        InputDeviceManager.UnregisterInputAction("Esc Action");
        inputs.Disable();
    }
}
