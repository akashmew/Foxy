using Godot;
using System;

public partial class Boss : Node2D
{
	[Export] private AnimationTree _animationTree;
	[Export] private Area2D _trigger;
	[Export] private Shooter _shooter;
	[Export] private Node2D _visuals;
	[Export] private HitBox _hitBox;
	[Export] private float _lives;
	[Export] private int _points=20;
	protected Player _playerRef;
	private Vector2 _visualsPos;

	private bool _invincible = false;

	private AnimationNodeStateMachinePlayback _state;
	public override void _Ready()
	{

		_playerRef = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_PLAYER) as Player;
		if (_playerRef == null)
		{
			GD.Print("not found the player");
			QueueFree();
		}
		_state = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
		_trigger.AreaEntered += OnAreaEntered;
		_hitBox.AreaEntered += OnHitBoxAreaEntered;
		_animationTree.AnimationFinished += OnAnimationFinished;
		_visualsPos = _visuals.Position;
	}

   
    private void TweenHit()
	{
		Tween tween = CreateTween();
		tween.TweenProperty(
			_visuals,
			Node2D.PropertyName.Position.ToString(),
			_visualsPos,
			1.8f
		);
	}

	private void ReduceLife()
	{
		_lives--;
		if (_lives <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		SignalHub.SpawnCheckPointFlag();
		SignalHub.EmitPointsScored(_points);
		QueueFree();
	}
	private void TakeDamage()
	{
		if (_invincible) return;
		_invincible = true;
		_state.Travel("hit");
		TweenHit();
		ReduceLife();
	}

	private void OnHitBoxAreaEntered(Area2D area)
	{
		TakeDamage();
		
	}

    private void OnAreaEntered(Area2D area)
	{
		_animationTree.Set("parameters/conditions/on_trigger", true);
		_trigger.AreaEntered -= OnAreaEntered;
    }

 	private void OnAnimationFinished(StringName animName)
    {
		if (animName == "hit")
		{
			_invincible = false;
	    }
    }

	public void ActivateHitBox()
	{
		_hitBox.Activate(true);
	}

	public void Shoot()
	{
		_shooter.Shoot(_visuals.GlobalPosition.DirectionTo(_playerRef.GlobalPosition));
	}
    // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
