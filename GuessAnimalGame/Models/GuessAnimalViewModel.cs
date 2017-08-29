using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessAnimalGame.Models
{
    public class GuessAnimalViewModel
    {
        public virtual List<string> HasQuestions { get; set; }

        public virtual List<string> BehaveQuestions { get; set; }

        public virtual List<string> IsQuestions { get; set; }

        public virtual string Message { get; set; }

        public GuessAnimalViewModel() {
            HasQuestions = new List<string>();
            BehaveQuestions = new List<string>();
            IsQuestions = new List<string>();
            Message = "Success";
        }
    }
}