using Godot;
using System;

public partial class Level : Node2D
{
	[Export]
	public int levelMinerals;

	private Player myPlayer;
	private HUD hud;


	public event Event levelFinished;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		myPlayer = GetNode<Player>("Player");
		hud = GetNode<HUD>("HUD");
		myPlayer.mineralDrilled += MineralMinado;
		this.UpdateMinerals();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void MineralMinado()
	{
		this.levelMinerals--;
		this.UpdateMinerals();
		if(levelMinerals == 0)
		{
			this.FinishLevel();
		}
	}

	public void UpdateMinerals()
	{
		hud.UpdateMinerals(levelMinerals);
	}

	public void FinishLevel()
	{
		this.RemoveChild(this.GetNode<CanvasModulate>("CanvasModulate"));
		//this.GetParent().RemoveChild(this);
		this.GetParent<LevelManager>().PlayNextLevel();
		this.QueueFree();
	}
}
