﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Dsl;
  using Core.Effects;
  using Core.Modifiers;
  using Core.Triggers;

  public class Dromosaur : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Dromosaur")
        .ManaCost("{2}{R}")
        .Type("Creature Lizard")
        .Text("Whenever Dromosaur blocks or becomes blocked, it gets +2/-2 until end of turn.")
        .FlavorText(
          "They say dromosaurs are frightened of dogs, even little ones. There are no dogs in Shiv. Not even little ones.")
        .Power(2)
        .Toughness(3)
        .Abilities(
          TriggeredAbility(
            "Whenever Dromosaur blocks or becomes blocked, it gets +2/-2 until end of turn.",
            Trigger<OnBlock>(t =>
              {
                t.GetsBlocked = true;
                t.Blocks = true;
              }),
            Effect<ApplyModifiersToSelf>(e => e.Modifiers(
              Modifier<AddPowerAndToughness>(m =>
                {
                  m.Power = 2;
                  m.Toughness = -2;
                }, untilEndOfTurn: true))), triggerOnlyIfOwningCardIsInPlay: true)
        );
    }
  }
}