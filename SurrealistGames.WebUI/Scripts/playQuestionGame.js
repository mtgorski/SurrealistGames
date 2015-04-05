$.validator.addMethod("regex", function (value, element, regex) {
    return value.match(/^\s*[wW]/) != null;
}, "Your input does not match the expected format.");

$(document).ready(function () {
    $('#saveButton').prop('disabled', true);
    $("#reportQBtn").hide();
    $("#reportABtn").hide();
    $("#reportQBtn .auth").click(function () {
        onReportClick("#reportQBtn", "Q");
    });
    $("#reportABtn .auth").click(function () {
        onReportClick("#reportABtn", "A");
    });

    $('#prefixForm').validate({
        rules: {
            prefix: {
                required: true,
                regex: /^[wW]/
            }
        },
        onkeyup: false,
        onfocusout: false,
        submitHandler: function (form) {
            $.ajax({
                url: "/QuestionGame/SubmitQuestionAction",
                method: "POST",
                data: { questionContent: $("#prefix").val() },
                success: function (data, textStatus, jqXHR) {
                    onSubmitResponseHandler(data, textStatus, jqXHR);
                    $("#reportABtn").show();
                    $("#reportQBtn").hide();
                    $("#reportABtn .auth").prop("disabled", false).html("Report");
                }
            });

            $('#prefixForm .form-group').removeClass('has-success');
        },
        messages: {
            prefix: {
                required: "The question cannot be empty.",
                regex: "The question must begin with 'what'."
            }
        }
    });

    $('#suffixForm').validate({
        rules: {
            suffix: {
                required: true
            }
        },
        onkeyup: false,
        onfocusout: false,
        submitHandler: function (form) {
            $.ajax({
                url: "/QuestionGame/SubmitAnswerAction",
                method: "POST",
                data: { answerContent: $("#suffix").val() },
                success: function (data, textStatus, jqXHR) {
                    onSubmitResponseHandler(data, textStatus, jqXHR);
                    $("#reportABtn").hide();
                    $("#reportQBtn").show();
                    $("#reportQBtn .auth").prop("disabled", false).html("Report");
                }
            });

            $('#suffixForm .form-group').removeClass('has-success');
        },
        messages: {
            suffix: {
                required: "The answer cannot be empty."
            }
        }
    });
});

function onSubmitResponseHandler(data, textStatus, jqXHR) {
    if (data.Success) {
        $("#question").html(data.GameOutcome.Question.QuestionContent);
        $("#answer").html(data.GameOutcome.Answer.AnswerContent);
        $("#prefix").val("");
        $("#suffix").val("");
        $('input[name="QuestionId"]').val(data.GameOutcome.Question.QuestionId);
        $('input[name="AnswerId"]').val(data.GameOutcome.Answer.AnswerId);
        $('#saveButton').prop('disabled', false).html('Save');
    } else {
        //clear previous question and answer results
        $("#question").html("");
        $("#answer").html("");

        var errors = "";
        for (var i = 0; i < data.ErrorMessages.length; i++) {
            errors += ("<li>" + data.ErrorMessages[i] + "</li>");
        }
        var $errorList = $("<ul>" + errors + "</ul>");

        $("#question").append($errorList);
    }
}

function onSaveClick() {
    var postData = {};
    postData.QuestionId = $('input[name="QuestionId"]').val();
    postData.AnswerId = $('input[name="AnswerId"]').val();

    $.ajax(
    {
        url: '/SavedQuestionGameResult/Post',
        method: 'POST',
        data: postData,
        success: function (data, textStatus, jqXHR) {
            if (!Boolean(data.LoggedIn)) {
                alert("Please log in to use this feature.");
            }

            $("#saveButton").prop('disabled', true).html("Saved");

        }
    });
}

function onReportClick(id, type) {
    $(id).prop("disabled", true);

    var jsonRequest = {};
    if (type == "A") {
        jsonRequest["AnswerId"] = $('input[name="AnswerId"]').val();
    }
    else {
        jsonRequest["QuestionId"] = $('input[name="QuestionId"]').val();
    }

    $.post("/api/ReportApi/", jsonRequest, function (data) {
        $(id).html("Thanks!");
        }
    );
}