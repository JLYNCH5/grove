﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Dsl;
  using Core.Effects;
  using Core.Modifiers;
  using Core.Preventions;
  using Core.Triggers;
  using Core.Zones;

  public class EnergyField : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Energy Field")
        .ManaCost("{1}{U}")
        .Type("Enchantment")
        .Text(
          "Prevent all damage that would be dealt to you by sources you don't control.{EOL}When a card is put into your graveyard from anywhere, sacrifice Energy Field.")
        .Abilities(
          Continuous(e =>
            {
              e.ModifierFactory = Modifier<AddDamagePrevention>(
                m => m.Prevention = Prevention<PreventDamageToTarget>(p =>
                  {                    
                    p.SourceFilter = (self, source) => self.Controller != source.Controller;
                  }));
              e.CardFilter = delegate { return false; };
              e.PlayerFilter = (player, effect) => player == effect.Source.Controller;
            }),
          TriggeredAbility(
            "When a card is put into your graveyard from anywhere, sacrifice Energy Field.",
            Trigger<OnZoneChanged>(t =>
              {
                t.Filter = (ability, card) => ability.OwningCard.Controller == card.Owner;
                t.To = Zone.Graveyard;
              }),
            Effect<SacrificeSource>(),
            triggerOnlyIfOwningCardIsInPlay: true
            )
        );
    }
  }
}