using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button btn_play, btn_tutorial, btn_exit;
    public Button btn_1, btn_2;

    public GameObject tutorial, mode;

    void Start()
    {
        btn_play.onClick.AddListener(() =>
        {
            mode.SetActive(true);
        });
        btn_1.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        btn_2.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        btn_tutorial.onClick.AddListener(() =>
        {
            tutorial.SetActive(true);
        });
        btn_exit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

}
