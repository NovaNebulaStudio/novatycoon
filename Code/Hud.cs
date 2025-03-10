using Sandbox;
using Sandbox.UI;

public class Hud : Panel {
  public Label MyLabel { get; set; }

  public Hud() {
    MyLabel = new Label();
    MyLabel.Parent = this;
  }

    public override void Tick(){
        MyLabel.Text = $"{Time.Now}";
    }
}