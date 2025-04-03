// This file contains the scripts associated with the player, such as movement, etc.

using Godot;
using System;

public partial class Player : Area2D
{
	// Member variables
	[Export]	// Used to ensure we can edit this value inside the inspector
	public int Speed { get; set; } = 400;	// Player speed measured in pixels per second
		
	public Vector2 ScreenSize;	// Window size
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;	// Screen size when player instantiated
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 velocity = Vector2.Zero;	// Strongly typed is best
		
		// See input map in project settings for below definitions
		if (Input.IsActionPressed("move_right")) {
			velocity.X += 1;
		}
		
		if (Input.IsActionPressed("move_left")) {
			velocity.X -= 1;
		}
		
		AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		// Normalize velocity to ensure the player can't move faster by moving diagonally
		if (velocity.Length() > 0) {
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		} else {
			animatedSprite2D.Stop();
		}
		
		// Keep player on-screen
		Position += velocity * (float)delta;	// Using delta keeps movmement smooth even across framerate variation
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}
}
