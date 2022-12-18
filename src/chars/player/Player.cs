using Godot;
using System;

public class Player : Area2D
{
	[Export]
	private int Speed = 0;
	private Vector2 _screenSize;

	[Signal]
	public delegate void Hit();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_screenSize = GetViewport().Size;
		Hide();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var velocity = new Vector2();
		var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");

		if (Input.IsActionPressed("ui_down")) {
			animatedSprite.Animation = "UpDown";
			velocity.y += 1;
		}

		if (Input.IsActionPressed("ui_up")) {
			animatedSprite.Animation = "UpDown";
			velocity.y -= 1;
		}

		if (Input.IsActionPressed("ui_right")) {
			animatedSprite.Animation = "Right";
			velocity.x += 1;
		}

		if (Input.IsActionPressed("ui_left")) {
			animatedSprite.Animation = "Left";
			velocity.x -= 1;
		}

		

		if (velocity.Length() > 0) {
			velocity = velocity.Normalized() * Speed;
			animatedSprite.Play();
		} else {
			animatedSprite.Play("Front");
		}

		Position += velocity * delta;
		Position = new Vector2(
			Mathf.Clamp(Position.x, 0, _screenSize.x),
			Mathf.Clamp(Position.y, 0, _screenSize.y)
		);
		
	}

	public void Start(Vector2 pos)
	{
		Position = pos;
		Show();

		var collisionShape2D = (CollisionShape2D) GetNode("CollisionShape2D");
		collisionShape2D.Disabled = false;
	}


	private void OnPlayerBodyEntered(Godot.Object body)
	{
		Hide();
		EmitSignal("Hit");
		var collisionShape2D = (CollisionShape2D) GetNode("CollisionShape2D");
		collisionShape2D.Disabled = true;

	}

}
