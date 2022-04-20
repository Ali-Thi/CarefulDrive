using Godot;
using System;

public class ButtonsContainer : HBoxContainer
{
	private void OnReplayButtonPressed()
	{
		foreach (Control button in GetChildren())
		{
			button.Hide();
		}
	}
}
