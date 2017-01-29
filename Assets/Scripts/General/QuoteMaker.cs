using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Provide this class with a list of quotes, and it
 * will return a random quote from the list when asked. */
public class QuoteMaker : MonoBehaviour {
    [SerializeField]
    private List<string> quotes = new List<string>();

	public string GetQuote()
    {
        string quote = "";
        if (quotes.Count > 0)
            quote = quotes[Random.Range(0, quotes.Count-1)];
        return quote;
    }
}
