using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private int _distanceX = 2000;

    [SerializeField] private int speed = 2;

    [SerializeField] private bool _stopOnCollide = false;
    [SerializeField] private string targetTag = "";

    private Tweener moving;

    void Start()
    {
        Vector3 endPos = gameObject.transform.position;
        endPos.x = _distanceX;
        moving = gameObject.transform.DOMove(endPos, speed);
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