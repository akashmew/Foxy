using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
	PackedScene _mainScene = GD.Load<PackedScene>("res://Scenes/main.tscn");
	//PackedScene _levelBase = GD.Load<PackedScene>("res://Scenes/level_base.tscn");
	public static GameManager Instance { get; private set; }

	List<PackedScene> _levels= new List<PackedScene>{
		GD.Load<PackedScene>("res://Scenes/level_1.tscn"),
		GD.Load<PackedScene>("res://Scenes/level_2.tscn")
	};

	public int currentLevel = -1;
	public override void _Ready()
	{
		Instance = this;
	}

	public void LoadNextScene()
	{
		currentLevel++;
		if(currentLevel>=_levels.Count) currentLevel=0;
		GetTree().ChangeSceneToPacked(_levels[currentLevel]);
	}

	public void LoadMainScene()
	{
		currentLevel = -1;
		GetTree().ChangeSceneToPacked(_mainScene);
	}

	public void ReloadCurrentScene()
	{
		GetTree().ChangeSceneToPacked(_levels[currentLevel]);
	}

	public static void ChangeToNextGameScene()
	{
		Instance.LoadNextScene();
	}
	public static void ChangeToMainScene()
	{
		Instance.LoadMainScene();
	}
	public static void Reload()
	{
		Instance.ReloadCurrentScene();
	}
	
}
