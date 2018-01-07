using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using JetBrains.Annotations;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[Node(false, "Map/Level")]
public class LevelNode : Node, ISaveable
{
    private string _getId = "LevelNode";

    public override string GetID
    {
        get { return _getId; }
    }

    [ValueConnectionKnob("Next", Direction.Out, "LevelConnection", NodeSide.Right, 30)]
    public ValueConnectionKnob next;

    [ValueConnectionKnob("Prev", Direction.In, "LevelConnection", NodeSide.Left, 30)]
    public ValueConnectionKnob prev;

    [ValueConnectionKnob("Wave", Direction.Out, "WaveConnection", NodeSide.Bottom, 30)]
    public ValueConnectionKnob waves;


    [FormerlySerializedAs("LevelCaption")] public string Caption = "";

    [FormerlySerializedAs("Gold")] public int Gold = 0;
    [FormerlySerializedAs("Guid")] public string Guid = string.Empty;
    [FormerlySerializedAs("IsComplete")] public bool IsComplete = false;
    [FormerlySerializedAs("IsAvailable")] public bool IsAvailable = true;

    public override void NodeGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Caption");
        Caption = EditorGUILayout.TextField(this.Caption);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Guid");
        Guid = EditorGUILayout.TextField(this.GetGUID().ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Gold");
        Gold = RTEditorGUI.IntField(this.Gold);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("IsComplete");
        IsComplete = EditorGUILayout.Toggle(IsComplete);
        GUILayout.Label("IsAvailable");
        IsAvailable = EditorGUILayout.Toggle(IsAvailable);
        EditorGUILayout.EndHorizontal();
    }

    public bool isFirst()
    {
        return getPrev().Count == 0;
    }

    public List<LevelNode> getPrev()
    {
        return getTargetNodes<LevelNode>(prev);
    }

    public List<LevelNode> getNext()
    {
        return getTargetNodes<LevelNode>(next);
    }


    public List<WaveNode> getInner()
    {
        return getTargetNodes<WaveNode>(waves);
    }


    public string GetGUID()
    {
        if (Guid == String.Empty)
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        return Guid;
    }

    public void Save()
    {
        Dictionary<string, bool> state = new Dictionary<string, bool>();
        state.Add("IsAvailable", IsAvailable);
        state.Add("IsComplete", IsComplete);

        string data = JsonUtility.ToJson(state);
        PlayerPrefs.SetString(GetGUID(), data);
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey(GetGUID()))
        {
            return;
        }

        string data = PlayerPrefs.GetString(GetGUID());
        Dictionary<string, bool> state = JsonUtility.FromJson<Dictionary<string, bool>>(data);
        IsComplete = state["IsComplete"];
        IsAvailable = state["IsAvailable"];
    }

    public void Complete()
    {
        IsComplete = true;
        foreach (LevelNode nextLevel in getNext())
        {
            nextLevel.IsAvailable = true;
            nextLevel.Save();
        }
        Save();
    }
}


public class LevelConnection : ValueConnectionType // : IConnectionTypeDeclaration
{
    public override string Identifier
    {
        get { return "LevelConnection"; }
    }

    public override Type Type
    {
        get { return typeof(float); }
    }

    public override Color Color
    {
        get { return Color.cyan; }
    }
}