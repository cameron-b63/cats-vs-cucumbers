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
			CallDeferred(nameof(LoadNextScene));
		}
	}
	private void LoadNextScene()
	{
		// changes to next level, currently restarts level  
		// until next levels are implemented
		GetTree().ReloadCurrentScene();
	}
}
