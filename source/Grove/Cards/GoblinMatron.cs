﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Dsl;
  using Core.Effects;
  using Core.Triggers;
  using Core.Zones;

  public class GoblinMatron : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Goblin Matron")
        .ManaCost("{2}{R}")
        .Type("Creature Goblin")
        .Text(
          "When Goblin Matron enters the battlefield, you may search your library for a Goblin card, reveal that card, and put it into your hand. If you do, shuffle your library.")
        .FlavorText("There's always room for one more.")
        .Power(1)
        .Toughness(1)
        .TriggeredAbility(p =>
          {
            p.Text =
              "When Goblin Matron enters the battlefield, you may search your library for a Goblin card, reveal that card, and put it into your hand. If you do, shuffle your library.";
            p.Trigger(new OnZoneChanged(to: Zone.Battlefield));
            p.Effect = () => new SearchLibraryPutToHand(
              minCount: 0,
              maxCount: 1,
              validator: c => c.Is("goblin"),
              text: "Search you library for a goblin card."
              );
          });
    }
  }
}