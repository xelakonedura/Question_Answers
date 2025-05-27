//Question 1 
var firstDay = new int[] { 1, 2, 3, 3, 3 };
var lastDay = new int[] { 2, 2, 3, 4, 4 };
var result = CountMeetings(firstDay, lastDay);
Console.WriteLine(result); // Output: 4


static int CountMeetings(int[] firstDay, int[] lastDay)
{
    int n = firstDay.Length;
    var investors = new List<(int start, int end)>();

    for (int i = 0; i < n; i++)
    {
        investors.Add((firstDay[i], lastDay[i]));
    }

    // Sort by lastDay (end) to prioritize earliest finishing investors
    investors.Sort((a, b) => a.end.CompareTo(b.end));

    var usedDays = new HashSet<int>();
    var count = 0;

    foreach (var (start, end) in investors)
    {
        for (var day = start; day <= end; day++)
        {
            if (usedDays.Contains(day))
            {
                continue;
            }
            usedDays.Add(day);
            count++;
            break;
        }
    }

    return count;
}
//Question 2 
var words = new string[] { "desserts", "stressed", "bats", "stabs", "are", "not" };
var phrases = new string[] { "bats are not stressed" };

var result2 = Substitutions(words, phrases);
Console.WriteLine(string.Join(", ", result2)); // Output: 2

static int[] Substitutions(string[] words, string[] phrases)
{
    var anagramMap = new Dictionary<string, int>();
    var wordToAnagramCount = new Dictionary<string, int>();

    // Step 1: Group words by sorted key
    foreach (var word in words)
    {
        var key = String.Concat(word.OrderBy(c => c));
        if (!anagramMap.ContainsKey(key))
            anagramMap[key] = 0;
        anagramMap[key]++;
    }

    // Step 2: Map each word to its group size
    foreach (var word in words)
    {
        var key = String.Concat(word.OrderBy(c => c));
        wordToAnagramCount[word] = anagramMap[key];
    }

    // Step 3: For each phrase, multiply substitution counts
    var result = new int[phrases.Length];

    for (var i = 0; i < phrases.Length; i++)
    {
        var wordsInPhrase = phrases[i].Split(' ');
        var count = 1;

        foreach (var word in wordsInPhrase)
        {
            if (wordToAnagramCount.TryGetValue(word, out int options))
            {
                count *= options;
            }
            else
            {
                count = 0;
                break;
            }
        }

        result[i] = count;
    }

    return result;
}