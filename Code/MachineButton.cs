using Sandbox;
using static Sandbox.Component;

public sealed class MachineButton : Component, IPressable {
	
	public bool Press(IPressable.Event e) {
		Log.Info("Pressed!");

		return true;
	}
}
