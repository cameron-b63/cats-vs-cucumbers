using Godot;
using System;

// written by: Gabe
// refactored by: Cameron

public partial class Cucumber1 : RigidBody2D
{
	[Export] public float PatrolSpeed = 150f;
	[Export] public float ChaseSpeed = 150f;
	[Export] public float ChaseRange = 250f;

	private bool moveLeft = true;
	
	[Export]
	private AnimatedSprite2D animatedSprite2D;
	
	public override void _Ready()
	{
		GD.Print("Cucumber 1 is ready to go.");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		// returns if there is no player detected
		if (Global.Instance.PlayerNode == null) 
		{
			GD.PrintErr("Player node was null in Global singleton.");
			return;
		}
		
		// gets distance of enemy instance from player
		float distance = GlobalPosition.DistanceTo(Global.Instance.PlayerNode.GlobalPosition);
		
		// If player is within range, chase
		if (distance < ChaseRange)
		{
			Vector2 direction = (Global.Instance.PlayerNode.GlobalPosition - GlobalPosition).Normalized();
			LinearVelocity = new Vector2(direction.X * ChaseSpeed, LinearVelocity.Y);
			animatedSprite2D.FlipH = direction.X < 0;
		}
		else
		{
			// Patrol movement
			Vector2 direction = moveLeft ? Vector2.Left : Vector2.Right;
			LinearVelocity = direction * PatrolSpeed;
			animatedSprite2D.FlipH = !moveLeft;
		}
	}
}
