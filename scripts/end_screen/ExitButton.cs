using Godot;
using System;

public class ExitButton : Button
{
	private bool show = false;

	public override void _Ready()
	{
		Modulate = new Color(1, 1, 1, 0);
	}

	public override void _Process(float delta)
	{
		if (show && Modulate.a < 1)
		{
			Modulate = new Color(1, 1, 1, Modulate.a+1F*delta);
		}
	}

	private void OnButtonsTimerTimeout()
	{
		show = true;
	}
	
	private void OnExitButtonPressed()
	{
		GetTree().Quit();
	}
}
