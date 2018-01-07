using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[Node(false, "Map/Wave")]
public class WaveNode : Node
{
    private string _getId = "WaveNode";

    public override string GetID
    {
        get { return _getId; }
    }

    public GameObject Enemy
    {
        get { return _enemy; }
        set { _enemy = value; }
    }

    public int EnemiesCount
    {
        get { return _enemiesCount; }
        set { _enemiesCount = value; }
    }

    public int Delay
    {
        get { return _delay; }
        set { _delay = value; }
    }

    public int SpawnInterval
    {
        get { return _spawnInterval; }
        set { _spawnInterval = value; }
    }


    [FormerlySerializedAs("Enemy Prefab")] public GameObject _enemy;
    [FormerlySerializedAs("Enemy Count")] public int _enemiesCount;
    [FormerlySerializedAs("Start Delay")] public int _delay;

    [FormerlySerializedAs("Spawn Interval")]
    public int _spawnInterval;

    [ValueConnectionKnob("Previous", Direction.None, "WaveConnection", NodeSide.Top, 30)]
    public ValueConnectionKnob prev;

    [ValueConnectionKnob("Next", Direction.Out, "WaveConnection", NodeSide.Bottom, 30)]
    public ValueConnectionKnob next;

    public bool IsRunning { get; set; }


    public Action<WaveNode> OnComplete;
    public bool IsComplete { get; private set; }

    public void Complete()
    {
        IsComplete = true;
        if (OnComplete != null) OnComplete.Invoke(this);
    }

    public bool IsReady()
    {
        foreach (WaveNode node in getPrev())
        {
            if (!node.IsComplete)
            {
                return false;
            }
        }

        return true;
    }


    public override void NodeGUI()
    {
        GameObject go = RTEditorGUI.ObjectField(Enemy, allowSceneObjects: false);
        if (GUI.changed)
        {
            if (go && go.GetComponent<Enemy>())
            {
                _enemy = go;
            }
        }


        //EditorGUI.ObjectField(new Rect(0,0,100,100), enemy, typeof(Enemy),false);
        // (WaveModel) EditorGUILayout.ObjectField(model, typeof(WaveModel), true);
        Delay = RTEditorGUI.IntField("Spawn delay", Delay);
        _enemiesCount = RTEditorGUI.IntField("Spawn count", this.EnemiesCount);
        _spawnInterval = RTEditorGUI.IntField("Spawn interval", this.SpawnInterval);

        if (GUI.changed)
        {
            NodeEditor.curNodeCanvas.OnNodeChange(this);
        }
    }

    public bool isFirst()
    {
        return getPrev().Count == 0;
    }

    public List<WaveNode> getPrev()
    {
        return getTargetNodes<WaveNode>(prev);
    }

    public List<WaveNode> getNext()
    {
        return getTargetNodes<WaveNode>(next);
    }


    //public List<WaveNode> getInner()
    //{
    //    return getTargetNodes<WaveNode>(waves);
    //}
}

public class WaveConnection : ValueConnectionType // : IConnectionTypeDeclaration
{
    public override Type Type
    {
        get { return typeof(float); }
    }

    public override string Identifier
    {
        get { return "WaveConnection"; }
    }

    public override Color Color
    {
        get { return Color.magenta; }
    }
}