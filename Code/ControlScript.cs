using Sandbox;
using Sandbox.UI;

public sealed class ControlScript : Component {
	public float cameraZoom = 100f;
	public bool rightShoulderCamera = true;
	public PlayerController playerController;
	public CameraComponent playerCamera;
	public float interactionRange = 250f;

	protected override void OnStart() {
		playerController = GetComponent<PlayerController>();
		playerCamera = playerController.GetComponentInChildren<CameraComponent>();

		playerController.StepDebug = true;
	}

	protected override void OnUpdate() {
		Vector2 centerScreenVec = playerCamera.ScreenRect.Center;
		Ray screenRay = playerCamera.ScreenPixelToRay(centerScreenVec);
		SceneTraceResult traceResult = Scene.Trace.Ray(screenRay.Position, screenRay.Position + screenRay.Forward * interactionRange).Run();

		DebugOverlay.ScreenText(centerScreenVec, "•");

		if (traceResult.Hit) {
			//DebugOverlay.Box(traceResult.HitPosition, new Vector3(1, 1, 1), Color.Green);

			var pressable = traceResult.GameObject.GetComponent<IPressable>();
			if (pressable != null) {
				playerController.Hovered = traceResult.Component;
				playerController.Pressed = playerController.Hovered;

				if (Input.Pressed("use")){
					var pressEvent = new IPressable.Event(playerController, screenRay);
					pressable.Press(pressEvent);
				}
			}

		} else {
			//DebugOverlay.Box(screenRay.Position + screenRay.Forward * interactionRange, new Vector3(1, 1, 1), Color.Red);
		}

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
