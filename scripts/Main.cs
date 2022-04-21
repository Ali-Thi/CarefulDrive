using Godot;
using System;
using Com.IsartDigital.Utils.Effects;

namespace Com.IsartDigital.OneButtonGame {
	
	public class Main : Node
	{
		public static main Instance { get; private set; }

		private const string PRESSED = "pressed";
		private const string UNPRESSED = "button_up";
		private const int NB_ROUND = 5;
		private const int COUNTER_SCALE = 25;

		private bool isButtonOnePressed = false;
		private bool isButtonTwoPressed = false;
		private bool inGame = true;

		private Button btnPlayerOne;
		private Button btnPlayerTwo;
		private Player playerOne;
		private Player playerTwo;
		//[Export] private PackedScene endScreen;
		//private  endScreenInstance;

		private int counterOne = 0;
		private int counterTwo = 0;

		private Tween tween1;
		private Tween tween2;
		private Tween tween3;
		private Tween tween4;
		private Tween tween5;
		private Tween tween6;


		[Export] private NodePath playerOnePath = default;
		[Export] private NodePath playerTwoPath = default;

		[Export] private NodePath playerOneButtonPath = default;
		[Export] private NodePath playerTwoButtonPath = default;

		[Export] private NodePath playerOneSpeedLabelPath = default;
		[Export] private NodePath playerTwoSpeedLabelPath = default;

		[Export] private NodePath trailOnePath;
		[Export] private NodePath trailTwoPath;

		private Label playerOneSpeedLabel;
		private Label playerTwoSpeedLabel;

		private Trail trailOne;
		private Trail trailTwo;

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
			(btnPlayerTwo).Connect(PRESSED, this, nameof(OnPlayerTwoInput));
			//endScreenInstance = ()endScreen;

			playerOneSpeedLabel = (Label)GetNode(playerOneSpeedLabelPath);
			playerTwoSpeedLabel = (Label)GetNode(playerTwoSpeedLabelPath);

			trailOne = (Trail)GetNode(trailOnePath);
			trailTwo = (Trail)GetNode(trailTwoPath);

			tween1 = (Tween)GetNode("Tween1");
			tween2 = (Tween)GetNode("Tween2");
			tween3 = (Tween)GetNode("Tween3");
			tween4 = (Tween)GetNode("Tween4");
			tween5 = (Tween)GetNode("Tween5");
			tween6 = (Tween)GetNode("Tween6");
			//tween.Start();

			trailOne.speed = playerOne.speed;
			trailTwo.speed = playerTwo.speed;
		}

		public override void _Process(float delta)
		{
			if (isButtonOnePressed == false)
			{
				playerOne.SpeedDown();
				//playerOne.Scale = playerOne.startScale;
			}
			if (isButtonTwoPressed == false)
			{
				playerTwo.SpeedDown();
				//playerTwo.Scale = playerTwo.startScale;
			}

			if (playerOne.scaleMore)
			{
				++counterOne;
				if (counterOne == COUNTER_SCALE)
				{
					playerOne.scaleMore = false;
					counterOne = 0;
				}
			}
			if (playerTwo.scaleMore)
			{
				++counterTwo;
				if (counterTwo == COUNTER_SCALE)
				{
					playerTwo.scaleMore = false;
					counterTwo = 0;
				}
			}

			if (playerOne.round == NB_ROUND || playerTwo.round == NB_ROUND || playerOne.lost || playerTwo.lost)
			{
				inGame = false;

			}

			playerOneSpeedLabel.Text = playerOne.speed.ToString("F0");
			playerTwoSpeedLabel.Text = playerTwo.speed.ToString("F0");

			//tween.InterpolateProperty(playerOneSpeedLabel, "modulate", playerOneSpeedLabel.Modulate, new Color(1, 0, 0, 0.6f),1.2f,delay:tween.GetRuntime());
			//tween.InterpolateCallback(this, tween.GetRuntime(), nameof());

			if (playerOne.speed <= Player.PRINTED_SPEED_SLOW)
			{
				tween1.Start();
				tween1.InterpolateProperty(playerOneSpeedLabel, "modulate", playerOneSpeedLabel.Modulate, new Color(0, 1, 0, 0.6f), 1.2f);

				if (!playerOne.trueColor)
				{
					playerOne.Modulate = playerOne.colo;
				}
			}
			else if (playerOne.speed <= Player.PRINTED_SPEED_MIDDLE)
			{
				tween2.Start();
				tween2.InterpolateProperty(playerOneSpeedLabel, "modulate", playerOneSpeedLabel.Modulate, new Color(0.5f, 0.5f, 0, 0.6f), 1.2f);
			}
			else
			{
				tween3.Start();
				tween3.InterpolateProperty(playerOneSpeedLabel, "modulate", playerOneSpeedLabel.Modulate, new Color(1, 0, 0, 0.6f), 1.2f);
				Blink(playerOne);
			}

			if (playerTwo.speed <= Player.PRINTED_SPEED_SLOW)
			{
				tween4.Start();
				tween4.InterpolateProperty(playerTwoSpeedLabel, "modulate", playerTwoSpeedLabel.Modulate, new Color(0, 1, 0, 0.6f), 1.2f);

				if (!playerTwo.trueColor)
				{
					playerTwo.Modulate = playerTwo.colo;
				}
			}
			else if (playerTwo.speed <= Player.PRINTED_SPEED_MIDDLE)
			{
				tween5.Start();
				tween5.InterpolateProperty(playerTwoSpeedLabel, "modulate", playerTwoSpeedLabel.Modulate, new Color(0.5f, 0.5f, 0, 0.6f), 1.2f);
			}
			else
			{
				tween6.Start();
				tween6.InterpolateProperty(playerTwoSpeedLabel, "modulate", playerTwoSpeedLabel.Modulate, new Color(1, 0, 0, 0.6f), 1.2f);
				Blink(playerTwo);
			}

		}

		private void OnPlayerOneInput()
		{
			if (inGame)
			{
				isButtonOnePressed = true;
				playerOne.SpeedUp();
				//tween.Start();

				//if (playerOne.speed <= Player.PRINTED_SPEED_SLOW)
				//{
				//	tween1.Start();
				//	tween1.InterpolateProperty(playerOneSpeedLabel, "modulate", playerOneSpeedLabel.Modulate, new Color(0, 1, 0, 0.6f), 1.2f);
				//}
				//else if (playerOne.speed <= Player.PRINTED_SPEED_MIDDLE)
				//{
				//	tween2.Start();
				//	tween2.InterpolateProperty(playerOneSpeedLabel, "modulate", playerOneSpeedLabel.Modulate, new Color(0.5f, 0.5f, 0, 0.6f), 1.2f);
				//}
				//else
				//{
				//	tween3.Start();
				//	tween3.InterpolateProperty(playerOneSpeedLabel, "modulate", playerOneSpeedLabel.Modulate, new Color(1, 0, 0, 0.6f), 1.2f);
				//}
			}
				isButtonOnePressed = false;
		}
		private void OnPlayerTwoInput()
		{
			if (inGame)
			{
				isButtonTwoPressed = true;
				playerTwo.SpeedUp();
				//tween.Start();

				//if (playerTwo.speed <= Player.PRINTED_SPEED_SLOW)
				//{
				//	tween4.Start();
				//	tween4.InterpolateProperty(playerTwoSpeedLabel, "modulate", playerTwoSpeedLabel.Modulate, new Color(0, 1, 0, 0.6f), 1.2f);
				//}
				//else if (playerTwo.speed <= Player.PRINTED_SPEED_MIDDLE)
				//{
				//	tween5.Start();
				//	tween5.InterpolateProperty(playerTwoSpeedLabel, "modulate", playerTwoSpeedLabel.Modulate, new Color(0.5f, 0.5f, 0, 0.6f), 1.2f);
				//}
				//else
				//{
				//	tween6.Start();
				//	tween6.InterpolateProperty(playerTwoSpeedLabel, "modulate", playerTwoSpeedLabel.Modulate, new Color(1, 0, 0, 0.6f), 1.2f);
				//}
			}

				isButtonTwoPressed = false;
		}
		public override void _UnhandledKeyInput(InputEventKey pEvent)
		{
			if (Input.IsKeyPressed((int)KeyList.Q))
			{
				OnPlayerOneInput();
			}

			if (Input.IsKeyPressed((int)KeyList.M))
			{
				OnPlayerTwoInput();
			}
		}

		private void Blink(Player pPlayer)
		{
			if (pPlayer.trueColor)
			{
				pPlayer.Modulate = new Color(1, 0, 0, 1);
				pPlayer.trueColor = false;
			}
			else
			{
				pPlayer.Modulate = pPlayer.colo;
				pPlayer.trueColor = true;
			}
		}
		protected override void Dispose(bool pDisposing)
		{
			if (pDisposing && Instance == this) 
				Instance = null;

			base.Dispose(pDisposing);
		}
	}
}
