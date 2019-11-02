using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyScriptureJournal.Models
{
    public class Scripture
    {
        public int ID { get; set; }
        
        [Required]
        public string Book { get; set; }

        [Display(Name = "Note Date"), Required]
        [DataType(DataType.Date)]
        public DateTime NoteDate { get; set; }

        [RegularExpression(@"^(\d+:\d+)$"), Display(Name = "Reference"), Required]
        public string ScriptureReference { get; set; }

        [Display(Name = "Verse Contents")]
        public string VerseContents { get; set; }
        public string Note { get; set; }

    }
}

