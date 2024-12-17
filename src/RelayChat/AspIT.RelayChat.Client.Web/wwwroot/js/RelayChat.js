window.scrollToBottom = function () {
    const container = document.getElementById('messageContainer');
    if (container) {
        container.scrollTop = container.scrollHeight;
    }
};