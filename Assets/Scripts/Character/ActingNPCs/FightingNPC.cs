using UnityEngine;
using System.Collections;

public class FightingNPC : ActingNPC {
  public override bool InCombat {
    get {
      return base.InCombat || ActionTarget != null;
    }
  }
}
