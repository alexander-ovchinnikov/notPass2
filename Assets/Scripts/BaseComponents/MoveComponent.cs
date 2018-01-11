using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveComponent : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private int _distanceX = 2000;

    [SerializeField] private int speed = 2;

    [SerializeField] private bool _stopOnCollide = false;
    [SerializeField] private string targetTag = "";

    private Rigidbody2D _rb;
    private Tweener moving;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        Vector3 endPos = gameObject.transform.position;
        endPos.x = _distanceX;
        //moving = gameObject.transform.DOMove(endPos, speed);
    }

    void FixedUpdate()
    {
        var velocity = new Vector3(_distanceX, 0, 0);
        var pos = transform.position + velocity * Time.fixedDeltaTime * speed;
        _rb.MovePosition(pos);
    }


    private void Update()
    {
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_stopOnCollide)
        {
            if (other.gameObject.tag == targetTag)
                moving.Kill();
        }
    }
}