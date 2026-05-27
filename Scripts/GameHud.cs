using Godot;
using System;

public partial class GameHud : Control
{
	[Export] ColorRect _colorRect;
	[Export] Label _gameOverLabel;
	[Export] AudioStreamPlayer _gameOverSfx;
	[Export] AudioStreamPlayer _levelCompletedSfx;
	[Export] Timer _gameOverTimer;

	private bool _canContinue = false;

	public override void _UnhandledInput(InputEvent @event)
	{
		
		if (@event.IsActionPressed("quit"))
		{
			GameManager.Instance.LoadMainScene();
		}

		if (_canContinue && @event.IsActionPressed("shoot"))
		{
			GameManager.Instance.LoadMainScene();
		//SignalHub.CreateBullet(new Vector2(150, -50), new Vector2(1, 1), 50f, _bulletBase);

		}
	}
	public override void _Ready()
	{
		_colorRect.Hide();
		SignalHub.Instance.OnLevelCompleted += OnLevelComplete;
		_gameOverTimer.Timeout += OnTimeOut;
	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnLevelCompleted -= OnLevelComplete;
    }

   
    private void OnLevelComplete()
	{
		_colorRect.Show();
		_gameOverLabel.Text = "LevelCompleted";
		_levelCompletedSfx.Play();
		_gameOverTimer.Start();
		GetTree().Paused = true;

	}
	 private void OnTimeOut()
    {
		_canContinue = true;
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
