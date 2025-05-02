using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 300;

	[Export]
	public int JumpVelocity { get; set; } = -600;

	[Export]
	public int Gravity { get; set; } = 1000;

	private AnimatedSprite2D animatedSprite2D;

	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
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
			animatedSprite2D.FlipH = direction.X < 0;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}
	}
}
