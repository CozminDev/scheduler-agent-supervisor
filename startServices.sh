sudo docker run --name mongo-database -d -p 27017:27017 -v /home/vagrant/mongodbdata:/data/db mongo:4.2.0

sudo docker run --name rabbitmq-queue -d -p 15672:15672 -p 5672:5672 rabbitmq:3-management
