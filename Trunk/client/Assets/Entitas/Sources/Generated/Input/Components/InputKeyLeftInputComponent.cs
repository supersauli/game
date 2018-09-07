//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public KeyLeftInputComponent keyLeftInput { get { return (KeyLeftInputComponent)GetComponent(InputComponentsLookup.KeyLeftInput); } }
    public bool hasKeyLeftInput { get { return HasComponent(InputComponentsLookup.KeyLeftInput); } }

    public void AddKeyLeftInput(bool newValue) {
        var index = InputComponentsLookup.KeyLeftInput;
        var component = CreateComponent<KeyLeftInputComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceKeyLeftInput(bool newValue) {
        var index = InputComponentsLookup.KeyLeftInput;
        var component = CreateComponent<KeyLeftInputComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveKeyLeftInput() {
        RemoveComponent(InputComponentsLookup.KeyLeftInput);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherKeyLeftInput;

    public static Entitas.IMatcher<InputEntity> KeyLeftInput {
        get {
            if (_matcherKeyLeftInput == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.KeyLeftInput);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherKeyLeftInput = matcher;
            }

            return _matcherKeyLeftInput;
        }
    }
}