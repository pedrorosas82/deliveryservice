# Delivery Service API

The project should be run under Visual Studio 2017 or later.

To use a database filled with some example data, please copy the files under the folder "database-init" to the folder "DeliveryService.WebApi\App_Data".

Start the application and use a tool of your choice to build queries to the API. My favorite one is Postman.

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
