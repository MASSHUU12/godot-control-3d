<div align="center">
	<img src="./icon.svg" width="128" />
	<h3>Godot Control3D</h3>
	<p />
	<p>This plugin allows using SubViewports to display 2D interfaces in 3D.</p>
</div>

> [!NOTE]
> SubViewports in Godot 4 are bugged.
>
> They work, but can be problematic, displaying errors that aren't there, or resetting the texture from time to time.
>
> An example of one such bug is [here](https://github.com/godotengine/godot/issues/66247).

## Prerequisites

-   [.NET SDK 7](https://dotnet.microsoft.com/download)
-   [.NET enabled Godot](https://godotengine.org/download)

## Usage

> [!NOTE]
> Detailed information on each class can be found in the individual source files.
>
> You can find an example of using this plugin in the [example](./example) folder.

All you need to do is create a scene with `Control3D` and:

-   `MeshInstance3D` - Used to display the content SubViewport needs:
    -   `Mesh` (preferably `QuadMesh`)
    -   `MaterialOverride` with `ViewportTexture`
-   `Area3D` - Used for mouse capture.
-   `SubViewport` - Used to render UI.

## License

Licensed under [MIT license](./LICENSE).
