using Godot;
using System;

public class BackButton : Button
{
	private void OnBackButtonPressed()
	{
		GetTree().ChangeScene("res://scenes/endScreen.tscn");
	}
}


	
