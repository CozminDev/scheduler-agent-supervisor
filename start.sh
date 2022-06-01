sudo docker-compose up --build --detach

sleep 5 

sudo docker exec mongo mongo --eval "rs.initiate();"