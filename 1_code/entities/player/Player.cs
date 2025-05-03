using Godot;
using System;

// written by: Cameron, Nicole
// tested by: Gabe

public partial class Player : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 300;

	[Export]
	public int JumpVelocity { get; set; } = -600;

	[Export]
	public int Gravity { get; set; } = 1000;
	
	[Export]
	public int MaxHealth { get; set; } = 3;
	public int CurrentHealth;

	[Export]
	private AnimatedSprite2D _animatedSprite2D;

	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Apply gravityw
		if (!IsOnFloor())
			velocity.Y += Gravity * (float)delta;

		// Movement input
		Vector2 direction = Vector2.Zero;

		if (Input.IsActionPressed("move_right"))
			direction.X += 1;
		if (Input.IsActionPressed("move_left"))
			direction.X -= 1;

		velocity.X = direction.X * Speed;

		// Jump input
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Update velocity and move the character
		Velocity = velocity;
		MoveAndSlide();

		// Handle animations
		if (direction.X != 0)
		{
			_animatedSprite2D.FlipH = direction.X < 0;
			_animatedSprite2D.Play();
		}
		else
		{
			_animatedSprite2D.Stop();
		}
	}
	
	public void TakeDamage(int amount)
	{
		CurrentHealth -= amount;
		
		if(CurrentHealth <= 0)
		{
			CallDeferred(nameof(Die));
		}
	}
	
	private void Die()
	{
		GD.Print("Player died :(");
		Global.Instance.MainScene.StartLevel(Global.Instance.CurrentLevelSource);
	}
	
	private void _on_attackHitBox_body_entered(Node2D body)
	{
		if(body is Cucumber1 cucumber && cucumber != null)
		{
			cucumber.CucumberTakeDamage(1);
			GD.Print("Cucumber hit sword");
		}
	}
}
