using UnityEngine;
using UnityEngine.UI;

public class ResourceBarController : BaseBehaviour {
  [SerializeField]
  private Image resourceBar;

  public Resource resource;

  protected override void OnUpdate() {
    base.OnUpdate();

    resourceBar.fillAmount = resource.CurrentValue / resource.MaxValue;
  }
}
