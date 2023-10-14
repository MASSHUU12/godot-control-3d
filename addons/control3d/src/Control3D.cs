using Godot;

[Tool]
public partial class Control3D : Node3D
{
	[Export(PropertyHint.Range, "0, 100, 0.1")]
	public float RayDistance = 2f;

	[Export]
	public bool InputEnabled = true;

	[Export] public Area3D CaptureArea { get; set; }
	[Export] public SubViewport SubViewport { get; set; }
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
		_viewportSize = SubViewport.Size;

		CaptureArea.MouseEntered += () => _mouseEntered = true;
		CaptureArea.MouseExited += () => _mouseEntered = false;
	}

	/// <summary>
	/// Handles the mouse input.
	/// </summary>
	/// <param name="eventMouse">The mouse event.</param>
	private void HandleMouse(InputEventMouse eventMouse)
	{
		Vector3 mousePos3D = FindMouse(eventMouse.GlobalPosition);
		bool mouseInside = mousePos3D != Vector3.Zero;

		// If the mouse is inside the panel,
		// we need to convert the mouse position to the 2D world.
		mousePos3D = mouseInside
			? CaptureArea.GlobalTransform.AffineInverse() * new Vector3(mousePos3D.X, mousePos3D.Y, 0)
			: new(_halfMeshSize.X, -_halfMeshSize.Y, 0);

		Vector2 mousePos2D = MouseTo2DWorld(mousePos3D);
		mousePos2D = ClampMousePosition(mousePos2D);

		eventMouse.Position = eventMouse.GlobalPosition = mousePos2D;
		SubViewport.PushInput(eventMouse);
	}

	/// <summary>
	/// Converts the mouse position to the 2D world.
	/// </summary>
	/// <param name="mousePos3D">The mouse position in the 3D world.</param>
	/// <returns>The mouse position in the 2D world.</returns>
	private Vector2 MouseTo2DWorld(Vector3 mousePos3D)
	{
		Vector2 mousePos2D = new(mousePos3D.X, -mousePos3D.Y);
		mousePos2D += _halfMeshSize;
		mousePos2D /= _meshSize;
		mousePos2D *= _viewportSize;
		return mousePos2D;
	}

	/// <summary>
	/// Clamps the mouse position to the viewport size.
	/// </summary>
	/// <param name="mousePos2D">The mouse position in the 2D world.</param>
	/// <returns>The clamped mouse position.</returns>
	private Vector2 ClampMousePosition(Vector2 mousePos2D)
	{
		return mousePos2D with
		{
			X = Mathf.Clamp(mousePos2D.X, 0, _viewportSize.X),
			Y = Mathf.Clamp(mousePos2D.Y, 0, _viewportSize.Y)
		};
	}

	/// <summarY>
	/// Finds the mouse position in the 3D world.
	/// </summary>
	/// <param name="globalPosition">The global position of the mouse.</param>
	/// <returns>The mouse position in the 3D world.</returns>
	private Vector3 FindMouse(Vector2 globalPosition)
	{
		var camera = GetViewport().GetCamera3D();
		PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;

		var rayOrigin = camera.ProjectRayOrigin(globalPosition);
		var rayEnd = rayOrigin + camera.ProjectRayNormal(globalPosition) * RayDistance;

		PhysicsRayQueryParameters3D physicsRay = new()
		{
			From = rayOrigin,
			To = rayEnd,
			CollideWithBodies = false,
			CollideWithAreas = true
		};

		var result = spaceState.IntersectRay(physicsRay);
		return result.Count > 0 ? (Vector3)result["position"] : Vector3.Zero;
	}
}
