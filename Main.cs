using Godot;
using System;

public class Main : Node2D
{
	[Export]
	public PackedScene Mob;
	
	public int Score = 0;
	
	private Random rand = new Random();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	private float RandRand(float min, float max) 
	{
		return (float) (rand.NextDouble() * (max - min) + min);
	}

	public void GameOver()
	{
		//timers
		var mobTimer = (Timer) GetNode("MobTimer");
		var scoreTimer = (Timer) GetNode("ScoreTimer");

		scoreTimer.Stop();
		mobTimer.Stop();
		
		var hud = (HUD) GetNode("HUD");
		hud.ShowGameOver();
	}
	
	public void NewGame()
	{
		Score = 0;

		var player = (Player) GetNode("Squid");
		var startTimer = (Timer) GetNode("StartTimer");
		var startPosition = (Position2D) GetNode("StartPosition");

		player.Start(startPosition.Position);
		startTimer.Start();
		
		var hud = (HUD) GetNode("HUD");
		hud.UpdateScore(Score);
		hud.ShowMessage("Get Ready!");
		
	}
	
	public void OnStartTimerTimeout()
	{
		//timers
		var mobTimer = (Timer) GetNode("MobTimer");
		var scoreTimer = (Timer) GetNode("ScoreTimer");

		mobTimer.Start();
		scoreTimer.Start();
	}

	public void OnScoreTimerTimeout()
	{
		Score += 1;
		var hud = (HUD) GetNode("HUD");
		hud.UpdateScore(Score);
	}
	
	public void OnMobTimerTimeout()
	{
		// Choose a random location on Path2D.
		var mobSpawnLocation = (PathFollow2D) GetNode("MobPath/MobSpawnLocation");
		mobSpawnLocation.Offset = rand.Next();

		// Create a Mob instance and add it to the scene.
		var mobInstance = (RigidBody2D) Mob.Instance();
		AddChild(mobInstance);

		// Set the mob's direction perpendicular to the path direction.
		var direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the mob's position to a random location.
		mobInstance.Position = mobSpawnLocation.Position;

		// Add some randomness to the direction.
		direction += RandRand(-Mathf.Pi / 4, Mathf.Pi / 4);
		mobInstance.Rotation = direction;

		// Choose the velocity.
		mobInstance.LinearVelocity = new Vector2(RandRand(150f, 250f), 0).Rotated(direction);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
