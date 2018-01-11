using Game;
using UnityEditor;
using UnityEngine;

public class DamageOnContactEffect : MonoBehaviour
{
    [SerializeField] private int damage = 0;
    [HideInInspector] [SerializeField] public string targetTag = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damage(other.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Damage(other.gameObject);
    }

    private void Damage(GameObject other)
    {
        if (other.tag == targetTag)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.GetHit(this.damage);
            }
        }
    }
}

[CustomEditor(typeof(DamageOnContactEffect))]
public class DamageOnContactEditor : Editor
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
        if (t != null) t.targetTag = _tag;

        EditorUtility.SetDirty(target);
    }
}