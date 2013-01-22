﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Costs;
  using Core.Dsl;
  using Core.Effects;
  using Core.Mana;
  using Core.Targeting;

  public class ElvishLyrist : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Elvish Lyrist")
        .ManaCost("{G}")
        .Type("Creature Elf")
        .Text("{G},{T}, Sacrifice Elvish Lyrist: Destroy target enchantment.")
        .FlavorText(
          "Bring the spear of ancient briar;{EOL}Bring the torch to light the pyre.{EOL}Bring the one who trod our ground;{EOL}Bring the spade to dig his mound.")
        .Power(1)
        .Toughness(1)        
        .Abilities(
          ActivatedAbility(
            "{G},{T}, Sacrifice Elvish Lyrist: Destroy target enchantment.",
            Cost<PayMana, Tap>(cost => cost.Amount = "{G}".ParseMana()),
            Effect<DestroyTargetPermanents>(),
            timing: Timings.InstantRemovalTarget(),
            effectTarget: Target(Validators.Card(card => card.Is().Enchantment), Zones.Battlefield()),
            targetingAi: TargetingAi.OrderByScore()            
            )
        );
    }
  }
}