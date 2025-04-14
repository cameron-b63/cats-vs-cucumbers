using Godot;
using System;

public partial class Enemy : RigidBody2D
{
	[Export] public float PatrolSpeed = 150f;
	[Export] public float ChaseSpeed = 150f;
	[Export] public float ChaseRange = 250f;

	private bool moveLeft = true;
	private AnimatedSprite2D animatedSprite2D;
	private Node2D player;
	
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		player = GetTree().Root.GetNode<Node2D>("Main_World/Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		// returns if there is no player detected
		if (player == null)
			return;
		
		// gets distance of enemy instance from player
		float distance = GlobalPosition.DistanceTo(player.GlobalPosition);
		
		// If player is within range, chase
		if (distance < ChaseRange)
		{
			Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
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
