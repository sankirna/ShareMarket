var Common = function () {
    /*Register a handler to be called when the first Ajax request begins.*/
    $(document).ajaxStart(function (data) {
        Common.AjaxStart(data);
    });

    /*Register a handler to be called when all Ajax requests have error.*/
    $(document).ajaxError(function (data) {
        Common.AjaxError(data);
    });

    /*Register a handler to be called when all Ajax requests stop.*/
    $(document).ajaxStop(function (data) {
        Common.AjaxStop(data);
    });

    /*Register a handler to be called when all Ajax requests complete.*/
    $(document).ajaxComplete(function (data) {
        Common.AjaxComplete(data);
    });

    /*Register a handler to be called when all Ajax requests success.*/
    $(document).ajaxSuccess(function (data) {
        Common.AjaxSuccess(data);
    });

    /*Clear All form errors*/
    $.fn.clearFormErrors = function () {
        $(this).each(function () {
            $(this).find(".field-validation-error").empty();
        });
    };

    /*Function clear the model validation inside element mentioned.*/
    $.fn.clearError = function () {
        $(this).find(".field-validation-error").empty();
    };
    return {

        /*CHeck Is variable is defined*/
        IsDefined: function (variable) {
            return typeof variable !== "undefined";
        },

        /*CHeck Is Data is null*/
        IsDataNull: function (data) {
            if (typeof (data) != 'undefined' && data != null && data != undefined && data != 'undefined' && data != "")
                return false;
            return true;
        },

        /*CHeck Is Data of type object*/
        IsTypeOfObject: function (data) {
            return typeof data === "object";
        },

        /*CHeck Is Data of type string*/
        IsTypeOfString: function (data) {
            return typeof data === "string";
        },

        /*CHeck Is Data of type boolean*/
        IsTypeOfBoolean: function (data) {
            return typeof data === "boolean";
        },

        /*CHeck Is Data of type function*/
        IsTypeOfFunction: function (data) {
            return typeof data === "function";
        },

        /*CHeck Is Data of type number*/
        IsTypeOfNumber: function (data) {
            return typeof data === "number";
        },

        /*CHeck Is Data of type undefined*/
        IsUnDefined: function (data) {
            return typeof data === "undefined";
        },

        /*CHeck Is Data of type Date*/
        IsDate: function (data) {
            var objDate = new Date(data);
            if (Object.prototype.toString.call(objDate) === "[object Date]")
                if (isNaN(objDate.getTime()) || objDate.getTime() == 0)
                    return false;
                else
                    return true;
            else
                return false;
        },

        /*Check string is null or empty*/
        IsNullOrEmptyWhiteSpace: function (stringVal) {
            return stringVal == null || $.trim(stringVal) == '';
        },

        /*Load External templates from request*/
        LoadExternalTemplate: function (path) {
            $.get(path)
                .success(function (result) {
                    $("body").append(result);
                })
                .error(function (result) {
                    ConsoleDebug("Error Loading Templates -- TODO: Better Error Handling");
                });
        },

        /*Parse json date string*/
        ParseJsonDateString: function (value) {
            var arr = value && JsonDateRegEx.exec(value);
            if (arr) {
                return new Date(parseInt(arr[1]));
            }
            return value;
        },

        /* Show Ajax Process Loader */
        ShowAJAXLoader: function () {

        },

        /* Hide Ajax Process Loader */
        HideAJAXLoader: function () {

        },

        /*Log Ajax Start*/
        AjaxStart: function (data) {
            Common.ShowAJAXLoader();
        },

        /*Log Ajax Complete*/
        AjaxComplete: function (data) {
            Common.HideAJAXLoader();
        },

        /*Log Ajax Success*/
        AjaxSuccess: function (data) {
            Common.HideAJAXLoader();
        },

        /*Log Ajax Error*/
        AjaxError: function (data) {
            Common.HideAJAXLoader();
        },

        /*Log Ajax Stop*/
        AjaxStop: function (data) {
            Common.HideAJAXLoader();
        },

        /*Check Is Element Empty or Not*/
        IsElementEmpty: function (elementId) {
            var element = $("#" + elementId);
            return element.is(':empty') || !$.trim(element.html()).length <= 0;
        },

        /*Get query string parameter from current url*/
        GetParameterByNameFromCurrentUrl: function (name) {
            return Common.GetParameterByNameFromUrl(name, location.search);
        },

        /*Get query string parameter from url*/
        GetParameterByNameFromUrl: function (name, url) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)");
            var results = regex.exec(url);
            return results == null ? null : decodeURIComponent(results[1].replace(/\+/g, " "));
        },

        /*Get data-* attribute*/
        GetDataAttrVal: function (element, attributeName) {
            if (element.context.dataset) {
                return eval('element.context.dataset.' + attributeName);
            }
            else {
                return element.attr('data-' + attributeName);
            }
        },

        /*Knockout custom utility Function*/
        KOut: {
            //Check is Collection is Null or Enpty
            IsNullOrEmptyCollection: function (coll) {
                return coll == null || coll.length <= 0;
            },

            //Clean Node And Re Apply bindings
            CleanNodeAndReApplyBindings: function (vm, node) {
                ko.cleanNode(node);
                ko.applyBindings(vm, node);
            }
        },
        Array: {
            SortFuns: {
                CaseInsensitive: function (a, b) {
                    if (a.toLowerCase() < b.toLowerCase()) return -1;
                    if (a.toLowerCase() > b.toLowerCase()) return 1;
                    return 0;
                }
            }
        },

        IsPageLoaded: false,

        RegularExpression: {
            EmailRegEx: "[ ]*[_A-Za-z0-9-]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,4})[ ]*$"
        },

        BlockUI: function () {

        },

        UnBlockUI: function () {

        },

        //Set Selcted Menue based on data-meta-name of li
        SetActiveMenuFromHash: function () {
            if (location.hash.length > 2) {
                $("li.active").removeClass("active");
                var hashLength = location.hash.length;
                var pageMetaName = location.hash.slice(2, hashLength);
                var currentMenu = $('li[data-meta-name$="' + pageMetaName + '"]');
                currentMenu.addClass("active");
                var parentMenu = currentMenu.closest(".group-menu");
                if (parentMenu.length > 0) {
                    parentMenu.addClass("open");
                    parentMenu.find("span.arrow").addClass("open");
                    parentMenu.find("ul.sub-menu").show();
                }
            }
        },

        //Show Error
        ShowError: function (message, isAutoHide, targetDom) {
            var notificationArea = targetDom || $(".notifications");
            notificationArea.loadTemplate($("#errorTemplate"),
            {
                status: message
            });
            if (isAutoHide) {
                setTimeout(function () {
                    notificationArea.children().fadeOut("slow", function () {
                        notificationArea.empty();
                    });
                }, 2000);
            }
        },

        //Show Success
        ShowSuccess: function (message, isAutoHide, targetDom) {
            var notificationArea = targetDom || $(".notifications");
            notificationArea.loadTemplate($("#successTemplate"),
            {
                status: message
            });
            if (isAutoHide) {
                setTimeout(function () {
                    notificationArea.children().fadeOut("slow", function () {
                        notificationArea.empty();
                    });
                }, 2000);
            }
        },

        //Clean And ReApply Validors on Form
        CleanAndReApplyValidators: function (formSelector) {
            var $form = $(formSelector);
            $form.removeData("validator");
            $form.removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse($form);
        },

        //Do AJAX Call
        AjaxPost: function (options) {
            var defaults = {
                type: "POST",
                contentType: 'application/json; charset=utf-8',
            };
            // Merge defaults and options, without modifying defaults
            var ajaxSettings = $.extend({}, defaults, options);
            $.ajax(ajaxSettings);
        },

        SetFocusOnFirstTextBox: function () {
            $("input[type=text]").first().focus();
        }
    };
}();

//Console Log
function ConsoleLog(dataToLog) {
    if (window.console && console.log) {
        console.log(dataToLog);
    }
}

//Console Debug
function ConsoleDebug(dataTodebug) {
    if (window.console && console.debug) {
        console.debug(dataTodebug);
    }
}

function SetDefaultButton(obj) {
    var e = window.event || arguments.callee.caller.arguments[0];
    var kCode = e.keyCode || e.charCode; //for cross browser
    if (kCode == 13) {
        var id = obj.id;
        var defaultbtn = $('#' + id).attr("defaultbutton");
        $("#" + defaultbtn).click();
        return false;
    }
}

//Dom Ready Event
$(document).ready(function () {
    //perform common operation after DOM loaded
    Common.SetFocusOnFirstTextBox();
});

//Window Load Event
$(window).load(function () {
    //perform common operation after window resources loaded
    Common.IsPageLoaded = true;
    var notificationMessageArea = $(".alert");
    setTimeout(function () {
        notificationMessageArea.fadeOut("slow", function () {
            notificationMessageArea.hide();
        });
    }, 2000);
});