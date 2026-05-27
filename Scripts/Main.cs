using Godot;
using System;

public partial class Main : Control
{
    public override void _Ready()
    {
		GetTree().Paused = false;
    }

	// Called when the node enters the scene tree for the first time.
	public override void _UnhandledInput(InputEvent @event)
	{
		GD.Print("Getting the Input");
		if (@event.IsActionPressed("shoot"))
		{
			GameManager.Instance.LoadGameScene();
		}
		if (@event.IsActionPressed("quit"))
		{
			GetTree().Quit();
		}
	}
}
