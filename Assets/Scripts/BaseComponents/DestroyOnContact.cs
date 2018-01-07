using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    [HideInInspector] [SerializeField] public string targetTag = "";

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == targetTag)
            StartCoroutine(DestroyAsync());
    }

    private IEnumerator DestroyAsync()
    {
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
    }
}

[CustomEditor(typeof(DestroyOnContact))]
public class DestroyOnContactEditor : Editor
{
    private string _tag;


    private void OnEnable()
    {
        _tag = (target as DamageOnContactEffect).targetTag;
    }

    public override void OnInspectorGUI()
    {
        var t = target as DamageOnContactEffect;
        // Draw the default inspector
        DrawDefaultInspector();
        _tag = EditorGUILayout.TagField(_tag);
        t.targetTag = _tag;

        EditorUtility.SetDirty(target);
    }
}