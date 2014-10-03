﻿namespace Grove.Effects
{
    using System.Collections.Generic;
    using System.Linq;
    using Decisions;
    using Modifiers;

    public class PreventAllDamagesToTargetsFromChosenColor : CustomizableEffect
    {
        public override ChosenOptions ChooseResult(List<IEffectChoice> candidates)
        {
            CardColor? color = null;

            if (!Stack.IsEmpty && !Stack.TopSpell.HasColor(CardColor.Colorless) &&
                !Stack.TopSpell.HasColor(CardColor.None))
            {
                color = Stack.TopSpell.Colors[0];
            }

            color = color ?? Controller.Opponent.Battlefield.GetMostCommonColor();

            return new ChosenOptions(ChoiceToColorMap.Single(x => x.Color == color).Choice);
        }

        public override void ProcessResults(ChosenOptions results)
        {
            var color = ChoiceToColorMap.Single(x => x.Choice.Equals(results.Options[0])).Color;

            var mp = new ModifierParameters
            {
                SourceCard = Source.OwningCard,
                SourceEffect = this,
            };

            foreach (var target in ValidEffectTargets)
            {
                var prevention = new PreventDamageToTarget(target, c => c.HasColor(color));
                var modifier = new AddDamagePrevention(prevention) { UntilEot = true };
                Game.AddModifier(modifier, mp);
            }
        }

        public override string GetText()
        {
            return "Prevent all damage that would be dealt by sources of #0.";
        }

        public override IEnumerable<IEffectChoice> GetChoices()
        {
            yield return new DiscreteEffectChoice(
              EffectOption.White,
              EffectOption.Blue,
              EffectOption.Black,
              EffectOption.Red,
              EffectOption.Green);
        }
    }
}
