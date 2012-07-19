﻿namespace Grove.Core.Details.Cards.Effects
{
  using System;

  public class DestroyPermanents : Effect
  {
    public Func<Card, bool> Filter;

    protected override void ResolveEffect()
    {
      Players.DestroyPermanents(Filter);
    }
  }
}