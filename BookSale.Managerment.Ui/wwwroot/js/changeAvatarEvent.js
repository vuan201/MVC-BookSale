const avatarImg = document.querySelector("#avatar-img");
const fileImgInput = document.getElementById("file-input");

fileImgInput.addEventListener("change", function () {
    const input = this.files[0];

    if (input) {
        avatarImg.classList.remove("no-image"); // Remove trước khi load
        avatarImg.src = URL.createObjectURL(input);
    }
});

avatarImg.addEventListener("error", function () {
    onErrorImage();
});

function onErrorImage() {
    avatarImg.src = "~/image/no_image.jpg";
    avatarImg.alt = "Default Avatar";
    avatarImg.classList.add("no-image"); // Thêm lại nếu ảnh lỗi
}
