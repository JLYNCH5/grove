﻿namespace Grove.Gameplay.Decisions
{
  using System.Collections.Generic;
  using Effects;
  using Results;

  public abstract class ChooseEffectOptions : Decision<ChosenOptions>
  {
    public List<object> Choices;
    public IChooseDecisionResults<List<object>, ChosenOptions> ChooseDecisionResults;
    public IProcessDecisionResults<ChosenOptions> ProcessDecisionResults;
    public string Text;

    protected override bool ShouldExecuteQuery { get { return true; } }
    public Effect Effect { get; set; }

    public override void ProcessResults()
    {
      ProcessDecisionResults.ProcessResults(Result);
    }
  }
}