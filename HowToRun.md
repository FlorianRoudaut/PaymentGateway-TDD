#How To Run the Gateway


##1 Clone the repository
Clone the repository from github 
You can use cmd line "git clone https://github.com/YOUR-USERNAME/YOUR-REPOSITORY"
Or you can use a GUI git client like GitKraken

##2 Install .Net Core 3.0 
### On linux
sudo apt-get update
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-sdk-3.0

##3 Run on standalone
Go the subfolder ./PaymentGateway and run the commands
dotnet build PaymentGateway.csproj
dotnet run PaymentGateway.csproj

You can test by opening a browser and going to http://localhost:5000/Home . You should see "Hello this is home"

##4 Or Run on docker
Got to the root folder (with the Dockerfile) and run the cmd
sudo docker build -t paymentgateway .

This will build the docker image called paymentgateway. Then you can run the docker image using 

sudo docker run  -it --rm -p 5000:80 paymentgateway
This will run the paymentgateway image in docker, mapping the container 80 port to the 5000 port of the host machine.


You can test by opening a browser and going to http://localhost:5000/Home . You should see "Hello this is home"

https://docs.microsoft.com/en-us/dotnet/core/docker/build-container