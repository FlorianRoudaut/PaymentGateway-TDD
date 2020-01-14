# PaymentGateway-TDD

The two key functionnalites of this sample Payment Gateway are :

    A merchant should be able to process a payment through the payment gateway and receive either a successful or unsuccessful response

    A merchant should be able to retrieve the details of a previously made payment
    
 ## Methodology 
 In this project the payment gateway was built using a Test Driven Development methodology. First, a functionnal analysis described in the file Functionnal Analysis.md has been done. The goal of this analysis is to identify the key functionnalities of the gateway using a top down approach. First, key features were considerered, then they were split into sub features. Input validations are detailed in InputsValidationCases.md
After that, for each sub feature iteratively, automated test cases were built, failed, developped and tested. Moving from one sub feature to another.


## Architecture
This project kept with a simple architecture. It is composed of a single monolithic service with a dependency that mocks an acquiring bank dll API. There is an additional project that contains Unit Tests.

Everything is written in .Net Core 3.0, using ASP.Net for the web service part. 

The project can run on a Standalone mode on Linux, or on Docker on Linux. Detailed instructions are found in the page HowToRun.md

## Scalability
The service component is stateless (excluding the db part), so if the usage of the service increases, it is possible to launch multiple instances of the gateway service and segregate users on the different services either using a load balancer or using sharding. 

This project has an hardcoded in memory database, so currently we cannot have yet multiple instances. Adding support for a database that can handle multi processes is a prerequisite for launching multiple instances that have access to a common data set. 
