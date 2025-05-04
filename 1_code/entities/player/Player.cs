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
	private Area2D _attackHitBox;
	
	private bool _isAttacking = false;
	private bool _facingRight = true;
	private string _selectedCharacter = "";
	private string _attackAnimation = "";
	private Vector2 _defaultHitboxPosition;

	public override void _Ready()
	{
		// set current health
		CurrentHealth = MaxHealth;
		
		// gets the selected character
		var selected = Global.SelectedCharacter;
		_selectedCharacter = selected;
		_attackAnimation = selected + "Attack";
		_animatedSprite2D.Play(_selectedCharacter);
		
		// get animations and attack hitbox for attack control
		GetNode<Area2D>("SwordsmanAttack").Visible = false;
		GetNode<Area2D>("SwordsmanAttack").Monitoring = false;
		GetNode<Area2D>("BerserkerAttack").Visible = false;
		GetNode<Area2D>("BerserkerAttack").Monitoring = false;
		GetNode<Area2D>("MageAttack").Visible = false;
		GetNode<Area2D>("MageAttack").Monitoring = false;

		// Assign correct hitbox
		_attackHitBox = GetNodeOrNull<Area2D>(_attackAnimation);

		if (_attackHitBox == null)
		{
			GD.PushError($"[Player.cs] Could not find hitbox at path: {_attackHitBox}");
			return; // prevent the rest of _Ready() from running
		}
	
		// Set the hitbox to true
		_attackHitBox.Visible = true;
		_attackHitBox.Monitoring = false;

		// Set animation
		_animatedSprite2D.Play(_selectedCharacter);
		
		// Listen for animation finished
		_animatedSprite2D.AnimationFinished += OnAnimationFinished;
		
		_defaultHitboxPosition = _attackHitBox.Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Apply gravity
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

		// Handles animations
		if (!_isAttacking)
		{
			if (direction.X != 0)
			{
				_animatedSprite2D.FlipH = direction.X < 0;
				_facingRight = direction.X > 0;
				_animatedSprite2D.Play();
				_attackHitBox.Scale = new Vector2(_facingRight ? 1 : -1, 1);
			}
			else
			{
				_animatedSprite2D.Stop();
			}
		}
		
		// if the player attacks, call the attack function
		if(Input.IsActionJustPressed("player_attack"))
		{
			Attack();
		}
	}
	
	public void TakeDamage(int amount)
	{
		// tracks player health and signals if the player dies
		CurrentHealth -= amount;
		
		if(CurrentHealth <= 0)
		{
			CallDeferred(nameof(Die));
		}
	}
	
	private void Die()
	{
		// death signal
		GD.Print("Player died :(");
		Global.Instance.MainScene.StartLevel(Global.Instance.CurrentLevelSource);
	}
	
	private void Attack()
	{
		// prevents a retrigger
		if (_isAttacking  || _attackHitBox == null) return; 
		
		// handles attack animations
		_isAttacking = true;
		_animatedSprite2D.Play(_attackAnimation);
		_animatedSprite2D.Frame = 0;
		_attackHitBox.Monitoring = true;
		
		// flips the player's attack hitbox if they face the other direction
		//_attackHitBox.Scale = new Vector2(_facingRight ? 1 : -1, 1);
		_attackHitBox.Scale = new Vector2(_facingRight ? 1 : -1, 1);
	}
	
	private void _on_attackHitBox_body_entered(Node2D body)
	{
		if (_attackHitBox == null || !_attackHitBox.Monitoring) return;
			
		// if the player hits a cucumber, signal the cucumber to lose life
		if(body is Cucumber1 cucumber && cucumber != null)
		{
			cucumber.CucumberTakeDamage(1);
			GD.Print("Cucumber hit sword");
			_attackHitBox.SetDeferred("monitoring", false);
		}
	}
	
	private void OnAnimationFinished()
	{
		// handles disabling the attack animation after the player finishes
		if (_animatedSprite2D.Animation == _attackAnimation)
		{
			_isAttacking = false;
			_attackHitBox.Monitoring = false;
			_animatedSprite2D.Play(_selectedCharacter);
		}
	}
}
