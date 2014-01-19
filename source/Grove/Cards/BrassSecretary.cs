﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Artifical.TimingRules;
  using Gameplay;
  using Gameplay.Costs;
  using Gameplay.Effects;
  using Gameplay.ManaHandling;
  using Gameplay.Misc;

  public class BrassSecretary : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Brass Secretary")
        .ManaCost("{3}")
        .Type("Artifact Creature Construct")
        .Text("{2}, Sacrifice Brass Secretary: Draw a card.")
        .FlavorText("The students disliked the secretary for its unfailing memory—until they discovered it couldn't dodge thrown objects.")
        .Power(2)
        .Toughness(1)
        .ActivatedAbility(p =>
          {
            p.Text = "{2}, Sacrifice Brass Secretary: Draw a card.";

            p.Cost = new AggregateCost(
              new PayMana(2.Colorless(), ManaUsage.Abilities),
              new Sacrifice());

            p.Effect = () => new DrawCards(1);

            p.TimingRule(new Any(
              new WhenOwningCardWillBeDestroyed(),              
              new OnEndOfOpponentsTurn()));            
          });
    }
  }
}