function validateData() {  

    var x = document.getElementById("Email").value;
    var expregEmail = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/gi; 

    var y = document.getElementById("Password").value;
    var z = document.getElementById("ConfirmPassword").value;  


    if ($("#Password").val() === "" ||
        $("#ConfirmPassword").val() === "" ||
        $("#DocumentNumber").val() === "" ||
        $("#FirstName").val() === "" ||
        $("#LastName").val() === "" ||
        $("#Email").val() === "" ||
        $("#Telephone").val() === "" ||
        $("#Speciality").val() === "") {
        swal("ERROR", "you must fill all fields...", "error");
        return false;

    } else if (!expregEmail.test(x)) {

        swal("ERROR", "it isn't an email invalid, try again.", "error");
        return false;

    } else if (y.length < 6) {

        swal("ERROR", "The field password should contain at least 6 characters, try again...", "error");
        return false;

    } else if (y !== z) {

        swal("ERROR", "Passwords it didn't match, try again", "error");
        return false;

    } else {
        swal("", "", "success");
        return true;
    }
}

function Validate(ctl, event) {
    event.preventDefault();
    
           
    swal({
        title: "Do you want to save this data?",
        text: "Check the information before to save!!",
        type: "info",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Save",
        cancelButtonText: "Cancel",
        closeOnConfirm: false,
        closeOnCancel: false
    },

        function (isConfirm) {

            if (isConfirm) {
                if (validateData(this.ctl)) {
                    swal("", "", "success");
                    $("#SavedForm").submit();
                    isConfirm.closeOnConfirm = true
                }
            } else {
                swal("Task cancelled", "You didn't save nothing!", "error");
            }
        });
}