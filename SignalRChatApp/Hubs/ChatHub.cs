using System.Net.WebSockets;          // For working with WebSocket connections (SignalR uses this under the hood)
using Microsoft.AspNetCore.SignalR;  // Provides Hub and real-time messaging support

namespace SignalRChatApp.Hubs
{
    public class ChatHub : Hub
    {
        // This method is called when a client wants to send a message
        public async Task SendMessage(string user, string message)
        {
            // Broadcast the message to all connected clients
            await Clients.All.SendAsync(
                "ReceiveMessage",  
                user,              
                message,          
                DateTime.Now.ToString("HH:mm:ss") 
            );
        }
    }
}


/*
================ APPLICATION FLOW & SIGNALR LOGIC =================

1. Server-Side (C# ChatHub):
   - ChatHub inherits from SignalR's Hub class. The Hub acts as the central server 
     endpoint for real-time messaging.
   - When a client wants to send a message, it calls the server method:
         SendMessage(user, message)
   - The server receives the message and broadcasts it to all connected clients using:
         Clients.All.SendAsync("ReceiveMessage", user, message, timestamp)
   - This ensures **every client sees messages instantly** without refreshing the page.

2. Client-Side (chat.js + SignalR JS library):
   - SignalR JS (`signalr.min.js`) sets up a persistent connection to the server.
   - chat.js uses this connection to:
       a) Send messages typed by the user to the server.
       b) Listen for "ReceiveMessage" events from the server.
       c) Update the chat UI in real-time, appending new messages to the chat box.

3. Real-Time Flow (step by step):
   - User types a message in the input box.
   - chat.js calls `connection.invoke("SendMessage", user, message)`.
   - SignalR sends the message over WebSocket (or fallback transport) to ChatHub.
   - ChatHub broadcasts the message to all connected clients via `ReceiveMessage`.
   - chat.js receives the message and updates the UI instantly.

4. Timestamp:
   - Each message includes a timestamp (`HH:mm:ss`) so users know when it was sent.

5. Analogy (Simple Explanation):
   - Think of the app as a **mailroom with instant delivery**:
       - **Browser clients** → people dropping letters (messages) into the mailroom.
       - **ChatHub (C# server)** → the mailroom clerk who receives letters and distributes them instantly.
       - **SignalR** → the courier system that ensures letters reach all recipients immediately, 
         even if roads (network conditions) are tricky.
       - **chat.js** → the person arranging letters in a readable way on the recipient’s desk (UI).

6. Key Benefit of SignalR:
   - Automatically handles real-time communication using the best available method (WebSocket, 
     Server-Sent Events, or Long Polling) without extra code.
   - Makes building live apps (chat, notifications, dashboards) simple and reliable.

Summary:
- The server manages logic, routing, and broadcasting.
- SignalR handles communication and connectivity.
- Client JS handles UI and user interactions.
- Together, they create a seamless **real-time chat experience**.
*/
