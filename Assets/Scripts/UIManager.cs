using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UI Manager is NULL");

            return _instance;
        }
    }

    [SerializeField]
    private Text _coinsText, _livesText, _interactText;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateCoinsText(int coinsCount)
    {
        _coinsText.text = "Coins: " + coinsCount;
    }

    public void UpdateLivesText(int lives)
    {
        _livesText.text = "Lives: " + lives;
    }

    public void DisplayInteractText(bool val)
    {
        _interactText.gameObject.SetActive(val);
    }

}
