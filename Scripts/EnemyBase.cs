using Godot;
using System;

public partial class EnemyBase : CharacterBody2D
{
	[Export]private VisibleOnScreenNotifier2D _screenNotifier;
	[Export] private HitBox _hitBox;
	[Export] protected AnimatedSprite2D _animatedSprite;

	[Export] protected float _speed = 30f;

    [Export] protected float _FallOffY = 200f;

    protected float _gravity = 800f;

    public override void _Process(double delta)
    {
        FallenOff();
    }

    private void FallenOff()
    {
        if(GlobalPosition.Y > _FallOffY)
        {
          CallDeferred(MethodName.QueueFree);
        }
    }
}
