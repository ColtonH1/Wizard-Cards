using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Menu", menuName = "Menu")]
public class SettingsSO : ScriptableObject
{

    public float soundSetting;
    public bool screenToggle;
    public string playerName;
    public Sprite[] playerSprites;
    public Sprite[] enemySprites;
    public int playerCharNum;
    public int enemyCharNum;
    public Sprite playerChar;
    public Sprite enemyChar;
    public int difficulty;

}
