using System;
using System.Collections.Generic;
using System.Text;

namespace WordQuiz.Models
{
    public class Dictionary
    {
        public string word { get; set; }
        public List<Phonetics> phonetics { get; set; }

        public Meaning meaning { get; set; }
    }

    public class Phonetics
    {
        public string text { get; set; }
        public string audio { get; set; }
    }

    public class Meaning
    {
        public List<Noun> noun { get; set; }
        public List<Noun> verb { get; set; }
    }

    public class Noun
    {
        public string definition { get; set; }
        public string example { get; set; }
        public List<string> synonyms { get; set; }
    }
}
