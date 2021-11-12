using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHP : CharacterBase
{    
    // Update is called once per frame
    void Update()
    {
        DisplayHP();
        DisplayMana();
    }
}
