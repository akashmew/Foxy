using Godot;
using System;

public partial class LevelBase : Node
{

 



  // Called when the node enters the scene tree for the first time.
  public override void _UnhandledInput(InputEvent @event)
  {
    if (@event.IsActionPressed("quit"))
    {
      GameManager.Instance.LoadMainScene();
    }

    if (@event.IsActionPressed("shoot"))
    {

      //SignalHub.CreateBullet(new Vector2(150, -50), new Vector2(1, 1), 50f, _bulletBase);

    }
  }
 

}
