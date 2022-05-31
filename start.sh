sudo docker-compose up --build --detach

sudo docker run --rm -d -p 27021:27017 -v /home/vagrant/mongodbdata:/data/db -h 127.0.0.1 --name mongo mongo:4.2.0 --replSet=dbrs 

sleep 5 

sudo docker exec mongo mongo --eval "rs.initiate();"