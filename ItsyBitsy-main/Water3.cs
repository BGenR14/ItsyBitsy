using Godot;
using System;

public class Water1 : RigidBody2D{
	
	[Export]
	public int MinSpeed = 150;
	
	[Export]
	public int MaxSpeed = 250;

	public override void _Ready(){
		var animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		//var mobTypes = animSprite.Frames.GetAnimationNames();
		//animSprite.Animation = mobTypes(0, mobTypes.Length);
	}
	
	public void OnVisibilityNotifier2DScreenExited(){
	QueueFree();
}

}
