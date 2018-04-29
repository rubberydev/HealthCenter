function validateData() {  
 
    if ($("#Password").val() === "" ||
        $("#ConfirmPassword").val() === "" ||
        $("#DocumentNumber").val() === "" ||
        $("#FirstName").val() === "" ||
        $("#LastName").val() === "" ||
        $("#Email").val() === "" ||
        $("#Telephone").val() === "" ||        
        $("#Speciality").val() === "" ) {
        swal("ERROR", "you must fill all fields...", "error");
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