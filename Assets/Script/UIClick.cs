using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIClick : MonoBehaviour
{
    public void CloseParent()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void ChangeActive(GameObject obj)
    {
        obj.SetActive(!obj.activeInHierarchy);
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetTimeScale(int scale)
    {
        Time.timeScale = scale;
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
