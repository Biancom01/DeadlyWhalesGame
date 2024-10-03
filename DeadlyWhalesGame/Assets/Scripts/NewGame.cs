using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    // Referencja do przycisku
    public Button startNewGameButton;

    void Start()
    {
        // Dodajemy listener do przycisku
        startNewGameButton.onClick.AddListener(StartNewGame);
    }

    // Funkcja zmieniaj�ca scen�
    void StartNewGame()
    {
        // Prze��czamy scen� na "GameScene"
        SceneManager.LoadScene("GameScene");
    }
}
