using Godot;
using System;

namespace Com.IsartDigital.Utils.StateMachines {

	public abstract class AStateMachine : Area2D
	{
		protected Action<float> doAction;

		public override void _Ready()
		{
			SetModeVoid();
		}

        public override void _Process(float pDelta)
        {
			doAction(pDelta);
        }

        virtual protected void SetModeVoid ()
        {
			doAction = DoActionVoid;
        }

		virtual protected void DoActionVoid(float pDelta) { }

		virtual protected void SetModeNormal()
		{
			doAction = DoActionNormal;
		}

		abstract protected void DoActionNormal(float pDelta);

		virtual public void Start()
        {
			SetModeNormal();
        }

		protected override void Dispose(bool pDisposing)
		{
			base.Dispose(pDisposing);
			SetModeVoid();
		}
	}

}