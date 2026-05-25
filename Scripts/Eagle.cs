using Godot;
using System;

public partial class Eagle : EnemyBase
{
	private readonly Vector2 FLY_SPEED = new Vector2(35.0f, 15.0f);

	private Vector2 _flyDirection = Vector2.Zero;
	[Export] RayCast2D _playerDetection;
	[Export] Shooter _shooter;

	protected override void StartTimer()
	{
		base.StartTimer();
		_animatedSprite.Play("eagle");
		FlyToPlayer();

	}

	public override void _Ready()
	{
		base._Ready();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Velocity = _flyDirection;
		MoveAndSlide();
		ShootPlayer();
	}

    private void ShootPlayer()
    {
		if (_playerDetection.IsColliding())
		{
			
			_shooter.Shoot(GlobalPosition.DirectionTo(_playerRef.GlobalPosition));

		}
    }

    private void FlyToPlayer()
	{
		FlipMe();
		float xDirection = _animatedSprite.FlipH ? 1f : -1;
		_flyDirection = new Vector2(FLY_SPEED.X * xDirection, FLY_SPEED.Y);

	}

	protected override void Attack()
	{
		FlyToPlayer();
	}
}
