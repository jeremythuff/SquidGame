using Godot;
using System;

public class Bomb : RigidBody2D
{
	// Declare member variables here. Examples:
	[Export]
	public int MinSpeed = 150;

	[Export]
	public int MaxSpeed = 250;

	private String[] _mobTypes = {"blue", "grey", "red"};


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		var randomMob = new Random();
		animatedSprite.Animation = _mobTypes[randomMob.Next(0, _mobTypes.Length)];
		animatedSprite.Play();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void onVisibilityScreenExited()
	{
		QueueFree();
	}

}
