using Com.IsartDigital.Utils.Signals;
using Godot;
using System;
using static Godot.Tween;

namespace Com.IsartDigital.Utils.Effects {

	public class Shake : Node
	{
		private const TransitionType TRANS = TransitionType.Sine;
		private const EaseType EASE = EaseType.InOut;

		[Export]
		private NodePath targetPath="..";
		private Node target;

		[Export]
		private string property="";

		private Timer frequency;
		private Timer duration;
		private Vector2 amplitude;
		private Tween shakeTween;
		private Vector2 startValue;
		private float friction;
		
		public override void _Ready()
		{
			target = GetNode(targetPath);
			if (property=="") property= target is Camera2D ? "offset" : "position";
			frequency = (Timer)GetNode("Frequency");
			duration = (Timer)GetNode("Duration");
			shakeTween = (Tween)GetNode("ShakeTween");
			frequency.Connect(TimerSignal.TIME_OUT, this, nameof(StartShaking));
			duration.Connect(TimerSignal.TIME_OUT, this, nameof(Stop));
		}

		public void Start(float pDuration = 1f, int pFrequency = 30, float pAmplitudeX = 15f, float pAmplitudeY = 15f, float pFriction=0.9f)
        {
			amplitude = new Vector2(pAmplitudeX,pAmplitudeY);
			duration.WaitTime = pDuration;
			frequency.WaitTime = 1f / pFrequency;
			friction = pFriction;
			duration.Start();
			frequency.Start();
			startValue = (Vector2)target.Get(property);
			StartShaking();
        }

		private void StartShaking () {
			Vector2 lRand = new Vector2(-amplitude.x + GD.Randf() * amplitude.x * 2, -amplitude.y + GD.Randf() * amplitude.y * 2);
			amplitude *= friction;
			shakeTween.InterpolateProperty(target, property, target.Get(property), lRand, frequency.WaitTime, TRANS, EASE);
			shakeTween.Start();
		}

		public void Stop()
        {	
			shakeTween.StopAll();
			frequency.Stop();
			duration.Stop();
			target.Set(property,startValue);
		}

	}

}