﻿namespace Grove.Ai.Decisions
{
  public class PayOr : Gameplay.Decisions.PayOr
  {
    protected override void ExecuteQuery()
    {
      if (ChooseDecisionResults != null)
      {
        Result = ChooseDecisionResults.ChooseResult();
      }

      Result = true;
    }
  }
}