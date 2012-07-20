﻿namespace Grove.Core.Details.Cards.Costs
{
  using Ai;
  using Dsl;
  using Infrastructure;
  using Targeting;

  [Copyable]
  public abstract class Cost : IHashable
  {
    protected Card Card { get; private set; }
    protected ICardController Controller { get { return Card.Controller; } }
    protected Game Game { get; private set; }
    public TargetSelector Selector { get; private set;  }

    public CalculateX XCalculator { get; set; }

    public virtual int CalculateHash(HashCalculator calc)
    {
      return GetType().GetHashCode();
    }

    public bool CanPay()
    {
      int? maxX = null;
      return CanPay(ref maxX);
    }

    public abstract bool CanPay(ref int? maxX);
    public abstract void Pay(ITarget target, int? x);

    protected virtual void AfterInit() {}

    public class Factory<TCost> : ICostFactory where TCost : Cost, new()
    {
      public Initializer<TCost> Init = delegate { };
      public Game Game { get; set; }

      public Cost CreateCost(Card card, TargetSelector selector)
      {
        var cost = new TCost();
        cost.Card = card;
        cost.Game = Game;
        cost.Selector = selector;

        Init(cost, new CardBuilder(Game));
        cost.AfterInit();

        return cost;
      }
    }
  }
}