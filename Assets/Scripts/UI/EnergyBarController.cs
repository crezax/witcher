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

    // * 0.65f because of how radial filled images work in Unity.
    // We could make it work without that by using different fill method on the 
    // image, but the effect wouldn't be so similar to original Witcher energy 
    // bar
    energy.fillAmount = playerEnergy.CurrentValue / playerEnergy.MaxValue * 0.65f;
  }
}
