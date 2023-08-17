using Godot;
using System;

public partial class radar_pod : RigidBody2D
{
	int collisionCount;
	public int maxCollisions;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		collisionCount = 0;
		maxCollisions = GetNode<Player>("/root/LevelTest/Player").GetBounces();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void OnRadarAreaEntered(Area2D area)
	{
		//this.EmitSignal("body_entered");
	}

	public void UpdateBounces(int bounces){
		maxCollisions = bounces;
	}

	public void OnCollisionEntered(Node body)
    {
		collisionCount++;
		GD.Print("Collision count: " + collisionCount);

		if (collisionCount > maxCollisions)
		{
				this.Sleeping = true;
		}
        
    }
}
