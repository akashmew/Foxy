using Godot;
using System;

public partial class GameManager : Node
{
	PackedScene _mainScene = GD.Load<PackedScene>("res://Scenes/main.tscn");
	PackedScene _levelBase = GD.Load<PackedScene>("res://Scenes/level_base.tscn");
	public static GameManager Instance { get; private set; }
	public override void _Ready()
	{
		Instance = this;
	}

	public void LoadGameScene()
	{
		GetTree().ChangeSceneToPacked(_levelBase);
	}

	public void LoadMainScene()
	{
		GetTree().ChangeSceneToPacked(_mainScene);
	}

	public static void ChangeToGameScene()
	{
		Instance.LoadGameScene();
	}
	public static void ChangeToMainScene()
	{
		Instance.LoadMainScene();
	}
	
}
