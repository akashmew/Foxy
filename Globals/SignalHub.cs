using Godot;
using System;

public partial class SignalHub : Node
{
	public static SignalHub Instance { get; private set; }

	[Signal] public delegate void OnCreateBulletEventHandler(Vector2 pos, Vector2 dir, float speed, PackedScene scene);
	[Signal] public delegate void OnExplosionEventHandler(Vector2 pos);
	[Signal] public delegate void OnEnemyKilledEventHandler(Vector2 pos);
	[Signal] public delegate void OnBossKilledEventHandler();
	[Signal] public delegate void OnLevelCompletedEventHandler(bool isWin);
	[Signal] public delegate void OnPointsScoredEventHandler(int points);
	[Signal] public delegate void OnReduceLivesEventHandler(int lives, bool shake);
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

	public static void SpawnCheckPointFlag()
	{
		Instance.EmitSignal(SignalName.OnBossKilled);
	}

	public static void CompletedLevel(bool isWin)
	{
		Instance.EmitSignal(SignalName.OnLevelCompleted,isWin);
	}

	public static void EmitPointsScored(int points)
	{
		Instance.EmitSignal(SignalName.OnPointsScored, points);
	}

	public static void ReduceLife(int lives, bool shake)
	{
		Instance.EmitSignal(SignalName.OnReduceLives, lives, shake);
	}
}
