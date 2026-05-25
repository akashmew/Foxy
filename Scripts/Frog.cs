using Godot;
using System;

public partial class Frog : EnemyBase
{
	private bool _jump = false;
	private Vector2 _velocity = Vector2.Zero;
	public override void _Ready()
	{
		base._Ready();
		_attackTimer.OneShot = true;
		_attackTimer.WaitTime = GD.RandRange(2, 4);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		_velocity = ApplyGravity(delta);
		ApplyJump();
		Velocity = _velocity;
		MoveAndSlide();
		FlipMe();

		if (IsOnFloor())
		{
			_animatedSprite.Play("frog_idle");
			_velocity = Vector2.Zero;
			Velocity = _velocity;
		}
	}

	private void ApplyJump()
	{
		if (IsOnFloor() && _jump)
		{
			_velocity = Velocity;
			_velocity.X = !_animatedSprite.FlipH ? -100 : 100;
			_velocity.Y = -150;
			_jump = false;
			_attackTimer.Start(GD.RandRange(2, 4));
		}
	}

	protected override void Attack()
	{
		base.Attack();
		_animatedSprite.Play("frog_jumping");
		_jump = true;
	}

}
