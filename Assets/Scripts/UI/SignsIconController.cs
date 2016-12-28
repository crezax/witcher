using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SignsIconController : BaseBehaviour {
  private Image icon;

  protected override void OnAwake() {
    base.OnAwake();

    icon = GetComponent<Image>();
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    icon.sprite = PlayerController.Instance.SelectedSign.Icon;
  }
}
