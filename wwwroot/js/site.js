$(function () {
    addRegistrationEventListeners();
    addActivityCreationEventListeners();
});


//LOGIN REGISTRATION START

function addRegistrationEventListeners()
{
    $(".questionMarkIcon").on('mouseenter', function() {
        if($(".passwordInput").val().length > 0)
        {
            validatePassword($(".passwordInput").val())
        }
        $(".passwordInfoPopupText").addClass("show");
    });

    $(".questionMarkIcon").on('mouseleave', function() {
            $(".passwordInfoPopupText").removeClass("show")       
    });

}



function validatePassword(password)
{
    console.log(password);
    //string length
    if(password.length >= 8)
    {
        $(".charCountRequirement").removeClass("darkred");
        $(".charCountRequirement").addClass("green");
    }
    else
    {
        $(".charCountRequirement").removeClass("green");
        $(".charCountRequirement").addClass("darkred");
    }
    //uppercase
    if(/[A-Z]/.test(password))
    {
        $(".upperCaseRequirement").removeClass("darkred")
        $(".upperCaseRequirement").addClass("green");
    }
    else
    {
        $(".upperCaseRequirement").removeClass("green")
        $(".upperCaseRequirement").addClass("darkred");
    }
    //number
    if(/[0-9]/.test(password))
    {
        $(".numberRequirement").removeClass("darkred")
        $(".numberRequirement").addClass("green");
    }
    else
    {
        $(".numberRequirement").removeClass("green")
        $(".numberRequirement").addClass("darkred");
    }
    //special characters
    if(/[~`!@#$%^&*()_\-+={[}\]|\\:;"'<,>.?/]/.test(password))
    {
        $(".specialCharacterRequirement").removeClass("darkred")
        $(".specialCharacterRequirement").addClass("green");
    }
    else
    {
        $(".specialCharacterRequirement").removeClass("green")
        $(".specialCharacterRequirement").addClass("darkred");
    }
    //no spaces
    if(!/\s/.test(password))
    {
        $(".noSpacesRequirement").removeClass("darkred");
        $(".noSpacesRequirement").addClass("green");
    }
    else
    {
        $(".noSpacesRequirement").removeClass("green");
        $(".noSpacesRequirement").addClass("darkred");
    }
}

//LOGIN REGISTRATION START END

//ACTIVITY CREATION START

function addActivityCreationEventListeners()
{
    $(".streetInput, .cityInput, .stateInput, .zipInput").on("focus", function() {
        var currentVal = $(this).val();
        $(this).attr("data-current-value", currentVal);
        console.log("assigned val is: " + $(this).attr("data-current-value"));
    });

    $(".streetInput, .cityInput, .stateInput, .zipInput").on("blur", function() {
        detectInputValueChange($(this));
        refreshActivityCreationMap();
    });
}

function detectInputValueChange(element)
{
    var origionalVal = element.attr("data-current-value");
    var currentVal = element.val();
    if(currentVal !== origionalVal)
    {
        element.attr("data-current-value", element.val());
        element.attr("data-changed-flag", "true");
    }
    else
    {
        element.attr("data-changed-flag", "false");
    }
}

function refreshActivityCreationMap()
{
    //store current vals in fields
    var street = $(".streetInput").val();
    var city = $(".cityInput").val();
    var state = $(".stateInput").val();
    var zipCode = $(".zipCodeInput").val();

    //don't trigger refresh if nothing has changed
    if($(".streetInput").attr("data-changed-flag") == "false" &&
        $(".cityInput").attr("data-changed-flag") == "false" &&
        $(".stateInput").attr("data-changed-flag") == "false" &&
        $(".zipCodeInput").attr("data-changed-flag") == "false")
    {
        return;
    }

    //set up formatting
    var addressVals = [street, city, state, zipCode];    
    var formatedVals = [];

    //format non empty fields
    addressVals.forEach((v) => 
    {
        if(v != "")
        {
            formatedVals.push(formatStringForMaps(v));
        }
    });


    //if we have 0 values, exit function
    if(formatedVals.length == 0)
    {
        return;
    }

    //construct new src string from formated values
    var srcString="https://www.google.com/maps/embed/v1/place?key=AIzaSyC95F71hOI6L7Zp8n-6r1Jg3fYK36RrLQo&q=";
    for(var i = 0; i < formatedVals.length; i++)
    {
        var subString = ""
        if(i != 0)
        {
            subString+= ",+"
        }
        subString+= formatedVals[i];
        srcString += subString;
    }

    //update src string and reload Iframe
    $("#createMapIframe").attr("src",srcString);

    

}

function formatStringForMaps(s)
{

    return s.replace(/ /g, "+");
}

//ACTIVITY CREATION END