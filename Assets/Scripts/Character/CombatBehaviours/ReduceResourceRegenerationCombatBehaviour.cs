using UnityEngine;

public class ReduceResourceRegenerationCombatBehaviour : CombatBehaviour {
  [SerializeField]
  private float healthRatio;
  [SerializeField]
  private float energyRatio;

  public override void OnCombatEnter(Character character) {
    character.Health.BonusRegenRate -=
      healthRatio * character.Health.BaseRegenRate;
    character.Energy.BonusRegenRate -=
      energyRatio * character.Energy.BaseRegenRate;
  }

  public override void OnCombatLeave(Character character) {
    character.Health.BonusRegenRate +=
      healthRatio * character.Health.BaseRegenRate;
    character.Energy.BonusRegenRate +=
      energyRatio * character.Energy.BaseRegenRate;
  }
}
