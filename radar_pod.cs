using Godot;
using System;

public partial class radar_pod : RigidBody2D
{
	int collisionCount;
	public int maxCollisions;
	private AudioStreamPlayer2D myPlayer;
	public event Event radarInactive;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		collisionCount = 0;
		maxCollisions = GetParent<Player>().GetBounces();
		this.myPlayer = this.GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
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
		myPlayer.Stream = ResourceLoader.Load<AudioStream>("Sounds/RadarBoop.wav");
		myPlayer.Play();

		collisionCount++;
		GD.Print("Collision count: " + collisionCount);


		if (collisionCount > maxCollisions)
		{
			this.Sleeping = true;
		}
        
    }
}
