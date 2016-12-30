using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(Speed))]
[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Character : BaseBehaviour {
  public virtual bool InCombat {
    get {
      return attackers.Count > 0;
    }
  }
  private bool lastFrameCombatStatus;

  protected virtual float DecayTime {
    get {
      return 5;
    }
  }

  private HashSet<GameObject> attackers;
  private Energy energy;
  private Health health;
  private MovementController movementController;
  private Animator animator;
  private CombatBehaviour[] combatBehaviours;
  private Collider characterCollider;

  private int skillStoppers;
  private int movementStoppers;
  private bool isUsingSkill;

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

  public float Height {
    get {
      return characterCollider.bounds.size.y;
    }
  }

  #region CharacterStateMethods
  public void EnableSkillUsage() {
    if (skillStoppers == 0) {
      throw new Exception(
        "You called EnableSkillUsage before calling DisableSkillUsage"
      );
    }
    skillStoppers--;
  }

  public void DisableSkillUsage() {
    skillStoppers++;
  }

  public void EnableMovement() {
    if (movementStoppers == 0) {
      throw new Exception(
        "You called EnableMovement before calling DisableMovement"
      );
    }
    movementStoppers--;
    movementController.CanMove = movementStoppers == 0;
  }

  public void DisableMovement() {
    movementStoppers++;
    movementController.CanMove = false;
  }

  public void EnableAction() {
    EnableMovement();
    EnableSkillUsage();
  }

  public void DisableAction() {
    DisableMovement();
    DisableSkillUsage();
  }

  public bool IsUsingSkill {
    get {
      return isUsingSkill;
    }
    set {
      isUsingSkill = value;
      if (isUsingSkill) {
        DisableMovement();
      } else {
        EnableMovement();
      }
    }
  }

  public bool CanMove {
    get {
      return movementController.CanMove;
    }
  }

  public bool CanUseSkills {
    get {
      return skillStoppers == 0;
    }
  }

  public void ReceiveDisablingHit(float duration) {
    if (Health.CurrentValue == 0) {
      return;
    }
    StartCoroutine(DisablingHitCoroutine(duration));
  }

  private IEnumerator DisablingHitCoroutine(float duration) {
    DisableAction();
    Animator.SetTrigger(AnimationConstants.GET_HIT);
    yield return new WaitForSeconds(duration);
    EnableAction();
  }
  #endregion

  protected override void OnAwake() {
    base.OnAwake();

    energy = GetComponent<Energy>();
    health = GetComponent<Health>();
    movementController = GetComponent<MovementController>();
    combatBehaviours = GetComponents<CombatBehaviour>();
    attackers = new HashSet<GameObject>();
    animator = GetComponent<Animator>();
    characterCollider = GetComponent<Collider>();

    health.OnDeath += OnDeath;
  }

  private void OnDeath() {
    DisableAction();
    Destroy(gameObject, DecayTime);
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
    OnAttackerRegistered(attacker);
  }

  protected virtual void OnAttackerRegistered(GameObject attacker) { }

  public void UnregisterAttacker(GameObject attacker) {
    attackers.Remove(attacker);
    OnAttackerUnregistered(attacker);
  }

  protected virtual void OnAttackerUnregistered(GameObject attacker) { }

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
