using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] int baseDamage = 1; // TODO make various turret types

    public int GetBaseDamage()
    {
        return baseDamage;
    }
}
