window.scrollToBottom = function () {
    const messagesContainer = document.querySelector('.message-container');
    if (messagesContainer) {
        messagesContainer.scrollTop = messagesContainer.scrollHeight;
    }
};
