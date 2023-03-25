### Start Docker Containers
docker compose up -d

### Create new Topic
docker exec \<BROKER CONTAINER NAME> kafka-topics --bootstrap-server \<BROKER CONTAINER NAME>:\<BROKER PORT> --create --topic \<TOPIC NAME>
docker exec \<BROKER CONTAINER NAME> kafka-topics --create --bootstrap-server localhost:9092 --replication-factor 3 --partitions 3 --topic \<TOPIC NAME>


### Write to Topic
docker exec --interactive --tty \<BROKER CONTAINER NAME> kafka-console-producer --bootstrap-server \<BROKER CONTAINER NAME>:\<BROKER PORT>  --topic \<TOPIC NAME>
  
### Read All From Topic
docker exec --interactive --tty \<BROKER CONTAINER NAME> kafka-console-consumer --bootstrap-server \<BROKER CONTAINER NAME>:\<BROKER PORT>  --topic \<TOPIC NAME> --from-beginning 

### Show all Topics on Broker
docker exec \<BROKER CONTAINER NAME> bash -c "kafka-topics --list --bootstrap-server localhost:9092"

### Build Docker Image of Backend Application
in the "Mietify.Backend" directory do the following:
docker build . -t mietifybackend

### Build Docker Image of DataProducer 
in the "Mietify" directory do the follwing:
docker build . -t producer --file Mietify.DataProducer/Dockerfile

### Build Docker image of Consumer
in the "Mietify" directory do the following:
docker build . -t consumer --file Mietify.Consumer/Dockerfile

### PostgresDB Connection
Pgadmin - localhost:5050, email: root@root.com, pw: root, dbpw: root
Postgresql - localhost:5432, dbpw: root, dbname: test_db (see pgpass file)
