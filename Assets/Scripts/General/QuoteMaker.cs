using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoteMaker : MonoBehaviour {
    [SerializeField]
    private List<string> quotes = new List<string>();

	public string GetQuote()
    {
        int i = Random.Range(0, quotes.Count-1);
        return quotes[i];
    }
}
