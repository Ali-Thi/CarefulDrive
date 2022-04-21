using Godot;
using System;

namespace Com.IsartDigital.Utils.StateMachines
{

	public abstract class AStateObject : AStateMachine
	{
		protected const string DEFAULT_STATE = "default";

		protected AnimatedSprite renderer;
		protected CollisionShape2D collider;

		public override void _Ready()
		{
			renderer = (AnimatedSprite)GetNode("Renderer");
			collider = (CollisionShape2D)GetNode("Collider");

		}

		protected override void SetModeNormal()
		{
			renderer.Animation = DEFAULT_STATE;
			base.SetModeNormal();
		}

		public abstract float Width { get;}
		public abstract float Height { get;}
	}

}