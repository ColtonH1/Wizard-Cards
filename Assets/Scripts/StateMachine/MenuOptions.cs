using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOptions : MonoBehaviour
{
    public void OnPause()
    {
        Time.timeScale = 0.0f;
    }

    public void OnPlay()
    {
        Time.timeScale = 1.0f;
    }
}
