function showToaster(type, message, timeOut = 5000) {
    $.toast({
        heading: type,
        text: message,
        showHideTransition: 'plain',
        showHideTransition: 'fade',
        stack: 4,
        position: 'top-right',
        icon: type == "Information" ? "info" : type.toLowerCase(),
        hideAfter: timeOut,
    })
}