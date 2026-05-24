using Godot;
using System;

public partial class Snail : EnemyBase
{
    [Export] private RayCast2D _raycastDetection;
   
  
    private float timeElapsed = 0.0f;
    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        velocity.Y += _gravity * (float)delta;

        if(IsOnFloor())
        {
            velocity.X = _animatedSprite.FlipH ? _speed : -_speed;
          
           
        }

       

        Velocity = velocity;
        MoveAndSlide();
        FlipMe();
    }

    private void FlipMe()
    {
        if (!_raycastDetection.IsColliding())
        {
            _animatedSprite.FlipH = !_animatedSprite.FlipH;
            _raycastDetection.Position = new Vector2(-_raycastDetection.Position.X, _raycastDetection.Position.Y);
        }
           
        

    }
}
