using Sandbox;
using Sandbox.Utility;

public sealed class FactoryMachine : Component {
	[Property]
	public GameObject productPrefab;
	[Property]
	public GameObject spawnerPart;
	[Property]
	public GameObject collectorPart;
	[Property]
	public GameObject cashLabel;
	public float lastTime = 0f;
	public double totalCash = 0;

	public void addCash(double v) {
		totalCash += v;

		var cashTextRenderer = cashLabel.GetComponent<TextRenderer>();
		cashTextRenderer.Text = $"${ totalCash }";
	}

	protected override void OnStart() {
		addCash(0);
		lastTime = Time.Now;

		var collectorCollider = collectorPart.GetComponent<BoxCollider>();
		collectorCollider.OnObjectTriggerEnter = (GameObject hitObject) => {
			if (!hitObject.Tags.Has("product")) return;

			Invoke(1f, ()=> {
				if (!hitObject.IsValid()) return;
				hitObject.Destroy();
			});

			addCash(100);
		};
	}

	protected override void OnUpdate() {
		var timeLapse = Time.Now - lastTime;
		if (timeLapse < 1) return;

		lastTime = Time.Now;

		// Spawn productPrefab: soda;

		GameObject newProduct = productPrefab.Clone(spawnerPart.WorldPosition);
		newProduct.Tags.Add("product");
	}
}
