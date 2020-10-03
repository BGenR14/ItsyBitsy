using Godot;
using System;

public class Main : Node{
	
	[Export]
	public PackedScene Mob;

	private int _score;
	
	private Random _random = new Random();

	public override void _Ready(){    
		NewGame();
	}
	
	private float RandRange(float min, float max){
		return (float)_random.NextDouble() * (max - min) + min;
	}

private void GameOver(){
	GetNode<Timer>("WaterSpawnTime").Stop();
	GetNode<Timer>("ScoreTimer").Stop();
}

private void NewGame(){
	_score = 0;
	
	var spider = GetNode<Spider>("Spider");
	var startPosition = GetNode<Position2D>("StartPosition");
	spider.Start(startPosition.Position);

	GetNode<Timer>("StartTimer").Start();
}

private void OnStartTimerTimeout(){
	GetNode<Timer>("ScoreTimer").Start();
	GetNode<Timer>("WaterSpawnTime").Start();
}

private void OnScoreTimerTimeout(){
	_score++;
}

private void OnWaterSpawnTimeTimeout(){
		 // Choose a random location on Path2D.
	var waterSpawnLocation = GetNode<PathFollow2D>("WaterPath/WaterSpawnLocation");
	waterSpawnLocation.Offset = _random.Next();

	// Create a Mob instance and add it to the scene.
	var waterInstance = (RigidBody2D)Mob.Instance();
	AddChild(waterInstance);

	// Set the mob's direction perpendicular to the path direction.
	float direction = waterSpawnLocation.Rotation + Mathf.Pi / 2;

	// Set the mob's position to a random location.
	waterInstance.Position = waterSpawnLocation.Position;

	// Add some randomness to the direction.
	direction += RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
	waterInstance.Rotation = direction;

	// Choose the velocity.
	waterInstance.LinearVelocity = new Vector2(RandRange(150f, 250f), 0).Rotated(direction);
}


} //end Main class
