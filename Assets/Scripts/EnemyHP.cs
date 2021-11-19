using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : CharacterBase
{
    void Update()
    {
        DisplayHP();
        DisplayMana();
    }
}
