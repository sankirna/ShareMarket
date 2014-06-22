var FormValidation = function () {
    var regxDate = "/^(\d{1,2})-(\d{1,2})-(\d{4})$/";
    return {
        //main function to initiate the module
        init: function () {

            // for more info visit the official plugin documentation: 
            // http://docs.jquery.com/Plugins/Validation

            var form1 = $('#form_sample_1');
            var error1 = $('.alert-error', form1);
            var success1 = $('.alert-success', form1);

            form1.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",
                rules: {
                    name: {
                        minlength: 2,
                        required: true
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    url: {
                        required: true,
                        url: true
                    },
                    number: {
                        required: true,
                        number: true
                    },
                    digits: {
                        required: true,
                        digits: true
                    },
                    creditcard: {
                        required: true,
                        creditcard: true
                    },
                    occupation: {
                        minlength: 5,
                    },
                    category: {
                        required: true
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit              
                    success1.hide();
                    error1.show();
                    App.scrollTo(error1, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.help-inline').removeClass('ok'); // display OK icon
                    $(element)
                        .closest('.control-group').removeClass('success').addClass('error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change dony by hightlight
                    $(element)
                        .closest('.control-group').removeClass('error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .addClass('valid').addClass('help-inline ok') // mark the current input as valid and display OK icon
                    .closest('.control-group').removeClass('error').addClass('success'); // set success class to the control group
                },

                submitHandler: function (form) {
                    success1.show();
                    error1.hide();
                }
            });

            //Sample 2
            var form2 = $('#form_sample_2');
            var error2 = $('.alert-error', form2);
            var success2 = $('.alert-success', form2);

            form2.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",
                rules: {
                    name: {
                        minlength: 2,
                        required: true
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    category: {
                        required: true
                    },
                    education: {
                        required: true
                    },
                    occupation: {
                        minlength: 5,
                    },
                    membership: {
                        required: true
                    },
                    service: {
                        required: true,
                        minlength: 2
                    }
                },

                messages: { // custom messages for radio buttons and checkboxes
                    membership: {
                        required: "Please select a Membership type"
                    },
                    service: {
                        required: "Please select  at least 2 types of Service",
                        minlength: jQuery.format("Please select  at least {0} types of Service")
                    }
                },

                errorPlacement: function (error, element) { // render error placement for each input type
                    if (element.attr("name") == "education") { // for chosen elements, need to insert the error after the chosen container
                        error.insertAfter("#form_2_education_chzn");
                    } else if (element.attr("name") == "membership") { // for uniform radio buttons, insert the after the given container
                        error.addClass("no-left-padding").insertAfter("#form_2_membership_error");
                    } else if (element.attr("name") == "service") { // for uniform checkboxes, insert the after the given container
                        error.addClass("no-left-padding").insertAfter("#form_2_service_error");
                    } else {
                        error.insertAfter(element); // for other inputs, just perform default behavoir
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   
                    success2.hide();
                    error2.show();
                    App.scrollTo(error2, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.help-inline').removeClass('ok'); // display OK icon
                    $(element)
                        .closest('.control-group').removeClass('success').addClass('error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change dony by hightlight
                    $(element)
                        .closest('.control-group').removeClass('error'); // set error class to the control group
                },

                success: function (label) {
                    if (label.attr("for") == "service" || label.attr("for") == "membership") { // for checkboxes and radip buttons, no need to show OK icon
                        label
                            .closest('.control-group').removeClass('error').addClass('success');
                        label.remove(); // remove error label here
                    } else { // display success icon for other inputs
                        label
                            .addClass('valid').addClass('help-inline ok') // mark the current input as valid and display OK icon
                        .closest('.control-group').removeClass('error').addClass('success'); // set success class to the control group
                    }
                },

                submitHandler: function (form) {
                    success2.show();
                    error2.hide();
                    form.submit();
                }

            });

            //apply validation on chosen dropdown value change, this only needed for chosen dropdown integration.
            $('.chosen, .chosen-with-diselect', form2).change(function () {
                form2.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
            });

            //Member Detail Form
            var membershipForm = $('#MembershipForm');
            var membershipFormError = $('.alert-error', membershipForm);
            var membershipFormSuccess = $('.alert-success', membershipForm);

            membershipForm.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",
                rules: {
                    Name: {
                        required: true
                    },
                    membershipTypeId: {
                        required: true
                    },
                },

                errorPlacement: function (error, element) { // render error placement for each input type        

                    error.insertAfter(element); // for other inputs, just perform default behavoir                    
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   

                    membershipFormSuccess.hide();
                    membershipFormError.show();
                    App.scrollTo(membershipFormError, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.help-inline').removeClass('ok'); // display OK icon
                    $(element)
                        .closest('.control-group').removeClass('success').addClass('error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change dony by hightlight
                    $(element)
                        .closest('.control-group').removeClass('error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .addClass('valid').addClass('help-inline ok') // mark the current input as valid and display OK icon
                    .closest('.control-group').removeClass('error').addClass('success'); // set success class to the control group

                },

                submitHandler: function (form) {
                    membershipFormSuccess.show();
                    membershipFormError.hide();
                    form.submit();
                }

            });

            //Member Detail Form
            var memberDetailForm = $('#MemberDetaillForm');
            var memberDetailFormError = $('.alert-error', memberDetailForm);
            var memberDetailFormSuccess = $('.alert-success', memberDetailForm);

            memberDetailForm.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",
                rules: {
                    Name: {
                        required: true
                    },
                    relationType: {
                        required: true
                    },
                    'memberContact.emailId': {
                        email: true
                    }
                },

                errorPlacement: function (error, element) { // render error placement for each input type        

                    error.insertAfter(element); // for other inputs, just perform default behavoir                    
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   

                    memberDetailFormSuccess.hide();
                    memberDetailFormError.show();
                    App.scrollTo(memberDetailFormError, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.help-inline').removeClass('ok'); // display OK icon
                    $(element)
                        .closest('.control-group').removeClass('success').addClass('error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change dony by hightlight
                    $(element)
                        .closest('.control-group').removeClass('error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .addClass('valid').addClass('help-inline ok') // mark the current input as valid and display OK icon
                    .closest('.control-group').removeClass('error').addClass('success'); // set success class to the control group

                },

                submitHandler: function (form) {
                    memberDetailFormSuccess.show();
                    memberDetailFormError.hide();
                }

            });


            //Member Detail Form
            var membershipDocumentForm = $('#MembershipDocumentForm');
            var membershipDocumentFormError = $('.alert-error', membershipDocumentForm);
            var membershipDocumentFormSuccess = $('.alert-success', membershipDocumentForm);
            var cntUpload = $('.uploadifyQueueItem', membershipDocumentForm);

            membershipDocumentForm.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",
                rules: {
                    Name: {
                        required: true
                    },
                    documentType: {
                        required: true
                    },
                },

                errorPlacement: function (error, element) { // render error placement for each input type        

                    error.insertAfter(element); // for other inputs, just perform default behavoir                    
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   

                    membershipDocumentFormSuccess.hide();
                    membershipDocumentFormError.show();
                    App.scrollTo(membershipDocumentFormError, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.help-inline').removeClass('ok'); // display OK icon
                    $(element)
                        .closest('.control-group').removeClass('success').addClass('error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change dony by hightlight
                    $(element)
                        .closest('.control-group').removeClass('error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .addClass('valid').addClass('help-inline ok') // mark the current input as valid and display OK icon
                    .closest('.control-group').removeClass('error').addClass('success'); // set success class to the control group

                },

                submitHandler: function (form) {
                    membershipDocumentFormSuccess.show();
                    membershipDocumentFormError.hide();
                }

            });

            var formSiteContent = $('#formSiteContent');
            var errorSiteContent = $('.alert-error', formSiteContent);
            var successSiteContent = $('.alert-success', formSiteContent);

            formSiteContent.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",
                rules: {
                    Name: {
                        required: true
                    },
                    templateId: {
                        required: true
                    },
                    emailAccountId: {
                        required: true
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    username: {
                        required: true,
                        email: true
                    },
                    salesGroupId: {
                        required: true
                    },
                    'contactModel.emailId': {
                        email: true
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit              
                    successSiteContent.hide();
                    errorSiteContent.show();
                    App.scrollTo(errorSiteContent, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.help-inline').removeClass('ok'); // display OK icon
                    $(element)
                        .closest('.control-group').removeClass('success').addClass('error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change dony by hightlight
                    $(element)
                        .closest('.control-group').removeClass('error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .addClass('valid').addClass('help-inline ok') // mark the current input as valid and display OK icon
                    .closest('.control-group').removeClass('error').addClass('success'); // set success class to the control group
                },

                submitHandler: function (form) {
                    successSiteContent.show();
                    errorSiteContent.hide();
                    form.submit();

                }
            });

            var salesMemberForm = $('#SalesMemberForm');
            var errorSalesMember = $('.alert-error', salesMemberForm);
            var successSalesMember = $('.alert-success', salesMemberForm);

            salesMemberForm.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",
                rules: {
                    Name: {
                        required: true
                    },
                    salesGroupId: {
                        required: true
                    },
                    'contactModel.emailId': {
                        email: true
                    },
                    categoryId: {
                        required: true
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit              
                    successSalesMember.hide();
                    errorSalesMember.show();
                    App.scrollTo(errorSalesMember, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.help-inline').removeClass('ok'); // display OK icon
                    $(element)
                        .closest('.control-group').removeClass('success').addClass('error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change dony by hightlight
                    $(element)
                        .closest('.control-group').removeClass('error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .addClass('valid').addClass('help-inline ok') // mark the current input as valid and display OK icon
                    .closest('.control-group').removeClass('error').addClass('success'); // set success class to the control group
                },

                submitHandler: function (form) {
                    successSalesMember.show();
                    errorSalesMember.hide();
                    form.submit();
                }
            });



        }

    };

}();

function resetFormClass(formName) {
    var form = $('#' + formName);
    var errorContent = $('.alert-error', form);
    var errorInput = $('.error', form);
    var successInput = $('.success', form);
    var inlineText = $('.help-inline', form);
    errorContent.hide();
    errorInput.removeClass('error');
    successInput.removeClass('success');
    inlineText.hide();
}

$(document).ready(function () {

    $.validator.addMethod('uploadfile', function (value, element) {

        return $(".uploadifyQueueItem").length != 0;
    },
    'This field is required.');

    $.validator.addMethod('validDate', function (value, element) {

        if (value != '') {
            var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
            return date_regex.test(value);
        }
        return true;
    },
    'Please enter a valid date.');

});


