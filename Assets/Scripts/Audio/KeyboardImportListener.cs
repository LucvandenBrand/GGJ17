using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Razer.Keyboard.Effects;
using Color = Corale.Colore.Core.Color;

public class KeyboardImportListener : AudioImpactListener {

    public override void AudioImpact( float reguestedCallbackValue ) {
        Keyboard.Instance.SetAll( new Color(
            Random.Range( 0, 0.1f ), 
            1f / (float)reguestedCallbackValue, 
            Random.Range( 0, 0.1f )) );
    }
}
