using Godot;
using System;

public partial class Checkpoint : Area2D
{
	[Export] AnimationTree _animationTree;
	public override void _Ready()
	{
		SignalHub.Instance.OnBossKilled += OnBossKilled;
		_animationTree.AnimationFinished += OnAnimatinFinished;
		AreaEntered += OnAreaEntered;
	}

	private void OnAreaEntered(Area2D area)
	{
		SignalHub.CompletedLevel();
		AreaEntered -= OnAreaEntered;
    }


    private void OnAnimatinFinished(StringName animName)
    {
		if (animName == "open")
		{
			SetDeferred(Area2D.PropertyName.Monitoring, true);
		}
    }

    public override void _ExitTree()
	{
		SignalHub.Instance.OnBossKilled -= OnBossKilled;
	}

    private void OnBossKilled()
    {
		_animationTree.Set("parameters/conditions/bossKilled", true);
    }

}
