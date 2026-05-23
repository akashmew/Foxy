using Godot;
using System;

public partial class SignalHub : Node
{
	public static SignalHub Instance { get; private set; }
	public override void _Ready()
	{
		Instance = this;
	}
}
