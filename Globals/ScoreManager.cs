using Godot;
using System;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }
	public override void _Ready()
	{
		Instance = this;
	}
}
