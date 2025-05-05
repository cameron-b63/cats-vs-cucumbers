using Godot;
using System;


// written by: Gabe
// refactored by: Cameron

public partial class CucumberBoss : CharacterBody2D
{
	[Signal]
	public delegate void BossDefeatedEventHandler();
	
	[Export] public float Speed = 100f;
	[Export] public float ChaseRange = 150f;
	public float gravity = 400f;
	public Vector2 velocity = Vector2.Zero;
	
	[Export]
	public int MaxHealth { get; set; } = 10;
	public int CurrentHealth;
	
	private bool moveLeft = true;
	private bool isBossDefeated = false;
	
	[Export]
	private AnimatedSprite2D animatedSprite2D;
	
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var area = GetNode<Area2D>("Area2D");
		CurrentHealth = MaxHealth;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = moveLeft ? Vector2.Left : Vector2.Right;
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
			direction = (Global.Instance.PlayerNode.GlobalPosition - GlobalPosition).Normalized();
			animatedSprite2D.FlipH = direction.X < 0;
		}
		else
		{
			// Patrol movement
			direction = moveLeft ? Vector2.Left : Vector2.Right;
			animatedSprite2D.FlipH = !moveLeft;
		}
		velocity.X = Speed * direction.X;
		Velocity = velocity; 
		MoveAndSlide();
	}
	
	private void _on_area_2d_body_entered(Node2D body)
	{
		if(body is Player player)
		{
			if(player != null)
			{
				player.TakeDamage(1);
			}
			GD.Print("Player touched cucumber!");
		}
	}
	
	public void CucumberTakeDamage(int amount)
	{
		GD.Print("Boss hit!");
		CurrentHealth -= amount;
		
		if(CurrentHealth <= 0)
		{
			CallDeferred(nameof(CucumberDie));
		}
	}
	
	private void CucumberDie()
	{
		isBossDefeated = true;
		EmitSignal(nameof(BossDefeated));
		QueueFree();
	}
}
