using Godot;
using System;

public partial class EnemyBase : CharacterBody2D
{
    [Export] private VisibleOnScreenNotifier2D _screenNotifier;
    [Export] private HitBox _hitBox;
    [Export] protected AnimatedSprite2D _animatedSprite;

    [Export] protected float _speed = 30f;

    [Export] protected float _FallOffY = 200f;
    [Export] protected Timer _attackTimer;
    [Export] private int _points = 5;

    protected float _gravity = 800f;

    protected Player _playerRef;

    public override void _Ready()
    {
        _playerRef = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_PLAYER) as Player;
        if (_playerRef == null)
        {
            GD.Print("not found the player");
            QueueFree();
        }
        _attackTimer.Timeout += Attack;
        _screenNotifier.ScreenEntered += StartTimer;
        _hitBox.AreaEntered += Explode;
    }

    private void Explode(Area2D area)
    {
        SignalHub.EmitPointsScored(_points);
        SignalHub.CreateExplosion(area.GlobalPosition);
        SignalHub.CreatePickups(area.GlobalPosition);
        QueueFree();
    }

    protected virtual void StartTimer()
    {
        GD.Print("timer was started");
        _attackTimer.Start();
        _screenNotifier.ScreenEntered -= StartTimer;

    }

    protected virtual void Attack()
    {
        GD.Print("Now attacking");
    }

    public override void _Process(double delta)
    {
        FallenOff();
    }

    private void FallenOff()
    {
        if (GlobalPosition.Y > _FallOffY)
        {
            CallDeferred(MethodName.QueueFree);
        }
    }

    protected Vector2 ApplyGravity(double delta)
    {
        Vector2 velocity = Velocity;
        velocity.Y += _gravity * (float)delta;
        return velocity;
    }

    protected virtual void FlipMe()
    {
        _animatedSprite.FlipH = _playerRef.GlobalPosition.X< Position.X ? false : true;
    }
}
