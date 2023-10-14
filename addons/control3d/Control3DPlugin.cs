#if TOOLS
using Godot;

[Tool]
public partial class Control3DPlugin : EditorPlugin
{
	public override void _EnterTree()
	{
		var script = GD.Load<Script>("res://addons/control3d/src/Control3D.cs");
		var texture = GD.Load<Texture2D>("res://icon.svg");
		AddCustomType("Control3D", "Node3D", script, texture);
	}

	public override void _ExitTree()
	{
		RemoveCustomType("Control3D");
	}
}
#endif
