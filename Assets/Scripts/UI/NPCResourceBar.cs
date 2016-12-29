using UnityEngine;

public class NPCResourceBar : ResourceBarController {

  public float Offset { get; set; }

  protected override void OnUpdate() {
    base.OnUpdate();

    transform.position = Camera.main.WorldToScreenPoint(
      resource.transform.position + Vector3.up * Offset
    );
  }
}
