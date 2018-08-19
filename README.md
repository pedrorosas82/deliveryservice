# Delivery Service API

The project should be run under Visual Studio 2017 or later.

To use a database filled with some example data, please copy the files under the folder "database-init" to the folder "DeliveryService.WebApi\App_Data".

Start the application and enjoy.

# API Examples

GET /api/routes - get all routes
GET /api/routes/origin/1/destination/2 - get all routes from point 1 to point 2
POST /api/routes - save new route
PUT /api/routes - update existing route
DEL /api/routes/1 - delete route 1

GET /api/points - get all points
POST /api/points - save new points
PUT /api/points - update existing points
DEL /api/points/1 - delete point 1
