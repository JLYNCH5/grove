﻿namespace Grove.Modifiers
{
  using System.Collections.Generic;

  public class ChangeBasicLandSubtype : Modifier, ICardModifier
  {
    private readonly string _landSubtype;
    private readonly bool _replace;
    private ActivatedAbilities _abilities;
    private ActivatedAbility _addedAbility;
    private CardTypeCharacteristic _cardType;
    private CardTypeSetter _cardTypeModifier;
    private PropertyModifier<List<ActivatedAbility>> _modifier;

    private ChangeBasicLandSubtype() {}

    public ChangeBasicLandSubtype(string landSubtype, bool replace)
    {
      _landSubtype = landSubtype;
      _replace = replace;
    }

    public override void Apply(ActivatedAbilities abilities)
    {
      _abilities = abilities;

      var ap = new ManaAbilityParameters
        {
          Text = "{{T}}: Add {0} to your mana pool.",
        };

      ap.ManaAmount(Mana.GetBasicLandMana(_landSubtype));
      _addedAbility = new ManaAbility(ap);
      _addedAbility.Initialize(OwningCard, Game);

      if (_replace)
      {
        _modifier = new SetList<ActivatedAbility>(new List<ActivatedAbility> {_addedAbility});
      }
      else
      {
        _modifier = new AddToList<ActivatedAbility>(_addedAbility);
      }

      _modifier.Initialize(ChangeTracker);
      _abilities.AddModifier(_modifier);
    }

    public override void Apply(CardTypeCharacteristic cardType)
    {
      _cardType = cardType;

      var type = _cardType.Value.Change(subTypes: _landSubtype);
      _cardTypeModifier = new CardTypeSetter(type);
      _cardTypeModifier.Initialize(ChangeTracker);

      _cardType.AddModifier(_cardTypeModifier);
    }

    protected override void Unapply()
    {
      _cardType.RemoveModifier(_cardTypeModifier);
      _abilities.RemoveModifier(_modifier);
    }
  }
}