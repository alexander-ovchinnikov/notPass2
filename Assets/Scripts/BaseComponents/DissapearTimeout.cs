using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearTimeout : MonoBehaviour
{
    [SerializeField] private int timeout = 10;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DissapearCoroutine());
    }

    private IEnumerator DissapearCoroutine()
    {
        yield return new WaitForSeconds(timeout);
        Destroy(this.gameObject);
    }
}