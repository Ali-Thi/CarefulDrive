using Godot;
using System;

namespace Com.IsartDigital.OneButtonGame {
	
	public class main : Node
	{
		public static main Instance { get; private set; }

		private const string PRESSED = "pressed";
		private const string UNPRESSED = "button_up";

		private bool isButtonOnePressed = false;
		private bool isButtonTwoPressed = false;

		private Button btnPlayerOne;
		private Button btnPlayerTwo;
		private Player playerOne;
		private Player playerTwo;


		[Export] private NodePath playerOnePath = default;
		[Export] private NodePath playerTwoPath = default;

		[Export] private NodePath playerOneButtonPath = default;
		[Export] private NodePath playerTwoButtonPath = default;

		private main ():base() {}

		public override void _Ready()
		{
			if (Instance != null){  
				Free();
				GD.Print($"{nameof(main)} Instance already exist, destroying the last added.");
				return;
			}
			
			Instance = this;

			btnPlayerOne = (Button)GetNode(playerOneButtonPath);
			btnPlayerTwo = (Button)GetNode(playerTwoButtonPath);
			playerOne = (Player)GetNode(playerOnePath);
			playerTwo = (Player)GetNode(playerTwoPath);

			(btnPlayerOne).Connect(PRESSED, this, nameof(OnPlayerOneInput));
			//(btnPlayerOne).Connect(UNPRESSED, this, nameof(OutPlayerOneInput));
			(btnPlayerTwo).Connect(PRESSED, this, nameof(OnPlayerTwoInput));
			//(btnPlayerTwo).Connect(UNPRESSED, this, nameof(OutPlayerTwoInput));
		}

		public override void _Process(float delta)
		{
			//GD.Print($"{playerOne.speeding}" + " deu " + $"{isButtonOnePressed}");
			//if (isButtonOnePressed)
			//{
			//	playerOne.SpeedUp();
			//}
			//else
			//{
			//	playerOne.SpeedDown();
			//}

			//if (isButtonTwoPressed)
			//{
			//	playerTwo.SpeedUp();
			//}
			//else
			//{
			//	playerTwo.SpeedDown();
			//}

			//GD.Print("playerOne.speeding " + playerOne.speeding);
			//GD.Print("isButtonOnePressed " + isButtonOnePressed);
			//GD.Print("playerTwo.speeding " + playerTwo.speeding);
			//GD.Print("isButtonTwoPressed " + isButtonTwoPressed);
			if (isButtonOnePressed == false)
			{
				playerOne.SpeedDown();
			}
			if (isButtonTwoPressed == false)
			{
				playerTwo.SpeedDown();
			}
			//playerOne.SpeedDown();
			//playerTwo.SpeedDown();
		}

		private void OnPlayerOneInput()
		{

			isButtonOnePressed = true;
			playerOne.SpeedUp();
			isButtonOnePressed = false;

		}
		private void OnPlayerTwoInput()
		{
			isButtonTwoPressed = true;
			playerTwo.SpeedUp();
			isButtonTwoPressed = false;
		}

		//private void OutPlayerOneInput()
		//{
		//	isButtonOnePressed = false;
		//}
		//private void OutPlayerTwoInput()
		//{
		//	isButtonTwoPressed = false;
		//}

		protected override void Dispose(bool pDisposing)
		{
			if (pDisposing && Instance == this) 
				Instance = null;

			base.Dispose(pDisposing);
		}
	}
}
