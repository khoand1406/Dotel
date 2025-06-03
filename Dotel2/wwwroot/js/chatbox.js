document.addEventListener("DOMContentLoaded", function () {
    const chatPopup = document.getElementById("chat-popup");
    const openBtn = document.getElementById("open-chat");
    const closeBtn = document.getElementById("close-chat");
    const sendBtn = document.getElementById("send-message");
    const messageInput = document.getElementById("message-input");
    const chatBody = document.getElementById("chat-body");

    if (openBtn && chatPopup && closeBtn && sendBtn && messageInput && chatBody) {
        openBtn.addEventListener("click", function () {
            chatPopup.style.display = "flex";
        });

        closeBtn.addEventListener("click", function () {
            chatPopup.style.display = "none";
        });

        sendBtn.addEventListener("click", async function () {
            const message = messageInput.value;
            if (message.trim() === "") return;

            // Hiển thị ngay bên client
            const msgDiv = document.createElement("div");
            msgDiv.textContent = "Bạn: " + message;
            chatBody.appendChild(msgDiv);
            messageInput.value = "";

            // Gửi API đến server (bạn cần cập nhật recipientId phía Razor)
            await fetch("/api/messages/send", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    recipientId: window.recipientId, // gán trong Razor
                    content: message
                })
            });
        });
    }
});