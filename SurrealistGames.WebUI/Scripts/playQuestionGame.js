$.validator.addMethod("regex", function (value, element, regex) {
    return value.match(/^\s*[wW]/) != null;
}, "Your input does not match the expected format.");

$(document).ready(function () {
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
                url: "SubmitQuestionAction",
                method: "POST",
                data: { questionContent: $("#prefix").val() },
                success: onSubmitResponseHandler
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
                url: "SubmitAnswerAction",
                method: "POST",
                data: { answerContent: $("#suffix").val() },
                success: onSubmitResponseHandler
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
        $("#question").html(data.GameOutcome.QuestionPrefix.Content);
        $("#answer").html(data.GameOutcome.QuestionSuffix.Content);
        $("#prefix").val("");
        $("#suffix").val("");
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