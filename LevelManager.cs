using Godot;
using System;

public partial class LevelManager : Node
{
	private AudioStreamPlayer2D myPlayer;
	private int currentLevel = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.myPlayer = this.GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		myPlayer.Stream = ResourceLoader.Load<AudioStream>("Sounds/BGMusic.mp3");
		myPlayer.Play();

		// PackedScene level = ResourceLoader.Load<PackedScene>("res://level_test.tscn");
		// AddChild(level.Instantiate());
		

		// Level levelNode = GetNode<Level>("LevelTest");
		// levelNode.levelFinished += PlayNextLevel;
		this.PlayNextLevel();
	}

	public void MineralMined()
	{
		GD.Print("Mineral minado");
	}

	public void PlayNextLevel()
	{
		if(currentLevel != 1)
		{
			this.RemoveChild(this.GetNode("Level" + (currentLevel -1).ToString()));
		}


		PackedScene level = ResourceLoader.Load<PackedScene>("res://level_" + currentLevel.ToString() + ".tscn");
		AddChild(level.Instantiate());

		currentLevel++;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
