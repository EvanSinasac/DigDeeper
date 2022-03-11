using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Matthew Pizzo - 
 * Last Worked On: January 17, 2021
 * */

public class ToolHit : MonoBehaviour
{
    public virtual void Hit(int damage){

    }

    public virtual bool CanBeHit(List<ResourceNodeType> canBeHit) {

        return true;
    }

    public virtual bool CanGetSlayerBuff(List<EnemyType> canGetSlayerBuff)
    {

        return true;
    }
}
