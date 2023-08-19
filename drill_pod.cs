using Godot;
using System;

public partial class drill_pod : RigidBody2D
{
	public event Event mineralDrilled;
	public event Event drillInactive;

	private Timer myTimer;
	private PointLight2D myLight;
	private AudioStreamPlayer2D myPlayer;
	private LevelManager myLvlMng;


	private bool mining = false;
	private bool active = true;
	private float angleCorrection = MathF.PI/2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.myTimer = this.GetNode<Timer>("Timer");
		this.myLight = this.GetNode<PointLight2D>("PointLight2D");
		this.myPlayer = this.GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("drill_direction_change") && this.active)
		{
			Vector2 newDirection = (GetGlobalMousePosition() - this.GlobalPosition).Normalized();
			this.LinearVelocity = newDirection * this.LinearVelocity.Length();
			this.Rotation = newDirection.Angle() + angleCorrection;
			
		}
	}

	public void OnBodyEntered(Node2D body)
	{
		this.Sleeping = true;
		this.active = false;
		this.CollisionLayer = 0;
		this.CollisionMask = 2;


		if(body.GetGroups().Contains("Minable"))
		{
			myPlayer.Stream = ResourceLoader.Load<AudioStream>("Sounds/Drilling.wav");
			myPlayer.Play();


			this.GetNode<CpuParticles2D>("Mining").Emitting = true;
			this.mining = true;
			myLight.Enabled = true;

			this.myTimer.Timeout += body.QueueFree;
			this.myTimer.Timeout += this.StartExplosion;
			this.myTimer.Start(2.4f);
		}
		else
		{
			this.StartExplosion();
		}
	}

	private void StartExplosion()
	{
		myPlayer.Stop();
		myPlayer.Stream = ResourceLoader.Load<AudioStream>("Sounds/DrillExplosion.wav");
		myPlayer.Play();


		this.GetNode<CpuParticles2D>("Explosion").Emitting = true;
		this.myLight.Enabled = true;
		this.myLight.TextureScale = 0.8f;
		if(mining)
		{
			this.myTimer.Timeout -= StartExplosion;
			mineralDrilled.Invoke();
			mining = false;
		}
		this.myTimer.Timeout += this.FinalExplosion;
		this.myTimer.Start(1);

	}

	private void FinalExplosion()
	{
		this.myLight.Enabled = false;
		this.GetNode<Sprite2D>("Sprite2D").Visible = false;
		this.GetNode<CpuParticles2D>("Explosion").Emitting = false;
		this.GetNode<CpuParticles2D>("Mining").Emitting = false;
		this.drillInactive.Invoke();
		this.GetParent().RemoveChild(this);
	}
}
