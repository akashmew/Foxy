using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	private const float GRAVITY = 690.0f;

	private float _movementSpeed = 120.0f;
	private float _jumpSpeed = -270.0f;
	private const float MAX_FALL = 300f;
	private bool _isJumped = false;

	public bool IsStill { get { return Mathf.IsZeroApprox(Velocity.X); }}
	public bool IsOnFloor { get { return IsOnFloor(); }}
	public bool IsFalling { get { return Velocity.Y<0; }}
	

	[Export] AudioStreamPlayer2D _jumpSound;
	[Export] Sprite2D _sprite;
	[Export] Shooter _shooter;

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
	}

	private Vector2 GetInput(Vector2 velocity)
	{
		velocity.X = Input.GetAxis("left", "right") * _movementSpeed;

		if (IsOnFloor() && _isJumped)
		{
			velocity.Y = _jumpSpeed;
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

}
