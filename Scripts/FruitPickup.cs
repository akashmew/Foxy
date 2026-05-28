using Godot;
using System;

public partial class FruitPickup : Area2D
{
	[Export] AnimatedSprite2D _animatedSprite;
	[Export] AudioStreamPlayer2D _pickupSfx;
	[Export] private int _points = 2;
	public override void _Ready()
	{
		PlayRandomAnimation();
		AreaEntered += OnAreaEntered;
		_pickupSfx.Finished += QueueFree;
	}


	private void PlayRandomAnimation()
	{
		var animSprite = _animatedSprite.SpriteFrames.GetAnimationNames();
		if (animSprite.Length > 0)
		{
			string randName = animSprite[new Random().Next(animSprite.Length)];
			_animatedSprite.Play(randName);
		}
	}
	private void OnAreaEntered(Area2D area)
	{
		SignalHub.EmitPointsScored(_points);
		_pickupSfx.Play();
		Hide();
		AreaEntered -= OnAreaEntered;
    }

}
