using Godot;
using System;

public partial class NextLevel : Area2D
{
	public override void _Ready()
	{
		// creates the sprite and collision barrier
		GetNode<Sprite2D>("NextLevelSprite").Visible = true;
		GetNode<CollisionShape2D>("NextLevelCollision").SetDeferred("disabled", false);
	}
	
	private void _on_body_entered(Node body)
	{
		// checks if the colliding body is a player or not
		if (body.IsInGroup("Player"))
		{
			GD.Print("Player entered next level zone");
			CallDeferred(nameof(LoadNextScene));
		}
	}
	private void LoadNextScene()
	{
		if (Global.Instance.MainScene == null)
		{
			GD.PrintErr("MainScene missing from Global.");
			return;
		} 
		
		// changes to next level
		if (Global.Instance.ShouldLoadNextLevel)
		{
			Global.Instance.MainScene.StartLevel(Global.Instance.NextLevelSource);	
		} else 
		{
			Global.Instance.MainScene.ShowWinScreen();
		}
	}
}
