using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs
{
    public static string NumberPlay = "number_play";
    public static int NumberPlayer
    {
        get => PlayerPrefs.GetInt(NumberPlay, 1);

        set => PlayerPrefs.SetInt(NumberPlay, value);
    }
}
