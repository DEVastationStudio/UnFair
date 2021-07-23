using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace testing
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _playedGamesTiroAlBlanco; 
        [SerializeField]
        private TextMeshProUGUI _playedGamesCaballos; 
        [SerializeField]
        private TextMeshProUGUI _playedGamesPatos; 
        //[SerializeField]
        //private TextMeshProUGUI _playedGamesCanicas;
        void Start()
        {
            LoadNumOfGames();
            FadeController.FinishLoad();   
        }

        private void LoadNumOfGames()
        {
            //Tiro al blanco
            if(PlayerPrefs.HasKey("playedGamesTiroAlBlanco"))
                _playedGamesTiroAlBlanco.text = PlayerPrefs.GetInt("playedGamesTiroAlBlanco")+"/10";
            else
            {
                PlayerPrefs.SetInt("playedGamesTiroAlBlanco", 0);
                _playedGamesTiroAlBlanco.text = "0/10";
            }
        
            ////Canicas
            //if(PlayerPrefs.HasKey("playedGamesCanicas"))
            //    _playedGamesCanicas.text = PlayerPrefs.GetInt("playedGamesCanicas")+"/10";
            //else
            //{
            //    PlayerPrefs.SetInt("playedGamesCanicas", 0);
            //    _playedGamesCanicas.text = "0/10";
            //}
        
            //Caballos
            if(PlayerPrefs.HasKey("playedGamesCaballos"))
                _playedGamesCaballos.text = PlayerPrefs.GetInt("playedGamesCaballos")+"/10";
            else
            {
                PlayerPrefs.SetInt("playedGamesCaballos", 0);
                _playedGamesCaballos.text = "0/10";
            }
        
            //Patos
            if(PlayerPrefs.HasKey("playedGamesPatos"))
                _playedGamesPatos.text = PlayerPrefs.GetInt("playedGamesPatos")+"/10";
            else
            {
                PlayerPrefs.SetInt("playedGamesPatos", 0);
                _playedGamesPatos.text = "0/10";
            }
        }

        public void Fade(string scene)
        {
            FadeController.Fade(scene);
        }

        public void ResetStats()
        {
        
            PlayerPrefs.SetInt("playedGamesTiroAlBlanco", 0);
            PlayerPrefs.SetInt("playedGamesPatos", 0);
            PlayerPrefs.SetInt("playedGamesCaballos", 0);
            //PlayerPrefs.SetInt("playedGamesCanicas", 0);
            LoadNumOfGames();
            Directory.Delete(Application.dataPath + "/../Minigame_Data");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
