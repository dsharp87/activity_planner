$(".questionMarkIcon").hover(
    function() {
        if($(".passwordInput").val().length > 0)
        {
            validatePassword($(".passwordInput").val())
        }
        $(".passwordInfoPopupText").addClass("show");
    },
    function() {
        $(".passwordInfoPopupText").removeClass("show")       
    })

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