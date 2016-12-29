using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(Speed))]
[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
public class Character : BaseBehaviour {
  public virtual bool InCombat {
    get {
      return attackers.Count > 0;
    }
  }
  private bool lastFrameCombatStatus;

  private HashSet<GameObject> attackers;
  private Energy energy;
  private Health health;
  private MovementController movementController;
  private Animator animator;
  private CombatBehaviour[] combatBehaviours;

  public Energy Energy {
    get {
      return energy;
    }
  }

  public Health Health {
    get {
      return health;
    }
  }

  public MovementController MovementController {
    get {
      return movementController;
    }
  }

  public Animator Animator {
    get {
      return animator;
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    energy = GetComponent<Energy>();
    health = GetComponent<Health>();
    movementController = GetComponent<MovementController>();
    combatBehaviours = GetComponents<CombatBehaviour>();
    attackers = new HashSet<GameObject>();
    animator = GetComponent<Animator>();
  }

  protected override void OnStart() {
    base.OnStart();

    energy.BaseMaxValue = 100;
    energy.RegenerationDelay = 1;
    energy.BaseRegenRate = 50;
    health.BaseMaxValue = 100;
    health.BaseRegenRate = 5;
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    if (InCombat && !lastFrameCombatStatus) {
      OnCombatEnter();
    }
    if (!InCombat && lastFrameCombatStatus) {
      OnCombatLeave();
    }
    lastFrameCombatStatus = InCombat;
  }

  public void RegisterAttacker(GameObject attacker) {
    attackers.Add(attacker);
  }

  public void UnregisterAttacker(GameObject attacker) {
    attackers.Remove(attacker);
  }

  // These 2 functions make use of "CombatBehaviours". They are used for things
  // like removing regeneration in combat or comming back to spawn point after
  // fight.
  protected virtual void OnCombatEnter() {
    foreach (CombatBehaviour cb in combatBehaviours) {
      cb.OnCombatEnter(this);
    }
  }

  protected virtual void OnCombatLeave() {
    foreach (CombatBehaviour cb in combatBehaviours) {
      cb.OnCombatLeave(this);
    }
  }
}
