﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Ai.TimingRules;
  using Gameplay.Card.Factory;
  using Gameplay.Effects;

  public class CruelEdict : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Cruel Edict")
        .ManaCost("{1}{B}")
        .Type("Sorcery")
        .Text("Target opponent sacrifices a creature.")
        .FlavorText("Choose your next words carefully. They will be your last.")
        .Cast(p =>
          {
            p.Effect = () => new PlayerSacrificeCreatures(1, P(e => e.Controller.Opponent));
            p.TimingRule(new NonTargetRemoval(1));
          });
    }
  }
}