using Godot;
using System;

public partial class LevelBase : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _UnhandledInput(InputEvent @event)
    {
		if (@event.IsActionPressed("quit"))
		{
			GameManager.Instance.LoadMainScene();
		}
    }

}
