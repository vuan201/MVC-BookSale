(function () {
    const avatarImg = document.querySelector('#avatar-img');

    document.getElementById("input-avatar").onchange(function () {
        const input = this.files[0];

        if (input) {
            avatarImg.src = URL.createObjectURL(input);
        }
    })

    avatarImg.onerror = function () {

    }

    function onErrorImage() {
        avatarImg.src = "~/image/no_image.jpg";
        avatarImg.alt = "Default Avatar";
    }

}())