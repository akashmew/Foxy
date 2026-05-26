using Godot;
using System;

public partial class ObjectMaker : Node
{
	[Export] PackedScene _explosionScene;
	[Export] PackedScene _pickups;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnCreateBullet += OnCreateBullet;
		SignalHub.Instance.OnExplosion += OnExplosion;
		SignalHub.Instance.OnEnemyKilled += OnEnemyKill;
	}

  
    public override void _ExitTree()
	{
		SignalHub.Instance.OnCreateBullet -= OnCreateBullet;
		SignalHub.Instance.OnExplosion -= OnExplosion;
		SignalHub.Instance.OnEnemyKilled -= OnEnemyKill;
	}

	private void OnExplosion(Vector2 pos)
	{
		Explosion explosion = _explosionScene.Instantiate<Explosion>();
		explosion.GlobalPosition = pos;
		CallDeferred(MethodName.AddObject, explosion);
		
    }

    private void AddObject(Node node)
	{
		AddChild(node);
	}

	private void OnCreateBullet(Vector2 pos, Vector2 dir, float speed, PackedScene scene)
	{
		Bullets bullets = scene.Instantiate<Bullets>();
		bullets.ShootBullets(pos, dir, speed);
		CallDeferred(MethodName.AddObject,bullets);
	}
  	private void OnEnemyKill(Vector2 pos)
    {
        FruitPickup pickup = _pickups.Instantiate<FruitPickup>();
		pickup.GlobalPosition = pos;
		CallDeferred(MethodName.AddObject, pickup);
    }

  
}
