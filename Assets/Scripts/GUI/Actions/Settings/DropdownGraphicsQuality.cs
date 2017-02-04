using UnityEngine;
using UnityEngine.UI;

/* UI dropdown menu item that allows you to set render Quality level. */
public class DropdownGraphicsQuality : Dropdown {

	/* On start, add all possible qualities and add the event listener. */
	protected override void Start () {
        base.Start();
        int optionNumber = 0;
        string[] qualityNames = QualitySettings.names;
        int currentQuality = QualitySettings.GetQualityLevel();
        options.Clear();
        foreach (string qualityName in qualityNames)
        {
            options.Add(new OptionData(qualityName));
            if (optionNumber == currentQuality)
                this.value = currentQuality;
            optionNumber++;
        }
        this.onValueChanged.AddListener(delegate {
            QualitySettings.SetQualityLevel(this.value);
        });
    }
}
