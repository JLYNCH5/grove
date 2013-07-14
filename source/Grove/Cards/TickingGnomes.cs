﻿namespace Grove.Cards
{
  using System;
  using System.Collections.Generic;
  using Artifical.TargetingRules;
  using Artifical.TimingRules;
  using Gameplay.Costs;
  using Gameplay.Effects;
  using Gameplay.Misc;

  public class TickingGnomes : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Ticking Gnomes")
        .ManaCost("{3}")
        .Type("Artifact Creature Gnome")
        .Text(
          "{Echo} {3}(At the beginning of your upkeep, if this came under your control since the beginning of your last upkeep, sacrifice it unless you pay its echo cost.){EOL}Sacrifice Ticking Gnomes: Ticking Gnomes deals 1 damage to target creature or player.")
        .Power(3)
        .Toughness(3)
        .Echo("{3}")
        .ActivatedAbility(p =>
          {
            p.Text = "Sacrifice Ticking Gnomes: Ticking Gnomes deals 1 damage to target creature or player.";
            p.Cost = new Sacrifice();
            p.Effect = () => new DealDamageToTargets(1);
            p.TargetSelector.AddEffect(trg => trg.Is.CreatureOrPlayer().On.Battlefield());
            p.TargetingRule(new DealDamage(1));
            p.TimingRule(new TargetRemoval());
          });
    }
  }
}