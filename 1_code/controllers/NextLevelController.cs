using Godot;
using System;

// written by: Gabe

public partial class NextLevelController : Node2D
{
	[Export]
	private PackedScene nextLevelScene;
	
	[Export]
	private bool AutoSpawnOnReady = true;
	
	public override void _Ready()
	{
		if (AutoSpawnOnReady)
		{
			SpawnLevelMarker(new Vector2(3500, 450));
		}
	}
	
	public void SpawnLevelMarker(Vector2 position)
	{
		// creates the level marker
		NextLevel nextLevel = (NextLevel)nextLevelScene.Instantiate();
		nextLevel.Position = position;
		AddChild(nextLevel);
	}
}
