using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class Damagable : MonoBehaviour, IDamagable
{
    private int _health = 10;

    public void GetHit(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}