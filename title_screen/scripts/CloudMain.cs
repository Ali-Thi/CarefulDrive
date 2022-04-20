using Godot;
using System;

public class cloud2 : Sprite
{
	private float coordYOutOfScreen;
	private float coordYMiddleOfScreen;
	private float coordYAimed;
	
	public override void _Ready()
	{
		coordYOutOfScreen = -500F;
		coordYMiddleOfScreen = Position.y+1;
		coordYAimed = coordYOutOfScreen;
	}
	
	
 	public override void _Process(float delta)
 	{
		
	  	if (Position.y != coordYAimed)
		{
			Position = new Vector2(Position.x, Position.y+coordYAimed * delta);
		}
		else if (coordYAimed == coordYMiddleOfScreen)
		{
			GetTree().ChangeScene("res://scenes/endScreen.tscn");
		}
		else
		{
			Hide();
		}
	}
	
	private void OnTimerTimeout()
	{
		Show();
		coordYAimed = coordYMiddleOfScreen;
	}
}
