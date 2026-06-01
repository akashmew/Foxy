using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameHud : Control
{
	[Export] ColorRect _colorRect;
	[Export] Label _gameOverLabel;
	[Export] Label _levelLabel;
	[Export] Label _scoreLabel;
	[Export] AudioStreamPlayer _gameOverSfx;
	[Export] AudioStreamPlayer _levelCompletedSfx;
	[Export] HBoxContainer _hbContainer;
	List <TextureRect> _heartTex;
	[Export] Timer _gameOverTimer;

	private int totalPointsScored = 0;

	private bool _canContinue = false;
	private bool _didComplete = false;

	public override void _UnhandledInput(InputEvent @event)
	{
		
		if (@event.IsActionPressed("quit"))
		{
			GameManager.Instance.LoadMainScene();
		}

		if (_canContinue && @event.IsActionPressed("shoot"))
		{
			if (_didComplete)
			{
				GameManager.ChangeToNextGameScene();
			}
			else
			{
				GameManager.Reload();
			}
		//SignalHub.CreateBullet(new Vector2(150, -50), new Vector2(1, 1), 50f, _bulletBase);

		}
	}
	public override void _Ready()
	{
		GetTree().Paused = false;
		_levelLabel.Text = $"LV:{GameManager.Instance.currentLevel+1}";
		_scoreLabel.Text = ScoreManager.Instance.cachedScore.ToString("D4");
		_colorRect.Hide();
		SignalHub.Instance.OnLevelCompleted += OnLevelComplete;
		SignalHub.Instance.OnPointsScored += OnPointsScored;
		SignalHub.Instance.OnReduceLives += ReduceLives;
		_gameOverTimer.Timeout += OnTimeOut;
		_heartTex = _hbContainer.GetChildren().OfType<TextureRect>().ToList();
	
	}

    private void ReduceLives(int lives, bool shake)
    {
		if (lives <= 0)
		{
			OnLevelComplete(false);
		}
		
		for (int i = 0; i < _heartTex.Count; i++)
		{

			_heartTex[i].Visible = lives > i;
		}
    }

    public override void _ExitTree()
	{
		SignalHub.Instance.OnLevelCompleted -= OnLevelComplete;
		SignalHub.Instance.OnPointsScored -= OnPointsScored ;
		SignalHub.Instance.OnReduceLives -= ReduceLives ;
    }


	private void OnLevelComplete(bool isWin)
	{

		_colorRect.Show();
		if (isWin)
		{
			_gameOverLabel.Text = "LevelCompleted";
			_levelCompletedSfx.Play();
			_didComplete = true;
		}
		else
		{
			_gameOverLabel.Text = "GameOver";
			_gameOverSfx.Play();
			_didComplete = false;
		}


		_gameOverTimer.Start();
		GetTree().Paused = true;
		

	}
	private void OnTimeOut()
    {
		_canContinue = true;
    }

	private void OnPointsScored(int points)
	{
		if (ScoreManager.Instance.cachedScore > 0) totalPointsScored = ScoreManager.Instance.cachedScore;
		totalPointsScored += points;
		_scoreLabel.Text = totalPointsScored.ToString("D4");
		ScoreManager.Instance.cachedScore = totalPointsScored;
    }



    // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
