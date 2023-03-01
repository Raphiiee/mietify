### Start Docker Containers
docker compose up -d

### Create new Topic
docker exec \<BROKER CONTAINER NAME> kafka-topics --bootstrap-server \<BROKER CONTAINER NAME>:\<BROKER PORT> --create --topic \<TOPIC NAME>
  
### Write to Topic
docker exec --interactive --tty \<BROKER CONTAINER NAME> kafka-console-producer --bootstrap-server \<BROKER CONTAINER NAME>:\<BROKER PORT>  --topic \<TOPIC NAME>
  
### Read All From Topic
docker exec --interactive --tty \<BROKER CONTAINER NAME> kafka-console-consumer --bootstrap-server \<BROKER CONTAINER NAME>:\<BROKER PORT>  --topic \<TOPIC NAME> --from-beginning 
