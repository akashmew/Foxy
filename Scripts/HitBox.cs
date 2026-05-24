using Godot;
using System;

[Tool]
public partial class HitBox : Area2D
{
	
	[Export] public Shape2D Shape
	{
		get => _shape;
		set
		{
			_shape = value;
			if (Engine.IsEditorHint() && _collisionShape != null)
			{
				_collisionShape.Shape = _shape;
				_collisionShape.QueueRedraw();
            }
        }
    }
	 private Shape2D _shape;
	[Export] private CollisionShape2D _collisionShape;
    public override void _Ready()
	{
		_collisionShape.Shape = _shape;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
