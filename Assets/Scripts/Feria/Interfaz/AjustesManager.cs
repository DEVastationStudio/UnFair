using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AjustesManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<Button> _botones;
    [SerializeField] private List<GameObject> _pestañas;
    [SerializeField] private List<Slider> _primerosAjustes;

    private int _activeId = 0;

    #endregion Variables

    #region Metodos

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
                nav.selectOnDown = _primerosAjustes[n];
                _botones[i].navigation = nav;
            }
        }
    }

    #endregion Metodos
}
