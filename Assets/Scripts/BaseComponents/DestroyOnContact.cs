using System.Collections;
using UnityEditor;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    [HideInInspector] [SerializeField] public string targetTag = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == targetTag)
            StartCoroutine(DestroyAsync());
    }

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
        _tag = (target as DestroyOnContact).targetTag;
    }

    public override void OnInspectorGUI()
    {
        var t = target as DestroyOnContact;
        // Draw the default inspector
        DrawDefaultInspector();
        _tag = EditorGUILayout.TagField(_tag);
        if (t != null) t.targetTag = _tag;

        EditorUtility.SetDirty(target);
    }
}