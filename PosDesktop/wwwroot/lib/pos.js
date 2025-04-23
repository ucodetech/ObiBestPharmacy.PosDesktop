function playBeep() {
    var audio = new Audio('https://pixabay.com/sound-effects/beep-warning-6387/');
    audio.play();
}

window.scrollableModal = {
    showModal: function (modalId) {
        const modalEl = document.getElementById(modalId);
        const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
        modal.show();
    },
    hideModal: function (modalId) {
        const modalEl = document.getElementById(modalId);
        const modal = bootstrap.Modal.getInstance(modalEl);
        if (modal) {
            modal.hide();
        }
    }
};
