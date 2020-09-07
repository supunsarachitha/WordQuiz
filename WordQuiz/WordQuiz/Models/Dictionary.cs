using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordQuiz.Models
{
    public partial class Dictionary
    {
        [JsonProperty("word")]
        public string Word { get; set; }

        [JsonProperty("phonetics")]
        public Phonetic[] Phonetics { get; set; }

        [JsonProperty("meanings")]
        public Meaning[] Meanings { get; set; }
    }

    public partial class Meaning
    {
        [JsonProperty("partOfSpeech")]
        public string PartOfSpeech { get; set; }

        [JsonProperty("definitions")]
        public Definition[] Definitions { get; set; }
    }

    public partial class Definition
    {
        [JsonProperty("definition")]
        public string DefinitionDefinition { get; set; }

        [JsonProperty("example", NullValueHandling = NullValueHandling.Ignore)]
        public string Example { get; set; }

        [JsonProperty("synonyms", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Synonyms { get; set; }
    }

    public partial class Phonetic
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("audio")]
        public Uri Audio { get; set; }
    }



}
