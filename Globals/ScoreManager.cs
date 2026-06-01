using Godot;
using System;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }

	public HighScores ScoresHistory { get;private set; } = new HighScores();

	public int cachedScore { get; set; }
	public override void _Ready()
	{
		Instance = this;
	}
}
