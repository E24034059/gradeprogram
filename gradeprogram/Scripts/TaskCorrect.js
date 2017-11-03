$(document).ready(function () {
    $('#Course').change(function () {
        ChangeCourse();
        $('#assignmentType').on('change', function (event) {
            ChangeType();
        });
    });

});

function SetHWNumEmpty() {
    $('#HWNum').empty();
    $('#HWNum').append($('<option></option>').val('').text('請選擇'));
}
function SetassignmentTypeEmpty() {
    $('#assignmentType').empty();
    $('#assignmentType').append($('<option></option>').val('').text('請選擇'));
}
function ChangeCourse() {
    var selectedCourseId = $.trim($('#Course option:selected').val());
    if (selectedCourseId.length == 0) {
        SetassignmentTypeEmpty();
    }
    else {
        $.ajax(
        {
            url: "GetTypeData",
            data: { Course_ID: selectedCourseId},
            type: 'POST',
            cache: false,
            async: false,
            dataType: 'html',

            success: function (data) {
                if (data.length > 0) {
                    $('#assignmentType').replaceWith(data);
                    SetHWNumEmpty();
                }
            }
        });
    }
}
function ChangeType() {
    var selectedCourseId = $.trim($('#Course option:selected').val());
    var assignmentType = $.trim($('#assignmentType option:selected').text());
    if (assignmentType.length == 0) {
        SetHWNumEmpty();
    }
    else {
        $.ajax(
        {
            url: "GetHWData",
            data: { Course_ID: selectedCourseId, assignmentType: assignmentType },
            type: 'POST',
            cache: false,
            async: false,
            dataType: 'html',

            success: function (data) {
                if (data.length > 0) {
                    $('#HWNum').replaceWith(data);
                }
            }
        });
    }
}

function CorrectSelectTask() {
    var selectedCourseId = $("#Course").find(":selected").val();
    var selectassignmentType = $.trim($('#assignmentType option:selected').text());
    var selectedHWNum = $("#HWNum option:selected").text();
    $.ajax(
       {
           url: "CheckIsAnswerExist",
           data: { Course_ID: selectedCourseId, HWNum: selectedHWNum },
           type: 'POST',
           cache: false,
           async: false,
           dataType: 'html',

           success: function (data) {
               if (data === "True") {
                   alert(selectedCourseId + selectedHWNum);
                   $.ajax(
                         {
                             url: "CorrectTask",
                             data: { Course_ID: selectedCourseId, assignmentType: selectassignmentType, HWNum: selectedHWNum },
                             type: "POST",
                             cache: false,
                             async: false,
                             dataType: 'html',
                             success: function (data) {

                             }
                         });
                   
               }
               else
               {
                   alert("There isn't a correct answer of this homework!");
               }
           }
       });
        
    
}