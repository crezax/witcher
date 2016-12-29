using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : BaseBehaviour {
  [SerializeField]
  private Image health;
  private Health playerHealth;

  protected override void OnStart() {
    base.OnStart();

    playerHealth = Player.Instance.GetComponent<Health>();
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    health.fillAmount = playerHealth.CurrentValue / playerHealth.MaxValue;
  }
}
