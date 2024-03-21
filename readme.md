# MassTransit Sample

Showcases a basic setup for Pub/Sub with MassTransit. 

## How to run

The sample requires [Docker](https://www.docker.com/) to run on your machine.

Run the docker-compose file from the Deployments folder.

```sh
docker-compose up -d
```
It sets up a default instance of RabbitMQ in a container, reachable at [http://localhost:15672](http://localhost:15672)
un: guest
pwd: guest

Then run the Producer and Consumer projects (both are console apps). Pressing a key in the Producer console publishes a KeyPressed event to be consumed by the Consumer.

## Different setups in different branches
- branch 02-errors-and-faults: demonstrated handling errors and faults (second-level retry), individual configuration of a single consumer...
- branch 03-azure-servicebus: Basic setup with Azure service bus.
- branch 04-interoperability: uses the raw Json serializer so that messages can be produced/consomed with non .NET applications.
