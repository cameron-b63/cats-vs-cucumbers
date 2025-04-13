using Godot;
using System;

public enum powerUpType
	{
		Speed,
		Jump
	}

public partial class PowerUp : Area2D
{
	// buffs and their duration
	[Export] public float SpeedBuff = 1.4f;
	[Export] public float JumpBuff = 1.3f;
	[Export] public double PowerUpDuration = 5.0;
	[Export] public powerUpType Type = powerUpType.Speed;

	// timer for tracking duration and affected player for reference
	private Timer _timer;
	private Player affectedPlayer;

	public override void _Ready()
	{
		// creates the timer
		_timer = new Timer();
		_timer.OneShot = true;
		_timer.WaitTime = PowerUpDuration;
		AddChild(_timer);
		
		_timer.Timeout += OnPowerUpExpired;
		
		// removes the sprites
		GetNode<Sprite2D>("SpeedSprite").Visible = false;
		GetNode<Sprite2D>("JumpSprite").Visible = false;
		// removes the collision barriers
		GetNode<CollisionShape2D>("SpeedCollision").SetDeferred("disabled", true);
		GetNode<CollisionShape2D>("JumpCollision").SetDeferred("disabled", true);
		
		// chooses which power up to create
		switch (Type)
		{
			case powerUpType.Speed:
				GetNode<Sprite2D>("SpeedSprite").Visible = true;
				GetNode<CollisionShape2D>("SpeedCollision").SetDeferred("disabled", false);
				break;

			case powerUpType.Jump:
				GetNode<Sprite2D>("JumpSprite").Visible = true;
				GetNode<CollisionShape2D>("JumpCollision").SetDeferred("disabled", false);
				break;
		}
	}

	private void _on_body_entered(Node body)
	{
		// if the player collides with the hitbox
		if (body is Player player)
		{
			affectedPlayer = player;
			
			GetNode<Sprite2D>("SpeedSprite").Visible = false;
			GetNode<Sprite2D>("JumpSprite").Visible = false;

			// Disable collisions to prevent accidental re-triggers
			GetNode<CollisionShape2D>("SpeedCollision").SetDeferred("disabled", true);
			GetNode<CollisionShape2D>("JumpCollision").SetDeferred("disabled", true);
			
			// gives specified power up 
			switch (Type)
			{
				case powerUpType.Speed:
					affectedPlayer.Speed = (int)(affectedPlayer.Speed * SpeedBuff);
					break;
				
				case powerUpType.Jump:
					affectedPlayer.JumpVelocity = (int)(affectedPlayer.JumpVelocity * JumpBuff);
					break;
			}
			// starts the timer
			_timer.Start();
		}
	}
	
	private void OnPowerUpExpired()
	{
		// gets rid of power up applied
		switch (Type)
		{
			case powerUpType.Speed:
					affectedPlayer.Speed = (int)(affectedPlayer.Speed / SpeedBuff);
					break;
				
			case powerUpType.Jump:
					affectedPlayer.JumpVelocity = (int)(affectedPlayer.JumpVelocity / JumpBuff);
					break;
					
		}
		// frees up power up queue
		QueueFree();
	}
}
