using Godot;

[Tool]
public partial class Control3D : Node3D
{
	[Export(PropertyHint.Range, "0, 100, 0.1")]
	public float RayDistance = 2f;

	[Export]
	public bool InputEnabled = true;

	[Export] public Area3D CaptureArea { get; set; }
	[Export] public SubViewport Viewport { get; set; }
	[Export] public MeshInstance3D Display { get; set; }

	private Vector2 _meshSize;
	private Vector2 _halfMeshSize;
	private Vector2 _viewportSize;
	private bool _mouseEntered = false;

	public override void _Ready()
	{
		base._Ready();

		// Cache the values.
		_meshSize = Display.Mesh.Get("size").AsVector2();
		_halfMeshSize = _meshSize / 2;
		_viewportSize = Viewport.Size;
	}
}
