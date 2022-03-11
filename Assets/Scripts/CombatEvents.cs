﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvents : MonoBehaviour
{

    public delegate void EnemyEventHandler(EnemyController enemy);
    public static event EnemyEventHandler OnEnemyDeath;

    public static void EnemyDied (EnemyController enemy)
    {
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath(enemy);
        }
    }
}
