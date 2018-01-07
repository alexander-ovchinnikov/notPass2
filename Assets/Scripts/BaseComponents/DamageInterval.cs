using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class DamageInterval : MonoBehaviour
{
    [SerializeField] private string targetTag = "";
    [SerializeField] private int _damage = 10;
    [SerializeField] private int _hitInterval = 1;

    private Coroutine cooldown;
    private bool _canHit = true;


    IEnumerator StartCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(_hitInterval);
            _canHit = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == targetTag)
            if (cooldown == null)
            {
                cooldown = StartCoroutine(StartCooldown());
            }
    }

    private void OnCollisionStay(Collision other)
    {
        if (_canHit)
        {
            if (other.gameObject.tag == targetTag)
            {
                var target = other.gameObject.GetComponent<IDamagable>();
                target.GetHit(_damage);
                _canHit = false;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}