﻿namespace Grove.Artifical.TargetingRules
{
  using System.Collections.Generic;
  using Gameplay;
  using Gameplay.Misc;
  using Gameplay.Targeting;

  public class EffectLandEnchantment : TargetingRule
  {
    private readonly ControlledBy _controlledBy;

    public EffectLandEnchantment(ControlledBy controlledBy)
    {
      _controlledBy = controlledBy;
    }

    private EffectLandEnchantment() {}

    protected override IEnumerable<Targets> SelectTargets(TargetingRuleParameters p)
    {
      var candidates = p.Candidates<Card>(_controlledBy);
      return Group(candidates, 1);
    }
  }
}