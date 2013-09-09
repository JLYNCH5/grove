﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Artifical;
  using Artifical.TargetingRules;
  using Gameplay.Effects;
  using Gameplay.Misc;
  using Gameplay.Modifiers;

  public class MightOfOaks : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Might of Oaks")
        .ManaCost("{3}{G}")
        .Type("Instant")
        .Text("Target creature gets +7/+7 until end of turn.")
        .FlavorText("Suddenly, she couldn't see the acorns for the trees.")
        .Cast(p =>
          {
            p.Effect = () => new ApplyModifiersToTargets(
              () => new AddPowerAndToughness(7, 7) {UntilEot = true})
              {Category = EffectCategories.ToughnessIncrease};

            p.TargetSelector.AddEffect(trg => trg.Is.Creature().On.Battlefield());
            p.TargetingRule(new EffectPump(7, 7));
          });
    }
  }
}