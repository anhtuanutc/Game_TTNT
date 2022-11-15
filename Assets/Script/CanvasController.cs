using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;
    public GameObject popup_end;
    public TextMeshProUGUI txt_win, txt_score_1, txt_score_2;

    private void Awake()
    {
        instance = this;
    }

    public void Win(int score1, int score2)
    {
        popup_end.SetActive(true);
        string s;
        if (score1 > score2)
        {
            s = Prefs.NumberPlayer == 2 ? "Player 1 win!" : "You winner!";
        }
        else if (score1 == score2)
        {
            s = "No people win! Continue playing.";
        }
        else
        {
            s = Prefs.NumberPlayer == 2 ? "Player 2 win!" : "Computer win!";
        }
        txt_win.text = s;
        txt_score_1.text = Prefs.NumberPlayer == 2 ? "Player 1: " : "You: " + score1;
        txt_score_2.text = Prefs.NumberPlayer == 2 ? "Player 2: ": "Computer: " + score2;
        Time.timeScale = 0;
    }
}
