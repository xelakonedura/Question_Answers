//Question 1 
var firstDay = new int[] { 1, 2, 3, 3, 3 };
var lastDay = new int[] { 2, 2, 3, 4, 4 };
var result = CountMeetings(firstDay, lastDay);
Console.WriteLine(result); // Output: 4

static int CountMeetings(int[] firstDay, int[] lastDay)
{
    var usedDays = new HashSet<int>();
    return firstDay.Zip(lastDay, (start, end) => (start, end))
        .OrderBy(x => x.end)
        .Count(investor => 
        {
            for (var day = investor.start; day <= investor.end; day++)
            {
                if (!usedDays.Contains(day))
                {
                    usedDays.Add(day);
                    return true;
                }
            }
            return false;
        });
}

//Question 2 
var words = new string[] { "desserts", "stressed", "bats", "stabs", "are", "not" };
var phrases = new string[] { "bats are not stressed" };

var result2 = Substitutions(words, phrases);
Console.WriteLine(string.Join(", ", result2)); // Output: 2

static int[] Substitutions(string[] words, string[] phrases)
{
    var anagramMap = words
        .GroupBy(word => String.Concat(word.OrderBy(c => c)))
        .ToDictionary(g => g.Key, g => g.Count());

    return phrases
        .Select(phrase => phrase.Split(' ')
            .Select(word => anagramMap.GetValueOrDefault(String.Concat(word.OrderBy(c => c)), 0))
            .Aggregate(1, (acc, count) => count == 0 ? 0 : acc * count))
        .ToArray();
}