# RabbitMQ.Demo

A simple demo showing **asynchronous communication** between services using **message queues**.  
This illustrates a **Producer–Consumer pattern** with a message broker for decoupled, scalable systems.

---

## Architecture Overview

Client → Producer Service → Message Broker (Queue) → Consumer Service → Processing Logic


- **Producer Service**: Sends events/messages to a queue.  
- **Consumer Service**: Listens to the queue and processes messages asynchronously.  
- **Message Broker**: Handles delivery and persistence of messages (e.g., RabbitMQ).

---

## Components

1. **Producer**
   - Accepts requests/events
   - Publishes messages to the queue
   - Example message payload: JSON object with relevant data
   - Declares queue (durable, non-exclusive)

2. **Consumer**
   - Connects to the message broker
   - Subscribes to the queue
   - Processes incoming messages asynchronously
   - Can run in parallel or multiple instances

3. **Message Broker (RabbitMQ)**
   - Stores and routes messages
   - Supports durable queues and management UI
   - Can be run in Docker for development/testing

---

## Running the Demo

1. Start the message broker (docker-compose up -d)
2. Run the Producer service
3. Run the Consumer service
4. Send events/requests to the Producer
5. Observe asynchronous processing in the Consumer

---

## Key Concepts

- **Producer–Consumer Pattern**: Decouples services and allows asynchronous processing.  
- **Durable Queues**: Ensure messages are not lost on broker restart.  
- **Scalable Architecture**: Multiple producers and consumers can work independently.  
- **Message Serialization**: JSON or other formats for transport.

---

## Benefits

- Loose coupling between services  
- Asynchronous and parallel processing  
- Reliable message delivery  
- Foundation for event-driven microservices

---

