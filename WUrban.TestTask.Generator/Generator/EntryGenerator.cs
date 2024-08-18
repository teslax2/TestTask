using System.Text;

namespace WUrban.TestTask.Generator.Generator
{
    internal class EntryGenerator
    {
        private static readonly string[] _words = {
            "the", "be", "to", "of", "and", "a", "in", "that", "have", "I",
            "it", "for", "not", "on", "with", "he", "as", "you", "do", "at",
            "this", "but", "his", "by", "from", "they", "we", "say", "her", "she",
            "or", "an", "will", "my", "one", "all", "would", "there", "their", "what",
            "so", "up", "out", "if", "about", "who", "get", "which", "go", "me",
            "when", "make", "can", "like", "time", "no", "just", "him", "know", "take",
            "person", "into", "year", "your", "good", "some", "could", "them", "see", "other",
            "than", "then", "now", "look", "only", "come", "its", "over", "think", "also",
            "back", "after", "use", "two", "how", "our", "work", "first", "well", "way",
            "even", "new", "want", "because", "any", "these", "give", "day", "most", "us",
            "is", "are", "was", "were", "had", "may", "might", "shall", "should", "must",
            "could", "would", "did", "does", "doing", "done", "has", "having", "made", "make",
            "where", "why", "who", "whom", "whose", "which", "what", "how", "when", "whether",
            "all", "some", "many", "much", "few", "little", "most", "more", "less", "none",
            "both", "either", "neither", "each", "every", "any", "other", "another", "such", "same",
            "own", "self", "selves", "these", "those", "here", "there", "where", "anywhere", "everywhere",
            "nowhere", "home", "inside", "outside", "above", "below", "over", "under", "across", "through",
            "among", "between", "in", "on", "at", "by", "with", "about", "for", "from",
            "off", "out", "up", "down", "into", "onto", "under", "over", "again", "once",
            "never", "always", "sometimes", "often", "usually", "seldom", "rarely", "now", "then", "soon",
            "later", "before", "after", "since", "until", "while", "during", "within", "without", "along",
            "aside", "beneath", "beyond", "inside", "outside", "around", "behind", "ahead", "next", "near",
            "far", "forward", "backward", "left", "right", "together", "apart", "here", "there", "where",
            "today", "tomorrow", "yesterday", "tonight", "morning", "afternoon", "evening", "day", "week", "month",
            "year", "century", "time", "moment", "second", "minute", "hour", "clock", "watch", "calendar",
            "before", "after", "since", "until", "because", "though", "although", "if", "unless", "provided",
            "as", "when", "while", "where", "why", "how", "that", "which", "who", "whom",
            "whose", "with", "without", "about", "above", "across", "after", "against", "along", "among",
            "around", "as", "at", "before", "behind", "below", "beneath", "beside", "between", "beyond",
            "by", "down", "during", "except", "for", "from", "in", "inside", "into", "like",
            "near", "of", "off", "on", "onto", "out", "outside", "over", "past", "since",
            "through", "to", "toward", "under", "until", "up", "upon", "with", "within", "without",
            "able", "bad", "best", "better", "big", "black", "certain", "clear", "different", "early",
            "easy", "economic", "federal", "free", "full", "good", "great", "hard", "high", "human",
            "important", "international", "large", "late", "little", "local", "long", "low", "major", "military",
            "national", "new", "old", "only", "other", "political", "possible", "public", "real", "recent",
            "right", "small", "social", "special", "strong", "sure", "true", "white", "whole", "young",
            "big", "black", "blue", "brown", "dark", "fast", "good", "great", "high", "long",
            "low", "old", "poor", "quick", "red", "short", "slow", "small", "strong", "white",
            "young", "left", "right", "second", "third", "next", "last", "same", "other", "true"
        };

        public static Entry GenerateEntry()
        {
            var random = new Random();
            var length = random.Next(3,20);
            var sentence = new string[length];
            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                {
                    var word = _words[random.Next(_words.Length)];
                    sentence[i] = char.ToUpper(word[0]) + word[1..];
                }
                else
                {
                    sentence[i] = _words[random.Next(_words.Length)];
                }

            }
            return new Entry(random.Next(),string.Join(" ",sentence));
        }
    }
}
