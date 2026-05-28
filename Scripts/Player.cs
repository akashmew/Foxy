using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	private const float GRAVITY = 690.0f;
	private readonly Vector2 HURT_JUMP_VEL = new Vector2(0f, -130f);
	private readonly float _FallOffY = 300f;

	private float _movementSpeed = 120.0f;
	private float _jumpSpeed = -270.0f;
	private const float MAX_FALL = 300f;
	private bool _isJumped = false;
	private bool _isHurt = false;
	private bool _isInvicible = false;

	public bool IsStill { get { return Mathf.IsZeroApprox(Velocity.X); } }
	public bool IsTouchingFloor { get { return IsOnFloor(); } }
	public bool IsFalling { get { return Velocity.Y > 0; } }
	public bool IsHurt { get { return _isHurt; } }


	[Export] AudioStreamPlayer2D _jumpSound;
	[Export] AudioStreamPlayer2D _hurtSound;
	[Export] Sprite2D _sprite;
	[Export] Shooter _shooter;
	[Export] HitBox _hitbox;
	[Export] Timer _hurtTimer;
	[Export] int _lives=5;
	[Export] AnimationPlayer _invinciblePlayer;

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("jump"))
		{
			_isJumped = true;

		}
		if (@event.IsActionPressed("shoot"))
		{
			Vector2 direction = _sprite.FlipH ? Vector2.Left : Vector2.Right;
			_shooter.Shoot(direction);
		}
	}

	public override void _EnterTree()
	{
		AddToGroup(GameConstants.GROUP_PLAYER);
	}


	public override void _Ready()
	{
		_hitbox.AreaEntered += OnAreaEntered;
		//_hitbox.AreaExited += OnAreExited;
		_hurtTimer.Timeout += ResetPlayer;
		_invinciblePlayer.AnimationFinished += OnInvAnimFinished;
		CallDeferred(nameof(LateEmit));
	}

   
    private void LateEmit()
	{
		SignalHub.ReduceLife(_lives, false);
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void _PhysicsProcess(double delta)
	{

		var velocity = Velocity;
		velocity.Y += GRAVITY * (float)delta;

		velocity = GetInput(velocity);

		velocity.Y = Mathf.Clamp(velocity.Y, _jumpSpeed, MAX_FALL);

		Velocity = velocity;

		MoveAndSlide();
		FallenOff();
	}

	private Vector2 GetInput(Vector2 velocity)
	{
		if (_isHurt) return velocity;
		velocity.X = Input.GetAxis("left", "right") * _movementSpeed;

		if (IsOnFloor() && _isJumped)
		{
			velocity.Y =_jumpSpeed;
			_isJumped = false;
			_jumpSound.Play();
		}

		if (!Mathf.IsZeroApprox(velocity.X))
		{
			_sprite.FlipH = velocity.X < 0;
		}
		return velocity;

	}

	private void ApplyGravity(double delta)
	{
		var velocity = Velocity;
		velocity.Y += GRAVITY * (float)delta;
		Velocity = velocity;
		MoveAndSlide();
	}

	private void ApplyHurt()
	{
		_isHurt = true;
		_hurtSound.Play();
		_hurtTimer.Start();
		Velocity = HURT_JUMP_VEL;

	}

	private void Reduction(int reduction)
	{
		_lives -= reduction;
		SignalHub.ReduceLife(_lives, false);
	}
	private void ApplyHit()
	{
		if (_isInvicible) return;
		GoInvincible();
		ApplyHurt();
		Reduction(1);
		

	}

	private void ResetPlayer()
	{
		_isHurt = false;

	}

	private void OnAreaEntered(Area2D area)
	{
		CallDeferred(nameof(ApplyHit));

	}
	
	


 private void FallenOff()
	{
		if (GlobalPosition.Y > _FallOffY)
		{
			Reduction(_lives);
			CallDeferred(MethodName.QueueFree);
			SignalHub.CompletedLevel(false);
		}
	}
	public void GoInvincible()
	{
		if (_isInvicible) return;
		_isInvicible = true;
		_invinciblePlayer.Play("invincible");
	}
	private void OnInvAnimFinished(StringName animName)
	{
		_isInvicible = false;
		_invinciblePlayer.Play("RESET");
	}


}
