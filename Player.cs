using Godot;
using System;

public delegate void Event();

public partial class Player : Node2D
{
	public event Event mineralDrilled;

	private const int SHOOTING_SPEED = 325;
	private float cannonCorrection = MathF.PI/2;

	private PackedScene drillScene;
	private PackedScene radarScene;
	private Marker2D cannonPivot;
	private HUD hud;
	private AudioStreamPlayer2D myPlayer;

	private int maxCollisions = 3;
	private int currentDrills;
	private int currentRadars;
	private bool shotActive = false;

	[Export]
	private int startingDrills;
	[Export]
	private int startingRadars;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.cannonPivot = this.GetChild<Marker2D>(0);
		this.drillScene = ResourceLoader.Load<PackedScene>("res://drill_pod.tscn");
		this.radarScene = ResourceLoader.Load<PackedScene>("res://radar_pod.tscn");
		this.hud = GetParent().GetNode<HUD>("HUD");
		this.myPlayer = this.GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		
		this.currentDrills = startingDrills;
		this.currentRadars = startingRadars;
		
		this.hud.UpdateDrills(currentDrills);
		this.hud.UpdateRadars(currentRadars);
		this.hud.UpdateBounces(maxCollisions);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 cannonDirection = (this.GetGlobalMousePosition() - this.GlobalPosition).Normalized();
		cannonPivot.Rotation = cannonDirection.Angle() + cannonCorrection;
		
		//Quizas los taladros no deberian ser hijos de la base, checkear;

		//CAMBIAR LOS IFS POR UN SWITCH
		if(Input.IsActionJustPressed("shoot_drill") && currentDrills>0 && !shotActive)
		{
			shotActive = true;

			RigidBody2D newDrill = drillScene.Instantiate<RigidBody2D>();
			newDrill.Rotation = cannonPivot.Rotation;
			newDrill.LinearVelocity = new Vector2(cannonDirection.X, cannonDirection.Y) * SHOOTING_SPEED;
			AddChild(newDrill);
			GetNode<drill_pod>("DrillPod").mineralDrilled += MineralMined;
			GetNode<drill_pod>("DrillPod").drillInactive += ShotFinished;

			currentDrills --;
			hud.UpdateDrills(currentDrills);

			//Sound
			myPlayer.Stream = ResourceLoader.Load<AudioStream>("Sounds/DrillShot.wav");
			myPlayer.Play();
		}
		if(Input.IsActionJustPressed("shoot_radar") && currentRadars > 0)
		{
			RigidBody2D newRadar = radarScene.Instantiate<RigidBody2D>();
			newRadar.Rotation = cannonPivot.Rotation;
			newRadar.LinearVelocity = new Vector2(cannonDirection.X, cannonDirection.Y) * SHOOTING_SPEED;
			AddChild(newRadar);
			GetNode<radar_pod>("RadarPod").radarInactive += ShotFinished;

			currentRadars --;
			hud.UpdateRadars(currentRadars);

			//Sound
			myPlayer.Stream = ResourceLoader.Load<AudioStream>("Sounds/RadarShot.wav");
			myPlayer.Play();
		}

		if(Input.IsActionJustPressed("up_bounce_sonar") && !shotActive){
			shotActive = true;

			GD.Print("Un Bounce mas");
			if(maxCollisions != 3){
				maxCollisions++;
				GD.Print(maxCollisions);
				hud.UpdateBounces(maxCollisions);
			}
		}
		if(Input.IsActionJustPressed("down_bounce_sonar") && !shotActive){
			shotActive = true;

			if(maxCollisions != 0){
				maxCollisions--;
				GD.Print(maxCollisions);
				hud.UpdateBounces(maxCollisions);
			}
		}
	}

	public void ShotFinished()
	{
		shotActive = false;
	}

	public void MineralMined()
	{
		this.shotActive = false;
		this.mineralDrilled.Invoke();
	}

	public void OnRadarCollided()
	{
		GD.Print("Aun funca");
	}

	public int GetBounces(){
		GD.Print("Max Collisions:" + maxCollisions.ToString());
		return maxCollisions;
	}
}
