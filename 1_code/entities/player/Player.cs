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
	
	[Export]
	public PackedScene MageProjectileScene { get; set; }
	
	private bool _isAttacking = false;
	private bool _facingRight = true;
	
	private float _mageAttackCooldown = 0.75f;
	private float _lastMageAttackTime = -1f;
	
	private string _selectedCharacter = "";
	private string _attackAnimation = "";
	private Vector2 _originalHitBoxOffset;

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
			return; 
		}
	
		// Set the hitbox to true
		_attackHitBox.Visible = true;
		_attackHitBox.Monitoring = false;

		// Set animation
		_animatedSprite2D.Play(_selectedCharacter);
		
		// Listen for animation finished
		_animatedSprite2D.AnimationFinished += OnAnimationFinished;
		
		// Sets default hitbox position
		_originalHitBoxOffset = _attackHitBox.Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Apply gravity
		if (!IsOnFloor())
			velocity.Y += Gravity * (float)delta;

		// Movement input
		Vector2 _direction = Vector2.Zero;

		if (Input.IsActionPressed("move_right"))
			_direction.X += 1;
		if (Input.IsActionPressed("move_left"))
			_direction.X -= 1;

		velocity.X = _direction.X * Speed;

		// Jump input
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Update velocity and move the character
		Velocity = velocity;
		MoveAndSlide();

		// Handles animations
		if (!_isAttacking)
		{
			if (_direction.X != 0)
			{
				// flip sprite
				bool nowFacingRight = _direction.X > 0;
				_animatedSprite2D.FlipH = !nowFacingRight;
				_facingRight = nowFacingRight;

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
			Attack();
	}
	
	// Takes player's health whenever damamged
	public void TakeDamage(int amount)
	{
		// tracks player health and signals if the player dies
		CurrentHealth -= amount;
		
		if(CurrentHealth <= 0)
			CallDeferred(nameof(Die));
	}
	
	// Death signal
	private void Die()
	{
		GD.Print("Player died :(");
		Global.Instance.MainScene.StartLevel(Global.Instance.CurrentLevelSource);
	}
	
	// Executes the attack animation 
	private void Attack()
	{
		if (_selectedCharacter == "Mage")
			FireMageProjectile();
		else
		{
			// prevents a retrigger
			if (_isAttacking  || _attackHitBox == null) return; 
			
			// handles attack animations
			_isAttacking = true;
			_animatedSprite2D.Play(_attackAnimation);
			_animatedSprite2D.Frame = 0;
			_attackHitBox.Monitoring = true;
		}
	}
	
	// Checks if there is an enemy in the hitbox whenever the player attacks
	private void _on_attackHitBox_body_entered(Node2D body)
	{
		if (_attackHitBox == null || !_attackHitBox.Monitoring) 
			return;
			
		// if the player hits a cucumber, signal the cucumber to lose life
		if(body is Cucumber1 cucumber)
		{
			cucumber.CucumberTakeDamage(1);
			_attackHitBox.SetDeferred("monitoring", false);
		}
		if(body is CucumberBoss boss)
		{
			boss.CucumberTakeDamage(1);
			_attackHitBox.SetDeferred("monitoring", false);
		}
	}
	
	// Resets the animation back to the normal animation
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
	
	// Creates the projectile and shoots it from the spawn point
	private void FireMageProjectile()
	{
		if (MageProjectileScene == null)
		{
			GD.PushError("MageProjectileScene not set.");
			return;
		}

		// Prevent spamming based on elapsed time
		if (Time.GetTicksMsec() - _lastMageAttackTime < _mageAttackCooldown * 1000)
			return;

		_lastMageAttackTime = Time.GetTicksMsec();

		Node2D projectileInstance = MageProjectileScene.Instantiate<Node2D>();
		projectileInstance.Position = GlobalPosition + new Vector2(_facingRight ? 30 : -30, 0);

		if (projectileInstance is MageAttack mageProjectile)
			mageProjectile.Direction = _facingRight ? Vector2.Right : Vector2.Left;

		GetTree().CurrentScene.AddChild(projectileInstance);
		}
}
