using Sandbox;
using Sandbox.UI;

public sealed class ControlScript : Component {
	public float cameraZoom = 100f;
	public bool rightShoulderCamera = true;
	public PlayerController playerController;

	protected override void OnStart() {
		playerController = GetComponent<PlayerController>();
		Log.Info("Load Control Script");
	}

	protected override void OnUpdate() {
		if (Input.MouseWheel.y != 0) {
			cameraZoom = MathX.Floor(MathX.Clamp(cameraZoom + (-Input.MouseWheel.y * 50), 0, 500));

			if (cameraZoom <= 0) {
				playerController.ThirdPerson = false;
			} else {
				playerController.ThirdPerson = true;
			}
		}
		
		if (Input.Pressed("switchcamshoulder")) {
			rightShoulderCamera = !rightShoulderCamera;
		}

		playerController.CameraOffset = new Vector3(cameraZoom, (rightShoulderCamera? 25: -25) , 0);
	}
}
