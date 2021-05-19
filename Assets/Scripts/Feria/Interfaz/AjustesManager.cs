using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AjustesManager : MonoBehaviour
{
    #region Variables
    [Header("Navigation")]
    [SerializeField] private List<Button> _botones;
    [SerializeField] private List<GameObject> _pestañas;
    [SerializeField] private List<GameObject> _primerosAjustes;

    [Header("Quality")]
    [SerializeField] private TMP_Dropdown _qualityDropdown;

    [Header("Resolution")]
    [SerializeField] private TMP_Dropdown _resolutionsDropdown;

    [Header("FullScreen")]
    [SerializeField] private Toggle _fullScreenToggle;

    [Header("FrameRate")]
    [SerializeField] private Slider _fpsSlider;
    [SerializeField] private TextMeshProUGUI _fpsTextValue;

    [Header("VSync")]
    [SerializeField] private Toggle _vsyncToggle;

    private Resolution[] _resolutions;
    private List<Resolution> _resolutionsToUse;
    private int _activeId = 0;
    #endregion Variables

    #region Metodos

    private void Start()
    {
        _resolutions = Screen.resolutions;
        _resolutionsDropdown.ClearOptions();
        _resolutionsToUse = new List<Resolution>();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        string option;
        string lastOption = "";
        for (int i = 0; i<_resolutions.Length; i++) 
        { 
            option = _resolutions[i].width + " x " + _resolutions[i].height;
            if (!option.Equals(lastOption))
            {
                //Debug.Log(_resolutions[i]);
                _resolutionsToUse.Add(_resolutions[i]);
                options.Add(option);
                if (_resolutionsToUse[_resolutionsToUse.Count-1].width == Screen.currentResolution.width && _resolutionsToUse[_resolutionsToUse.Count - 1].height == Screen.currentResolution.height)
                    currentResolutionIndex = _resolutionsToUse.Count - 1;
            }
            lastOption = option;
            
        }

        _resolutionsDropdown.AddOptions(options);
        _resolutionsDropdown.value = currentResolutionIndex;
        _resolutionsDropdown.RefreshShownValue();
        if (Screen.fullScreen)
            _fullScreenToggle.isOn = true;
        else
            _fullScreenToggle.isOn = false;

        _qualityDropdown.value = PlayerPrefs.GetInt("qualityIndex");
        _qualityDropdown.RefreshShownValue();

        if (PlayerPrefs.GetInt("fpsValue") == 0)
            _fpsSlider.value = (int)Screen.currentResolution.refreshRate;
        else
            _fpsSlider.value = PlayerPrefs.GetInt("fpsValue");

        if (PlayerPrefs.GetInt("vSyncState") != 0)
        {
            _vsyncToggle.isOn = true;
            _fpsSlider.value = (int)Screen.currentResolution.refreshRate;
        }
        else
        {
            _vsyncToggle.isOn = false;
        }
    }

    public void ChangePestaña(int n) 
    {
        _pestañas[_activeId].SetActive(false);
        _activeId = n;
        _pestañas[_activeId].SetActive(true);
        if (n < _primerosAjustes.Count)
        {
            for (int i = 0; i < _pestañas.Count; i++)
            {
                Navigation nav = _botones[i].navigation;
                switch (n) 
                {
                    case 0:
                        nav.selectOnDown = _primerosAjustes[n].GetComponent<Slider>();
                        break;
                    case 1:
                        nav.selectOnDown = _primerosAjustes[n].GetComponent<TMP_Dropdown>();
                        break;
                }
                _botones[i].navigation = nav;
            }
        }
    }

    public void OnFullScreenChange(bool isFullScreen) 
    {
        Screen.fullScreen = isFullScreen;
    }

    public void OnVSyncChange(bool isVSync)
    {
        if (isVSync)
        {
            QualitySettings.vSyncCount = 1;
            _fpsSlider.value = Screen.currentResolution.refreshRate;
            PlayerPrefs.SetInt("vSyncState", 1);
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("vSyncState", 0);
        }
    }

    public void OnFrameRateSliderChange() 
    {
        if (!(_fpsSlider.value == Screen.currentResolution.refreshRate && QualitySettings.vSyncCount == 1)) 
        {
            QualitySettings.vSyncCount = 0;
            _vsyncToggle.isOn = false;
        }


        if (_fpsSlider.value == 241)
        {
            Application.targetFrameRate = -1;
            _fpsTextValue.text = "Unlimited";
        }
        else
        {
            Application.targetFrameRate = (int)_fpsSlider.value;
            _fpsTextValue.text = ((int)_fpsSlider.value).ToString();
        }

        PlayerPrefs.SetInt("fpsValue", (int)_fpsSlider.value);
    }

    public void OnResolutionChange(int resolutionIndex) 
    {
        Resolution res = _resolutionsToUse[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen); 
    }

    public void OnQualityChange(int qualityIndex) 
    {
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    #endregion Metodos
}
