using System;
using UnityEngine;

// Base class for NPCs that take any actions in game.
// It requires the object to also contain implementations of TargetProvider and
// ActionProvider.
// 
// This creates a really robust system - we can create several different 
// TargetProviders - based on range, based on aggro mechanic, even a provider
// that will switch between given objects, like anvil and forge , that can be 
// used by, for example, blacksmith NPC to walk around doing something
//
// Same with ActionProviders, we can have one for running away, one for 
// attacking, one for setting animations, for example, for blacksmith, based
// on whether he is standing by the anvil or a forge.
//
// In case we ever need more parts of NPC behaviour to be changeable 
// independently, we can just add more functions to be called in response to 
// certain situations and add more Provider classes that implement them
public class ActingNPC : Character {
  private TargetProvider targetProvider;
  private ActionProvider actionProvider;

  private GameObject target;

  public GameObject ActionTarget {
    get {
      return target;
    }
    private set {
      if (target != value) {
        target = value;
        actionProvider.OnTargetSet(value);
      } else {
        target = value;
      }
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    targetProvider = GetComponent<TargetProvider>();
    actionProvider = GetComponent<ActionProvider>();

    if (targetProvider == null) {
      throw new Exception(
        typeof(ActingNPC) + " requires " + typeof(TargetProvider) +
        " to work properly"
      );
    }

    if (actionProvider == null) {
      throw new Exception(
        typeof(ActingNPC) + " requires " + typeof(ActionProvider) +
        " to work properly"
      );
    }
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    ActionTarget = targetProvider.ProvideTarget();
    if (ActionTarget != null) {
      actionProvider.PerformAction(ActionTarget);
    }
  }
}
