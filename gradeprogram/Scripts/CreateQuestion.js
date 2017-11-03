$(document).ready(function () {

});


    function CreateQuestion()
    {       
    var selectedCourseId = $('#Course option:selected').text();
    var question = $('#question').val();
    var outputformat = $('#outputformat').val();
    var testinput = $('#testinput').val();
    var answer = $('#answer').val();
    var data = new FormData(document.querySelector("form"));
    data.append("Course_ID", selectedCourseId);
    data.append("question",question);
    data.append("outputformat", outputformat);
    data.append("testinput", testinput);
    data.append("answer", answer);
    $.ajax(
       {
           url: "QuestionUpload",
           headers: {'Content-Type': undefined},
           type: "POST",
           cache: false,
           async: false,
           contentType: false,
           processData: false,
           data: data,
           dataType: 'html',
           success: function (data) {
              
                                    }
       });
    }