using Godot;
using System;

public partial class HUD : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NodePath nodePath = this.GetPath();
		GD.Print("Node path: ", nodePath);
	}

	public void UpdateBounces(int bounces){
		GD.Print("Update Bounces entered");
		GetNode<Label>("RadarBouncesLbl").Text = "Radar Bounces: " + bounces.ToString();
	}

	public void UpdateDrills(int drills)
	{
		GetNode<Label>("DrillsLeftLbl").Text = "Drills Left:: " + drills.ToString();
	}

	public void UpdateRadars(int radars)
	{
		GetNode<Label>("RadarsLeftLbl").Text = "Radars Left:: " + radars.ToString();
	}

	public void UpdateMinerals(int minerals)
	{
		GetNode<Label>("MineralsLeftLbl").Text = "Minerals Left:: " + minerals.ToString();
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
