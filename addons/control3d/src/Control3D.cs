using Godot;

[Tool]
public partial class Control3D : Node3D
{
	[Export(PropertyHint.Range, "0, 100, 0.1")]
	public float RayDistance = 2f;

	[Export]
	public bool InputEnabled = true;

	private Area3D _captureArea;
	private SubViewport _viewport;
	private MeshInstance3D _display;

	private Vector2 _meshSize;
	private Vector2 _halfMeshSize;
	private Vector2 _viewportSize;
	private bool _mouseEntered = false;

	public override void _Ready()
	{
		base._Ready();
	}
}
