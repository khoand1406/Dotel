document.addEventListener("DOMContentLoaded", function () {
    this.querySelectorAll(".conversation-item-wrapper").forEach(item => {
        item.addEventListener("click", function () {
            const conversationId = this.getAttribute("data-convid");
            fetch("?handler=LoadConversation", {
                method: 'POST',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                body:
                    "conversationId=" + encodeURIComponent(conversationId)

            }).then(response => response.text())
                .then(html => {
                    document.getElementById('chat-main').innerHTML = html;
                    document.querySelectorAll(".conversation-item").forEach(el => el.classList.remove("active"));
                    this.querySelector(".conversation-item").classList.add("active");
                }).catch(error => {
                    console.error('Failed to load conversation', error);
                });
        }
        );
    });
});