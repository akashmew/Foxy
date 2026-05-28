using Godot;
using System;

public partial class BallSpikes : PathFollow2D
{
	[Export] private float _speed = 50;
	[Export] private float _spinSpeed = 300;

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Progress += _speed * (float)delta;
		RotationDegrees += _spinSpeed * (float)delta;
	}
}
