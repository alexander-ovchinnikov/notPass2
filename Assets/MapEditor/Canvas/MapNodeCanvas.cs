using System.Collections.Generic;
using NodeEditorFramework;

[NodeCanvasType("Map Canvas")]
public class MapNodeCanvas : NodeCanvas
{
    public override string canvasName
    {
        get { return "Map"; }
    }

    public string Name = "Map";

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    
	public IEnumerable<LevelNode> GetAllLevels()
	{
		foreach (Node node in nodes) {
			if (node is LevelNode) {
				yield return (LevelNode)node;
			}
		}
	}
}