using Godot;
using System;

// written by: Gabe

public partial class NextLevelController : Node2D
{
	[Export]
	private PackedScene nextLevelScene;
	
	public override void _Ready()
	{
		// calls the level marker creator
		// TODO: make levels spawn on top of a Node2D "LevelEnd" found in each level file.
		SpawnLevelMarker(new Vector2(3500, 450));
	}
	
	public void SpawnLevelMarker(Vector2 position)
	{
		// creates the level marker
		NextLevel nextLevel = (NextLevel)nextLevelScene.Instantiate();
		nextLevel.Position = position;
		AddChild(nextLevel);
	}
}
