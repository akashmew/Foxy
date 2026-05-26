using Godot;
using System;

public partial class Bullets : Area2D
{
	private float _bulletSpeed = 100;
	private Vector2 _direction = Vector2.Right;
	public override void _Ready()
	{
		AreaEntered += DestroyBullet;
	}

    private void DestroyBullet(Area2D area)
    {
		QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		Position += _direction * (float)delta;
	}

	public void ShootBullets(Vector2 spawnPos, Vector2 dir, float speed)
	{
		GlobalPosition = spawnPos;
		_direction = dir * speed;
    }

}
