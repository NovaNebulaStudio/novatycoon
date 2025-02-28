using Sandbox;
using static Sandbox.Component;

public sealed class MachineButton : Component, IPressable {
	public bool Press(IPressable.Event e) {
		var playerController = e.Source as PlayerController;
		if (playerController == null) return false;

		Log.Info("Pressed! " + playerController.Pressed);
		var soundPoint = GetComponent<SoundPointComponent>();
		soundPoint.StartSound();

		return true;
	}
}
