using Godot;
using System;

public partial class LifeTime : Node
{
	[Export] Timer _timer;
	[Export] float _waitTime=30f;
	public override void _Ready()
	{
		_timer.Start(_waitTime);
		_timer.Timeout += OnTimeout;
	}

    private void OnTimeout()
    {
		 GetParent().QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
