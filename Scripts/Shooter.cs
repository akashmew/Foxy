using Godot;
using System;

public partial class Shooter : Node2D
{
	[Export] Timer _shooterTimer;
	[Export] PackedScene _bullet;
	[Export] float _speed=50f;
	[Export] float _shootDelay=0.7f;
	[Export] AudioStreamPlayer2D _shootSound;

	private bool _canShoot = false;
	public override void _Ready()
	{
		_shooterTimer.Timeout += ShootBullet;
		_shooterTimer.WaitTime = _shootDelay;
	}

	public void Shoot(Vector2 direction)
	{
		if (!_canShoot) return;
		_canShoot = false;
		SignalHub.CreateBullet(GlobalPosition, direction, _speed, _bullet);
		_shootSound.Play();
		
	}
    private void ShootBullet()
	{
		_canShoot = true;
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
