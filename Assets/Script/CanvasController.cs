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
            s = "Player 1 win!";
        }
        else if (score1 == score2)
        {
            s = "No people win! Continue playing";
        }
        else
        {
            s = "Player 2 win!";
        }
        txt_win.text = s;
        txt_score_1.text = "Player 1: " + score1;
        txt_score_2.text = "Player 2: " + score2;
        Time.timeScale = 0;
    }
}
