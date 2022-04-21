using Godot;
using System;

public class CloudsEndScene : Sprite
{
	private bool animated = true;
	private const int coordYMiddleOfScreen = 360;
	private const int coordYOutOfScreenByBottom = 1200;
	private int coordYAimed = coordYOutOfScreenByBottom;
	private float translationSpeed = 500F;
	
	public override void _Process(float delta)
	{
		/*ici tu peux rajouter dans la condition que si l'animation veut se lancer alors qu'aucun joueur 
		n'a gagné alors on la lance pas, ça permettre de pas avoir d'animation qui se lance quand tu 
		reviens de la page des crédits*/
		if (animated /*ici*/)
		{
			Position = new Vector2(Position.x, Position.y+translationSpeed * delta);
			
			if (Position.y <= coordYAimed && coordYAimed == coordYMiddleOfScreen)
			{
				GetTree().ChangeScene("res://scenes/main.tscn");
			}
			else if (Position.y >= coordYAimed && coordYAimed == coordYOutOfScreenByBottom)
			{
				animated = false;
			}
		}
	}
	
	private void OnReplayButtonPressed()
	{
		coordYAimed = coordYMiddleOfScreen;
		translationSpeed = -translationSpeed;
		animated = true;
	}
}
