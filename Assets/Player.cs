using System.Collections;
using System.Collections.Generic;
using Game;
using ProgressBar;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health = 100;
    [SerializeField] private ProgressRadialBehaviour _healthBar;

    public UnityEvent OnDie = new UnityEvent();

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                Die();
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        _healthBar.Value = _health;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GetHit(int damage)
    {
        _health -= damage;
        _healthBar.Value = _health;
    }

    public void Die()
    {
        OnDie.Invoke();
    }
}