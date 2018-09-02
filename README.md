# Delivery Service API

The project should be run under Visual Studio 2017 or later.

To use a database filled with some example data, please copy the files under the folder "database-init" to the folder "DeliveryService.WebApi\App_Data".

Start the application and use a tool of your choice to build queries to the API. 
My favorite one is Postman.

# API Authentication

Some operations are protected for admins only.
For testing purposes and ease of use, the following endpoint provides an easy way to create an admin user:

POST /api/v1/accounts/admins

In its body, please send the following JSON (replace the values for whatever you'd prefer):
{
  "userName": "your_username",
  "password": "your_password",
  "confirmPassword": "your_password"
}

The protected resources use the Bearer token authentication type. 
To get a token, please invoke the following endpoint after creating the user:

POST /api/v1/auth/token

In its body please choose "x-www-form-urlenconded" with the following params:
grant_type:password
username: "your_username"
password: "your_password"


# API Examples

GET /api/v1/routes - get all routes

GET /api/v1/routes/origin/2/destination/4 - get all routes from point 1 to point 2

POST /api/v1/routes - save new route

PUT /api/v1/routes - update existing route

DEL /api/v1/routes/3 - delete route 1


GET /api/v1/points - get all points

POST /api/v1/points - save new points

PUT /api/v1/points - update existing points

DEL /api/v1/points/1 - delete point 1


# Unit Testing

Both projects DeliveryService.BLL and DeliveryService.WebApi are fully covered by automated unit tests.
These tests were built over the NUnit framework, please use any compatible tool to run the tests.

For coverage, AxoCover can be used to get the metrics.
