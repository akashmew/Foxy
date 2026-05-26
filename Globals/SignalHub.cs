using Godot;
using System;

public partial class SignalHub : Node
{
	public static SignalHub Instance { get; private set; }

	[Signal] public delegate void OnCreateBulletEventHandler(Vector2 pos, Vector2 dir, float speed, PackedScene scene);
	[Signal] public delegate void OnExplosionEventHandler(Vector2 pos);
	[Signal] public delegate void OnEnemyKilledEventHandler(Vector2 pos);
	public override void _Ready()
	{
		Instance = this;
	}

	public static void CreateBullet(Vector2 pos, Vector2 dir, float speed, PackedScene scene)
	{
		Instance.EmitSignal(SignalName.OnCreateBullet, pos, dir, speed, scene);
	}

	public static void CreateExplosion(Vector2 pos)
	{
		Instance.EmitSignal(SignalName.OnExplosion, pos);
	}

	public static void CreatePickups(Vector2 pos)
	{
		Instance.EmitSignal(SignalName.OnEnemyKilled, pos);
	}
}
