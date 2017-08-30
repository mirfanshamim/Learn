function GuessAnimal() {

    this.AnsweredQuestions = {};
    this.HasQuestions = {};
    this.BehaveQuestions = {};
    this.IsQuestion = {};
    this.HasQuestionURL = '';
    this.IsQuestionURL = '';
    this.BehaveQuestionURL = '';
    this.CheckFoundURL = '';
    this.CurrentQuestion = {};

    this.AnswerDone = 0;

    this.GetQuestion = function () {
        
        if (!this.AnsweredQuestions || !this.AnsweredQuestions.length || this.AnsweredQuestions.length == 0) {
            
            this.GetHasQuestions();
        }
        else if (this.AnswerDone == 0 && this.AnsweredQuestions.length == 1) {
            return this.GetBehaveQuestions();
        }
        else if (this.AnswerDone == 0 && this.AnsweredQuestions.length == 2) {
            //return this.GetIsQuestion();
        }
    };

    this.GetHasQuestions = function () {
        var self = this;
       
        $.get(this.HasQuestionURL, function (data) {
           
            if (data && data.Message == "Success") {
                var result = data.HasQuestions;
                
                for (var cnt = 0; cnt < result.length; cnt++) {
                    var question = new Question();
                    question.Type = 'has';
                    question.Description = result[cnt];
                    question.IsAnswered = 0;
                    question.IsAsked = 0;

                    self.HasQuestions[cnt] = question;
                }
                
                
                $("#question").text("It has " + self.HasQuestions[0].Description);
                self.HasQuestions[0].IsAsked = 1;
                self.CurrentQuestion = self.HasQuestions[0];
                $("#dialog").dialog();
            }
        });
    };

    this.GetBehaveQuestions = function () {
        var self = this;

        $.get(this.BehaveQuestionURL, self.CurrentQuestion.Description, function (data) {

            if (data && data.Message == "Success") {
                var result = data.BehaveQuestions;
                

                for (var cnt = 0; cnt < result.length; cnt++) {
                    var question = new Question();
                    question.Type = 'behave';
                    question.Description = result[cnt];
                    question.IsAnswered = 0;
                    question.IsAsked = 0;

                    self.HasQuestions[cnt] = question;
                }


                $("#question").text("It " + self.BehaveQuestions[0].Description);
                self.BehaveQuestions[0].IsAsked = 1;
                self.CurrentQuestion = self.BehaveQuestions[0];
                $("#dialog").dialog();
            }
        });
    };

    this.GetIsQuestions = function () {
        var self = this;

        $.get(this.IsQuestionURL, self.AnsweredQuestions[0].Description, self.AnsweredQuestions[1].Description, function (data) {

            if (data && data.Message == "Success") {
                var result = data.IsQuestion;
                

                for (var cnt = 0; cnt < result.length; cnt++) {
                    var question = new Question();
                    question.Type = 'behave';
                    question.Description = result[cnt];
                    question.IsAnswered = 0;
                    question.IsAsked = 0;

                    self.IsQuestion[cnt] = question;
                }


                $("#question").text("It " + self.IsQuestion[0].Description);
                self.IsQuestion[0].IsAsked = 1;
                self.CurrentQuestion = self.IsQuestion[0];
                $("#dialog").dialog();
            }
        });
    };

    this.UpdateNo = function () {
      
        $("#dialog").dialog("close");
        this.CurrentQuestion.IsAnswered = 1;
        this.CurrentQuestion.Yes = 0;

        var NextQuestionQueue;

        if (this.CurrentQuestion.Type = 'has'){
            NextQuestionQueue = this.HasQuestions;
        }
        else if (this.CurrentQuestion.Type = 'behave') {
            NextQuestionQueue = this.BehaveQuestions;
        }
        else {
            NextQuestionQueue = this.IsQuestion;
        }
                
        var showDialog = false;

        for (var cnt = 0; cnt < this.ObjSize(NextQuestionQueue) ; cnt++) {
           
            if (NextQuestionQueue[cnt].IsAsked == 0) {
                 $("#question").text("It " + this.CurrentQuestion.Type + " " + NextQuestionQueue[cnt].Description);
                NextQuestionQueue[cnt].IsAsked = 1;
                self.CurrentQuestion = NextQuestionQueue[cnt];
                $("#dialog").dialog();
                showDialog = true;
                break;
            }           
        }

        if (!showDialog) {
            alert("Please review your answers");
        }      
        
    }

    this.UpdateYes = function () {
        this.CurrentQuestion.IsAnswered = 1;
        this.CurrentQuestion.Yes = 1;

        var index = this.ObjSize(this.AnsweredQuestions);
        this.AnsweredQuestions[index] = this.CurrentQuestion;

        var hasQ = '';
        var behaveQ = '';
        var isQ = '';

        if (index == 0) {
            hasQ = this.AnsweredQuestions[0].Description;
        }
        else if (index == 1) {
            hasQ = this.AnsweredQuestions[0].Description;
            behaveQ = this.AnsweredQuestions[1].Description;
        }
        else if (index == 2) {
            hasQ = this.AnsweredQuestions[0].Description;
            behaveQ = this.AnsweredQuestions[1].Description;
            isQ = behaveQ = this.AnsweredQuestions[2].Description;
        }

        var self = this;
       
        var input = hasQ + "," + behaveQ + "," + isQ;
        
        $.get(this.CheckFoundURL, {q: input}, function (data) {
           
                if (data.count == 1) {
                    $("#dialog").dialog("close");
                    alert("You've thought " + data.animal);
                }
                else {
                    if (self.CurrentQuestion == "has") {
                        self.GetBehaveQuestions();
                    }
                    else {
                        self.GetIsQuestions();
                    }
                }
            
        });       
    }

    this.ObjSize = function (obj) {
        var size = 0, key;
        for (key in obj) {
            if (obj.hasOwnProperty(key)) size++;
        }
        return size;
    };
};

function Question() {
    this.Type = '';
    this.IsAnswered = 0;
    this.Description = '';
    this.IsAsked = 0;
    this.Yes = 0;
};