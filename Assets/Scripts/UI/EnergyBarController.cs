using UnityEngine;
using UnityEngine.UI;

public class EnergyBarController : BaseBehaviour {
  [SerializeField]
  private Image energy;
  private Energy playerEnergy;

  protected override void OnStart() {
    base.OnStart();

    playerEnergy = Player.Instance.GetComponent<Energy>();
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    energy.fillAmount = playerEnergy.CurrentEnergy / playerEnergy.MaxEnergy;
  }
}
