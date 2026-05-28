using Godot;
using System;

public partial class MovingPlatform : AnimatableBody2D
{
    [Export] private float _speed = 150f;
    [Export] private Marker2D _from;
    [Export] private Marker2D _to;

    public override async void _Ready()
    {
        if (_from == null || _to == null)
        {
            QueueFree();
            return;
        }

        GlobalPosition = _from.GlobalPosition;

        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

        TweenPlatform(_to.GlobalPosition);
    }

    private void TweenPlatform(Vector2 target)
    {
        float totalTime = GlobalPosition.DistanceTo(target) / _speed;

        var tween = CreateTween();

        tween.TweenProperty(
            this,
            Node2D.PropertyName.GlobalPosition.ToString(),
            target,
            totalTime
        );

        tween.Finished += () =>
        {
            Vector2 newTarget =
                target == _to.GlobalPosition
                ? _from.GlobalPosition
                : _to.GlobalPosition;

            TweenPlatform(newTarget);
        };
    }
}