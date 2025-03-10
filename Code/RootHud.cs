using Sandbox;

public sealed class RootHud : PanelComponent {
	Hud hud;
 
	protected override void OnTreeFirstBuilt() {
		base.OnTreeFirstBuilt();

		hud = new Hud();
		hud.Parent = Panel;
	}
}