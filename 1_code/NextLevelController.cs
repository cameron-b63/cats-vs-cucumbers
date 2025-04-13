using Godot;
using System;

public partial class NextLevelController : Node2D
{
	private PackedScene nextLevelScene = GD.Load<PackedScene>("res://nextlevel.tscn");
	
	public override void _Ready()
	{
		// calls the level marker creator
		SpawnLevelMarker(new Vector2(2700, 520));
	}
	
	public void SpawnLevelMarker(Vector2 position)
	{
		// creates the level marker
		NextLevel nextLevel = (NextLevel)nextLevelScene.Instantiate();
		nextLevel.Position = position;
		AddChild(nextLevel);
	}
}
