using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStarManager : MonoBehaviour
{
    #region Variables

    [Header("Other scripts needed")]
    [SerializeField] private ShootingMinigameManager _gameManager;

    [Header("Number of points player must surpass to get 1 star")]
    public int _condition1;
    [Header("Combo player must surpass to get 3 stars")]
    public int _condition3;

    private bool _isStar1Complete;
    private bool _isStar2Complete;
    private bool _isStar3Complete;

    #endregion Variables

    #region Metodos

    public void CheckStar(int star)
    {
        switch (star)
        {
            //Player must get more points than what specified in _condition1
            case 1:
                if (_gameManager._uiGeneral.GetPuntuacion() >= _condition1)
                    _isStar1Complete = true;
                break;
            //Player must get the Unfair word and enter goldrush
            case 2:
                    _isStar2Complete = true;
                break;
            //Player must surpass a combo score specified in _condition3
            case 3:
                if (_gameManager._comboCounter._maxCombo >= _condition3)
                    _isStar3Complete = true;
                break;
        }
    }

    public void ResetStars()
    {
        _isStar1Complete = false;
        _isStar2Complete = false;
        _isStar3Complete = false;
    }

    public bool GetStar(int starID)
    {
        switch (starID)
        {
            case 1:
                return _isStar1Complete;
            case 2:
                return _isStar2Complete;
            case 3:
                return _isStar3Complete;
            default: return false;
        }
    }

    #endregion Metodos


}
